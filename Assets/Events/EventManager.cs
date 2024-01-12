using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum GameModes
{
    menu, pause, exploration, story
}
public class EventManager : MonoBehaviour
{
    public static GameModes currentGameMode = GameModes.exploration;

    public static event Action<GameModes> onSwitchGameModeEvent;

    private void Awake()
    {
        onSwitchGameModeEvent += SwitchGameMode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            onSwitchGameModeEvent?.Invoke(GameModes.pause);
        }
    }
    void SwitchGameMode(GameModes gameMode)
    {
        currentGameMode = gameMode;
        Debug.Log("Switched to pause");
    }
    private void PauseGame()
    {
        
    }
}
