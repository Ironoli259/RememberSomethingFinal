using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerFct : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        if (text != null)
            text.text = health.ToString();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (text != null)
            text.text = health.ToString();

        if(health <= 0f)
        {
            Timer.instance.StopTimer(); // Stop the timer
            PlayerControl.playerState = PlayerState.DEAD;
            Timer.instance.ResetTimer();
        }
    }
}
