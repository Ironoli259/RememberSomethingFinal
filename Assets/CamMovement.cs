using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamMovement : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetCameraMovement(bool enable)
    {
        
    }
}
