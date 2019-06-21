using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isSecondaryPauseButton;

    private bool isGamePaused;
    private GameObject myEventSystem;
    private GameObject primaryPauseButton;
    private void Start()
    {
        if(isSecondaryPauseButton)
        {
            primaryPauseButton = GameObject.Find("Pause Button");
        }
        else
        {
            myEventSystem = GameObject.Find("EventSystem");
            isGamePaused = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!isSecondaryPauseButton)
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        if(!isSecondaryPauseButton)
        {
            isGamePaused = !isGamePaused;

            if (isGamePaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                GetComponent<Image>().color = Color.red;
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                GetComponent<Image>().color = Color.white;
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
        }
    }

    public void continueGame()
    {
        if (isSecondaryPauseButton)
        {
            primaryPauseButton.GetComponent<UIPauseButton>().pauseGame();
        }   
    }
}
