using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyActions : MonoBehaviour
{
    //ATTACK
    [SerializeField] private float damage = 1f;
    [SerializeField] private float accuracy = 0.7f;     //The accuracy of the enemy's shots, where 1 is perfect accuracy and 0 is total inaccuracy.
    [SerializeField] private float fireRate = 2.0f;     //How often an enemy can fire    
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource shotSound;    
    private bool isReloading = false;

    //Components
    [SerializeField] private AnimationManager animationManager;
    private NavMeshAgent agent;
    private Sight sight;
    private EnemyFSM fsm;

    //Patrol settings
    [Header("Patrolling Settings")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float minDistToWaypoint = 1;   //Will switch waypoint destination when close enough to their current waypoint
    [SerializeField] private int waypointIndex = 0;

    //InvestigateSettings
    private Vector3 lastPlayerPos;
    private bool isRotating = false;

    void Start()
    {
        //Cache
        this.agent = GetComponent<NavMeshAgent>();
        this.agent.speed = 6;
        this.sight = GetComponent<Sight>();
        this.fsm = GetComponent<EnemyFSM>();
    }

    // Update is called once per frame
    void Update()
    {        

        switch (fsm.CurrentState)
        {
            case EnemyStates.IDLE:
                break;
            case EnemyStates.PATROL:
                this.Patrol();
                break;
            case EnemyStates.INVESTIGATE:
                this.Investigate();
                break;
            case EnemyStates.CHASE:
                this.Chase();
                break;
            case EnemyStates.ATTACK:
                this.Attack();
                break;
            default: break;
        }
    }

    public void Patrol()
    {
        animationManager.SetBool("isMoving", true);

        //Action: Patrol movement
        Vector3 destination = this.waypoints[waypointIndex].position;
        this.agent.SetDestination(destination);        

        float distance = Vector3.Distance(transform.position, destination);
        if (distance < this.minDistToWaypoint)
        {
            //animationManager.SetBool("isMoving", false);
            this.waypointIndex++;
            if (this.waypointIndex >= this.waypoints.Length)
                this.waypointIndex = 0;
            this.fsm.ChangeState(EnemyStates.IDLE);

          animationManager.SetBool("isMoving", false);

        }
    }
 

    public void Chase()
    {
        this.lastPlayerPos = this.fsm.Target.position;
        this.agent.SetDestination(this.fsm.Target.position);        
    }

    public void Investigate()
    {
        if(isRotating) { return; }
        if(Vector3.Distance(this.transform.position, lastPlayerPos)< 1)
        {
            StartCoroutine(RotateEnemy());
        }
        else
        {
            this.agent.SetDestination(this.lastPlayerPos);
        }
    }

    public void Attack()
    {
        //Look at target
        this.lastPlayerPos = this.fsm.Target.position;
        this.transform.LookAt(this.fsm.Target.position);
        
        if (this.isReloading) //Exit Attack if enemy is reloading
            return;

        //Stop moving
        this.agent.isStopped = true;
        //Set Animation Shoot
        this.Shoot();
    }

    public IEnumerator ReloadGun()
    {
        this.isReloading = true;
        yield return new WaitForSeconds(this.fireRate);
        this.isReloading = false;
        this.agent.isStopped = false;        
    }

    public IEnumerator RotateEnemy()
    {
        this.isRotating = true;
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while(t < 2 && this.fsm.IsInvestigating)
        {
            t+= Time.deltaTime;
            float yRotation = (startRotation + t / 2.0f * 360.0f) % 360.0f;
            this.transform.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
            yield return new WaitForFixedUpdate();
        }
        this.isRotating = false;
        this.fsm.IsInvestigating = false;
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        shotSound.Play();
        if(Physics.Raycast(this.bulletSpawn.transform.position, this.transform.forward, out hit))
        {
            PlayerFct playerScript = hit.transform.GetComponent<PlayerFct>();
            if (playerScript != null)
            {
                if (Random.Range(0, 1) >= this.accuracy)
                {
                    return;
                }
                Debug.Log("Hit Player");
                playerScript.TakeDamage(this.damage);
            }
        }

        //Reload Gun
        StartCoroutine(ReloadGun());        
    }
}
