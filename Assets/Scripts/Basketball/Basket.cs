using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameObject score; //reference to the ScoreText gameobject, set in editor 
    [SerializeField] private GameObject timerText; //reference to the TimeText gameobject, set in editor 
    [SerializeField] private AudioClip basket; //reference to the basket sound 
    [SerializeField] private VoiceManager wifeVoices;

    private AudioSource audioSource;
    private int currentTime;

    private void Start()
    {
        currentTime = (int)Timer.instance.timeLeft;
        timerText.GetComponent<TextMeshPro>().text = FormatTime(currentTime);
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TimerCountdown());
    }

    void OnTriggerEnter(Collider other) //if ball hits basket collider 
    {
        int currentScore = int.Parse(score.GetComponent<TextMeshPro>().text) + 1; //add 1 to the score 
        score.GetComponent<TextMeshPro>().text = currentScore.ToString();
        audioSource.Play();
        if(currentScore == 2)
        {
            wifeVoices.PlayAudioClip(0);
        }
        else if(currentScore == 5) 
        {
            wifeVoices.PlayAudioClip(1);
        }
        else if(currentScore == 8) 
        {
            wifeVoices.PlayAudioClip(2);        
        }
        if(currentScore > 9)
        {
            ArcadeManager.instance.basketballGamePlayed = true;
            LevelLoader.instance.LoadNextLevel(4, "Memory Unlocked", 2);
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            timerText.GetComponent<TextMeshPro>().text = FormatTime(currentTime);
        }
    }

    private string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        seconds %= 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
