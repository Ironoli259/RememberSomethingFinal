using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveChecker : MonoBehaviour
{
    public List<Transform> targetObjects;
    private Transform player;
    [SerializeField] private float range = 4;
    private int memory;
    private int targetObjectsCount;

    public int Memory { get => memory; set => memory = value; }
    public int TargetObjectsCount { get => targetObjectsCount; set => targetObjectsCount = value; }

    private void Start()
    {         
        this.player = PlayerControl.instance.transform;
        if (GameManager.instance.currentSceneIndex == 4)
            this.Memory = ArcadeManager.instance.currentMemory;
        else
            this.Memory = 0;

        targetObjectsCount = targetObjects.Count;
    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Memory >= targetObjectsCount) return;

        float distance = Vector3.Distance(this.player.position, this.targetObjects[this.Memory].position);

        if (distance <= this.range)
        {
            Debug.Log("Player is within range of the target object");
            if(GameManager.instance.currentSceneIndex == 4)
            {
                if (Memory != 0 && Memory != 3)
                    return;

                this.Memory++;
                ArcadeManager.instance.currentMemory = this.Memory;
                ArcadeManager.instance.UnlockMemory(this.Memory);
            }
            else
            {
                this.Memory++;
                VillageManager.instance.UnlockMemory(this.Memory);
            }
        }
    }
}
