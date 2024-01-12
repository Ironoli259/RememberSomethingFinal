using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private void Start()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
    }
}
