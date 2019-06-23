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
    private void Start()
    {
        resultText = UIEndRound.GetComponent<UIDisplayResult>();
        PlayerController.onSuccesfullLanding += OnPlayerFinish;
        PlayerController.onFailedLanding += OnPlayerFinish;
        UIDisplayCheatScreen.OnCheatPassLevel += CheatPassLevel;
        endRoundOnce = false;
    }

    // Update is called once per frame
    private void Update()
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

    private void OnPlayerFinish(string action)
    {
        if(!endRoundOnce)
        {
            switch (action)
            {
                case "win":
                    Debug.Log("Successfull Landing");
                    UIEndRound.SetActive(true);
                    resultText.UpdateText("SUCCESSFULL LANDING!", Color.green);
                    isPlayerDead = false;
                    isRoundFinished = true;
                    CurrentSessionStats.Get().level++;
                    break;
                case "loss":
                    Debug.Log("Failed Landing");
                    UIEndRound.SetActive(true);
                    resultText.UpdateText("FAILED LANDING", Color.red);
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
        OnPlayerFinish("win");
    }

    private void OnDestroy()
    {
        PlayerController.onSuccesfullLanding -= OnPlayerFinish;
        PlayerController.onFailedLanding -= OnPlayerFinish;
        UIDisplayCheatScreen.OnCheatPassLevel -= CheatPassLevel;
    }
}
