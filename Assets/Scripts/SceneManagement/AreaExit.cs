using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private string sceneTransitionName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UiFade.Instance.StartFadeToBlack(() => SceneManager.LoadScene(nextScene));
        }
    }
    
}
