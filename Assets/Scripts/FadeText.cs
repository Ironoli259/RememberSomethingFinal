using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeDelay;
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(fadeDelay);
        Color color = text.color;
        while (true)
        {
            while (color.a > 0.01f)
            {
                color.a -= Time.deltaTime * fadeSpeed;
                text.color = color;
                yield return null;
            }
            while (color.a < 1f)
            {
                color.a += Time.deltaTime * fadeSpeed;
                text.color = color;
                yield return null;
            }
        }
    }
}
