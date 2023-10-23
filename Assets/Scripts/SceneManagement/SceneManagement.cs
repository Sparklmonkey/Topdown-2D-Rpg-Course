using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : SingletonMono<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneName)
    {
        SceneTransitionName = sceneName;
    }
}
