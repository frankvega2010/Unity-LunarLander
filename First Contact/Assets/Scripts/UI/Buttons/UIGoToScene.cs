using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGoToScene : MonoBehaviour
{
    public string sceneName;
    public bool isLevelRestartButton;

    public void GoToThisScene()
    {
        switch (sceneName)
        {
            case "Level":
                LoaderManager.Get().LoadScene(sceneName);
                UILoadingScreen.Get().SetVisible(true);
                break;
            default:
                SceneManager.LoadScene(sceneName);
                Time.timeScale = 1;
                break;
        }
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif
    }
}
