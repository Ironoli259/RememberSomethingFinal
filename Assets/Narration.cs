using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Narration : MonoBehaviour
{
    public static Narration instance;
    [SerializeField] TMP_Text textNarration;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetNarrationText(string narration, float time)
    {
        textNarration.text = narration;
        StartCoroutine(NarrationDisplay(time));
    }

    IEnumerator NarrationDisplay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textNarration.text = "";
    }
}
