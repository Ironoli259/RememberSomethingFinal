using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [SerializeField] Animator transition;
    [SerializeField] TMP_Text loadingText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void LoadNextLevel(int buildIndex, string loadText, float delay)
    {        
        loadingText.text = loadText;

        StartCoroutine(LoadLevel(buildIndex, delay));
    }
    IEnumerator LoadLevel(int buildIndex, float delay)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildIndex);
        GameManager.instance.currentSceneIndex = buildIndex;
        if (buildIndex > 1)
        {
            yield return new WaitForSeconds(delay);
            GameManager.instance.SetState(GameStates.ACTIVE);
        }
    }
}
