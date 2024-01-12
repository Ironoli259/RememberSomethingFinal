using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    public static ArcadeManager instance;
    public bool basketballGamePlayed;
    public bool racingGamePlayed;
    public float timeAfterMinigame = 300f;
    public int currentMemory = 0;

    public Vector3 lastPlayerPos;
    private VoiceManager wifeVoices;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            basketballGamePlayed = false;
            racingGamePlayed = false;
            DontDestroyOnLoad(gameObject);
            Debug.Log("ArcadeManager Awake");
        }
        else if(instance != this)
        {
            Debug.Log("Arcade Else IF");
            //instance.basketballGamePlayed = basketballGamePlayed;
            //instance.racingGamePlayed = racingGamePlayed;
            PlayerControl.instance.transform.position = lastPlayerPos;
            Timer.instance.FindNewTimerText();
            Destroy(gameObject);            
        }
        else
        {
            Debug.Log("Arcade Else");            
            PlayerControl.instance.transform.position = lastPlayerPos;
            Timer.instance.FindNewTimerText();
        }
        Debug.Log("ArcadeManager Loaded");
        wifeVoices = FindObjectOfType<VoiceManager>();

        //Going back to Maze
        if (GameManager.instance.currentSceneIndex < 3)
        {
            Debug.Log("ArcadeManager Destroyed");
            instance = null;
            Destroy(gameObject);
        }
    }

    public void UnlockMemory(int memory)
    {
        switch (memory)
        {
            case 1:
                wifeVoices.PlayAudioClip(0);
                break;            
            case 4:                
                StartCoroutine(PlayAudioAndWaitForEnd());                
                break;
            default:
                break;
        }
    }

    private IEnumerator PlayAudioAndWaitForEnd()
    {
        wifeVoices.PlayAudioClip(1);
        yield return new WaitForSeconds(wifeVoices.audioSource.clip.length);
        GameManager.instance.FinishedLevel(4);
    }

}
