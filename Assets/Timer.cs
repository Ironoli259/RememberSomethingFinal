using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public static float timeLimit = 300f; // The time limit for the timer in seconds
    private TMP_Text timeText; // The UI text element that displays the time

    public float timeLeft; // The amount of time left in the timer
    private bool isTimerRunning = false; // Whether the timer is currently running

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Awake Timer");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            instance.timeLeft = Mathf.Max(instance.timeLeft, timeLeft);
            Destroy(gameObject);
        }
        //Going back to Maze
        if (GameManager.instance.currentSceneIndex < 3)
        {
            Debug.Log("Timer Destroyed");
            instance = null;
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timeText = GameObject.FindWithTag("TimerText").GetComponent<TMP_Text>();
        if (timeLeft <=0)
            timeLeft = timeLimit; // Set the initial time left to the time limit
        UpdateTimerText(); // Update the UI text element with the initial time left
        if (GameManager.instance.currentSceneIndex >= 3)
            StartTimer();
        //Test
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeLeft -= Time.deltaTime; // Subtract the amount of time that has passed since the last frame
            if (timeLeft <= 0.0f)
            {
                timeLeft = 0.0f; // Clamp the time left to zero
                StopTimer(); // Stop the timer
                PlayerControl.playerState = PlayerState.DEAD;
                ResetTimer();
            }
            UpdateTimerText(); // Update the UI text element with the new time left
        }
    }

    public void FindNewTimerText()
    {
        timeText = GameObject.FindWithTag("TimerText").GetComponent<TMP_Text>();
        UpdateTimerText();
    }

    public void StartTimer()
    {
        isTimerRunning = true; // Start the timer
    }

    public void StopTimer()
    {
        isTimerRunning = false; // Stop the timer
    }

    public void ResetTimer()
    {
        timeLeft = timeLimit; // Reset the time left to the time limit
        UpdateTimerText(); // Update the UI text element with the new time left
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60.0f); // Calculate the number of minutes left
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60.0f); // Calculate the number of seconds left
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds); // Format the time as "M:SS" and update the UI text element
    }
}

