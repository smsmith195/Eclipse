using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransistionName { get; private set; }

    public void SetTransistionName(string sceneTransistionName)
    {
        this.SceneTransistionName = sceneTransistionName;
    }
}
