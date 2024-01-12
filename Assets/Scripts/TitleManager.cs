using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] int buildIndex;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LevelLoader.instance.LoadNextLevel(buildIndex , "After a terrible accident, you lost memories. Your therapist hypnotizes you and transports you into your mind.\nFind your way to those memories\nW,A,S,D to move\nHold LeftSHIFT to run\nSPACEBAR to jump\nLeftCRTL Toggles crouch", 16f);
        }
    }
}
