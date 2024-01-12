using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HIghScoreManager : MonoBehaviour
{
    TMP_Text highScoreTextUI;
    int highScore = 0;
    void Start()
    {
        highScoreTextUI = GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreTextUI.text = highScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            highScore++;
            UpdateAndSaveUI(highScore);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            highScore--;
            UpdateAndSaveUI(highScore);
        }
    }
    public void UpdateAndSaveUI(int amount)
    {
        PlayerPrefs.SetInt("HighScore", amount);
        highScoreTextUI.text = amount.ToString();
    }
}
