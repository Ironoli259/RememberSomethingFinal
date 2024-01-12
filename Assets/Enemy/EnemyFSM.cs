using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Responsibility: Manage the AI's states
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFSM : MonoBehaviour
{
    Transform target;

    //IDLE SETTINGS
    [SerializeField] private float maxIdleTime = 1;
    private float currentIdleTime = 0;

    //INVESTIGATE SETTINGS
    private bool isInvestigating = false;

    //Attack settings
    [SerializeField] private float attackDistance = 1;

    //Sensors
    private NavMeshAgent agent;
    [SerializeField] private EnemyStates currentState;

    [SerializeField] private Sight sight;

    public EnemyStates CurrentState { get => currentState; set => currentState = value; }
    public Transform Target { get => target; set => target = value; }
    public bool IsInvestigating { get => isInvestigating; set => isInvestigating = value; }

    //Hearing hearing;

    private void Start()
    {
        //CACHE
        this.agent = GetComponent<NavMeshAgent>();        
        //hearing = GetComponent<Hearing>();

        this.target = PlayerControl.instance.transform;
    }
    private void Update()
    {

        switch (this.CurrentState)
        {
            case EnemyStates.IDLE:
                this.Idle();
                break;
            case EnemyStates.PATROL:
                this.Patrol();
                break;
            case EnemyStates.CHASE:
                this.Chase();
                break;
            case EnemyStates.INVESTIGATE:
                this.Investigate();
                break;
            case EnemyStates.ATTACK:
                this.Attack();
                break;
            default: break;
        }
    }

    private void Idle()
    {
        //Action: Do nothing
        this.currentIdleTime += Time.deltaTime;

        //Transition to chase
        if (this.sight.IsTargetInSight()) //|| hearing.IsHearingTarget()
        {
            this.currentIdleTime = 0;
            this.ChangeState(EnemyStates.CHASE);
            return;
        }

        //Transition - Patrol
        if (this.currentIdleTime > this.maxIdleTime)
        {
            this.currentIdleTime = 0;
            ChangeState(EnemyStates.PATROL);
        }
    }


    private void Patrol()
    {
        //Transition to chase
        if (sight.IsTargetInSight())    // || hearing.IsHearingTarget()
        {
            ChangeState(EnemyStates.CHASE);
        }
    }

    private void Chase()
    {
        //Transition to Investigate
        if (!sight.IsTargetInSight()) // && !hearing.IsHearingTarget()
        {
            this.IsInvestigating = true;
            ChangeState(EnemyStates.INVESTIGATE);
        }

        //Transition to attack
        if (Vector3.Distance(target.transform.position, this.transform.position) <= attackDistance)
        {
            ChangeState(EnemyStates.ATTACK);
        }
    }


    private void Investigate()
    {
        if (sight.IsTargetInSight())
        {
            this.IsInvestigating = false;
            ChangeState(EnemyStates.CHASE);
        }
        if(!isInvestigating) {            
            ChangeState(EnemyStates.PATROL);
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(target.transform.position, this.transform.position) > attackDistance)
        {
            ChangeState(EnemyStates.CHASE);
        }

        if (!sight.IsTargetInSight())
        {
            this.IsInvestigating = true;
            ChangeState(EnemyStates.INVESTIGATE);
        }
    }

    public void ChangeState(EnemyStates newState)
    {
        CurrentState = newState;
    }
}
