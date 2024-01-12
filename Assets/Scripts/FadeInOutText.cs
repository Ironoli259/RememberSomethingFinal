using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeInOutText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text; // Replace with 'public TextMeshProUGUI text;' if using TextMeshPro
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float displayDuration = 5f;
    [SerializeField] private float fadeOutDuration = 1f;

    void OnEnable()
    {
        StartCoroutine(ShowAndHideText());
    }

    private IEnumerator ShowAndHideText()
    {
        CanvasGroup canvasGroup = text.GetComponent<CanvasGroup>();

        // Fade in
        float fadeInStartTime = Time.time;
        while (Time.time < fadeInStartTime + fadeInDuration)
        {
            float t = (Time.time - fadeInStartTime) / fadeInDuration;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        // Display for specified duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        float fadeOutStartTime = Time.time;
        while (Time.time < fadeOutStartTime + fadeOutDuration)
        {
            float t = (Time.time - fadeOutStartTime) / fadeOutDuration;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        // Hide completely
        canvasGroup.alpha = 0;
        this.gameObject.SetActive(false);
    }
}
