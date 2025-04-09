using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string[] levelScenes; // Array of scene names for different levels

    public void PlayGame(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelScenes.Length)
        {
            SceneManager.LoadScene(levelScenes[levelIndex]);
        }
        else
        {
            Debug.LogError("Invalid level index!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
