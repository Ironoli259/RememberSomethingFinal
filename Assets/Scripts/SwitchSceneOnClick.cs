using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchSceneOnClick : MonoBehaviour
{
    [SerializeField] private int sceneToLoadIndex;
    [SerializeField] private float interactionRange = 3f;

    public void OnMouseDown()
    {
        Debug.Log("Button Click");
        float distance = Vector3.Distance(transform.position, PlayerControl.instance.transform.position);
        if (distance > interactionRange)
            return;
        ArcadeManager.instance.lastPlayerPos = PlayerControl.instance.transform.position;
        if (sceneToLoadIndex == 6)          //BBGame Game
        {
            if (ArcadeManager.instance.basketballGamePlayed)
                return;
            Debug.Log("Loading Level");
            ArcadeManager.instance.currentMemory = 2;
            LevelLoader.instance.LoadNextLevel(sceneToLoadIndex, "Thats right... In college I played on the varsity basketball team and that's where I met my beloved one.\n\n-Click on the basketball rack to pick a ball and click again to shoot and achieve a score of 10-", 12f);
        }
        else if (sceneToLoadIndex == 5)     //Racing Game
        {
            if (ArcadeManager.instance.racingGamePlayed)
                return;
            Debug.Log("Loading Level");
            ArcadeManager.instance.currentMemory = 3;
            LevelLoader.instance.LoadNextLevel(sceneToLoadIndex, "Use W,A,S,D keys to steer and accelerate", 5f);
        }
    }
}
