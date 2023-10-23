using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (SceneManagement.Instance.SceneTransitionName == transitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
            CameraController.Instance.SetCameraFollow();
            UiFade.Instance.StartFadeToClear();
        }
    }
}
