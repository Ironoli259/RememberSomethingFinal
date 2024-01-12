using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    public static VillageManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this) 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Play initial clip
    }

    public void UnlockMemory(int memory)
    {
        switch (memory)
        {
            case 1:
                Narration.instance.SetNarrationText("That chapel where we prayed for our survival", 10f);
                //Play Clip                
                break;
            case 2:                
                Narration.instance.SetNarrationText("This small room where we set camp for the night", 10f);
                //Play clip                
                break;
            case 3:
                //Blackout Screen
                //Play Clip
                GameManager.instance.FinishedLevel(3);
                break;
            default:
                break;
        }
    }
}
