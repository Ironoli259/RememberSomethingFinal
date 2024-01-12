using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Canvas UICanvas;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        PlayerControl.playerState = PlayerState.INACTIVE;
    }
    public void OnRestartClick()
    {
        UICanvas.sortingOrder = 0;        
        GameManager.instance.playedMemFriend = false;
        GameManager.instance.playedMemWife = false;
        LevelLoader.instance.LoadNextLevel(0, "Let's restart the adventure!", 3f);
        PlayerControl.playerState = PlayerState.ALIVE;
        InputManager.instance.input.Enable();
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
