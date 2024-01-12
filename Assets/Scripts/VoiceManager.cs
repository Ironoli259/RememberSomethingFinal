using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    private void Start()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(int index)
    {
        if(index < 0 || index >= audioClips.Count)
        {
            Debug.Log("Invalid audio clip index - Wife");
            return;
        }

        //Stop current audio clip - preventative
        audioSource.Stop();

        //Assign the clip
        audioSource.clip = audioClips[index];

        //Play the clip
        audioSource.Play();
    }
}
