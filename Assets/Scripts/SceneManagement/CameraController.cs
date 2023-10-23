using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : SingletonMono<CameraController>
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    public void SetCameraFollow()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.Follow = PlayerController.Instance.transform; 
    }
}
