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
            case "Profile":
                GameObject target = GameObject.Find("DatabasePHP");
                target.GetComponent<DatabasePHP>().switchToProfile();
                break;
            case "Menu":
                GameObject target3 = GameObject.Find("ProfilePanel");
                target3.transform.SetParent(null,false);

                DontDestroyOnLoad(target3);

                GameObject target2 = GameObject.Find("DatabasePHP");
                target2.GetComponent<DatabasePHP>().infoProfileText.text = "";

                SceneManager.LoadScene(sceneName);
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
