using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject checkpointPrefab;
    public List<Transform> checkpointPositions;
    private int currentCheckpoint = 0;
    private GameObject currentCheckpointObject;
    [SerializeField] private VoiceManager voiceManager;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;

    private void Start()
    {
        SpawnNextCheckpoint();
        Timer.instance.FindNewTimerText();
    }

    public void CheckpointReached()
    {
        Destroy(currentCheckpointObject);

        currentCheckpoint++;
        if (currentCheckpoint < checkpointPositions.Count)
        {
            voiceManager.PlayAudioClip(currentCheckpoint - 1);
            if (currentCheckpoint == 1)
                text1.SetActive(true);
            else
                text2.SetActive(true);
            SpawnNextCheckpoint();            
        }
        else
        {
            Debug.Log("All checkpoints reached");
            //Play Clip
            StartCoroutine(PlayAudioAndWaitForEnd());
        }
    }

    private void SpawnNextCheckpoint()
    {        
        currentCheckpointObject = Instantiate(checkpointPrefab, checkpointPositions[currentCheckpoint].position, checkpointPositions[currentCheckpoint].rotation);
        currentCheckpointObject.GetComponentInChildren<CheckpointTrigger>().checkpointManager = this;
    }

    private IEnumerator PlayAudioAndWaitForEnd()
    {
        voiceManager.PlayAudioClip(2);
        yield return new WaitForSeconds(voiceManager.audioClips[2].length);
        voiceManager.PlayAudioClip(3);
        yield return new WaitForSeconds(voiceManager.audioClips[3].length);
        ArcadeManager.instance.racingGamePlayed = true;
        ArcadeManager.instance.timeAfterMinigame = Timer.instance.timeLeft;
        LevelLoader.instance.LoadNextLevel(4, "And I'm the one who killed them both", 3);
    }
}
