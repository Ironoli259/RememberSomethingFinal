using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    [SerializeField] private AudioClip[] fireClips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioClip randomFireClip = fireClips[Random.Range(0, fireClips.Length)];
            audioSource.PlayOneShot(randomFireClip);
        }
    }
}
