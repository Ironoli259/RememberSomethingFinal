using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public enum GameStates
{
    ACTIVE,
    INACTIVE,
    PAUSED
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  
    public int currentSceneIndex;
    public int lastAliveSceneIndex;
    string transitionMessage;
    //Memories
    public bool playedMemFriend = false;
    public bool playedMemWife = false;

    public GameStates gameState = GameStates.ACTIVE;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        if (PlayerControl.playerState == PlayerState.DEAD)
        {
            SetState(GameStates.INACTIVE);
            lastAliveSceneIndex = currentSceneIndex;
            LevelLoader.instance.LoadNextLevel(1, "You were unable to remember your past...", 3f);
        }
        switch (gameState)
        {
            case GameStates.ACTIVE:
                if (PlayerControl.playerState == PlayerState.INACTIVE)
                {
                    PlayerControl.playerState = PlayerState.ALIVE;
                    Debug.Log("Enabling inputs");
                    if (InputManager.instance)
                    InputManager.instance.input.Enable();
                }
                break;
            case GameStates.INACTIVE:
                if (PlayerControl.playerState == PlayerState.ALIVE)
                {
                    PlayerControl.playerState = PlayerState.INACTIVE;
                }
                break;
            case GameStates.PAUSED:
                if (PlayerControl.playerState == PlayerState.ALIVE)
                {

                    PlayerControl.playerState = PlayerState.INACTIVE;
                    Debug.Log("Disabling inputs");                    
                    if (InputManager.instance)
                        InputManager.instance.input.Disable();
                }
                break;
        }

    }
    //Method called when level is completed
    public void FinishedLevel(int level)
    {
        switch (level)
        {
            case 3:
                playedMemFriend = true;                
                //Load Maze scene
                transitionMessage = "-Memory Unlocked-\n\nYou start to remember! Your best friend died killed in anambush by the enemy soldiers and you barely escaped it...";
                if(!playedMemWife)
                    LevelLoader.instance.LoadNextLevel(2, transitionMessage, 10f);
                else
                    LevelLoader.instance.LoadNextLevel(7, transitionMessage, 10f);
                //Block Village teleport
                break;
            case 4:
                playedMemWife = true;                
                //Load Maze scene
                transitionMessage = "-Memory Unlocked-\n\nAfter getting those memories back, you remember what happened when driving her back home. She did die in that tragic car accident that day...";
                if (!playedMemFriend)
                    LevelLoader.instance.LoadNextLevel(2, transitionMessage, 10f);
                else
                    LevelLoader.instance.LoadNextLevel(7, transitionMessage, 10f);
                //Block Arcade Teleport
                break;
            default:                
                break;
        }
    }

    
    public void SetState(GameStates state)
    {
        gameState = state;
    }
}
