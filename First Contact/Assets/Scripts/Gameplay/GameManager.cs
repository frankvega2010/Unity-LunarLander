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
    private float nextSceneTimer;
    private UIDisplayResult resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultText = UIEndRound.GetComponent<UIDisplayResult>();
        PlayerController.onSuccesfullLanding += onPlayerFinish;
        PlayerController.onFailedLanding += onPlayerFinish;
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
        switch (action)
        {
            case "win":
                Debug.Log("Successfull Landing");
                UIEndRound.SetActive(true);
                resultText.updateText("SUCCESSFULL LANDING!",Color.green);
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
                CurrentSessionStats.Get().score = 0;
                break;
            default:
                break;
        }
    }

    private void GoToNextScene()
    {
        if(isPlayerDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // GO BACK TO MENU
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    private void OnDestroy()
    {
        PlayerController.onSuccesfullLanding -= onPlayerFinish;
        PlayerController.onFailedLanding -= onPlayerFinish;
    }
}
