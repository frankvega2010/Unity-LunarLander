using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject UIEndRound;
    public float waitingTimeForNextScene;

    private bool isRoundFinished;
    private bool isPlayerDead;
    private bool endRoundOnce;
    private float nextSceneTimer;
    private UIDisplayResult resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultText = UIEndRound.GetComponent<UIDisplayResult>();
        PlayerController.onSuccesfullLanding += onPlayerFinish;
        PlayerController.onFailedLanding += onPlayerFinish;
        UIDisplayCheatScreen.OnCheatPassLevel += CheatPassLevel;
        endRoundOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRoundFinished)
        {
            nextSceneTimer += Time.deltaTime;

            if(nextSceneTimer >= waitingTimeForNextScene)
            {
                nextSceneTimer = 0;
                GoToNextScene();
                isRoundFinished = false;
            }
        }
    }

    private void onPlayerFinish(string action)
    {
        if(!endRoundOnce)
        {
            switch (action)
            {
                case "win":
                    Debug.Log("Successfull Landing");
                    UIEndRound.SetActive(true);
                    resultText.updateText("SUCCESSFULL LANDING!", Color.green);
                    isPlayerDead = false;
                    isRoundFinished = true;
                    CurrentSessionStats.Get().level++;
                    break;
                case "loss":
                    Debug.Log("Failed Landing");
                    UIEndRound.SetActive(true);
                    resultText.updateText("FAILED LANDING", Color.red);
                    isPlayerDead = true;
                    isRoundFinished = true;
                    CurrentSessionStats.Get().level = 1;
                    break;
                default:
                    break;
            }

            endRoundOnce = true;
        }
    }

    private void GoToNextScene()
    {
        if(isPlayerDead)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            LoaderManager.Get().LoadScene(SceneManager.GetActiveScene().name);
            UILoadingScreen.Get().SetVisible(true);
        }
    }

    private void CheatPassLevel()
    {
        onPlayerFinish("win");
    }

    private void OnDestroy()
    {
        PlayerController.onSuccesfullLanding -= onPlayerFinish;
        PlayerController.onFailedLanding -= onPlayerFinish;
        UIDisplayCheatScreen.OnCheatPassLevel -= CheatPassLevel;
    }
}
