using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabasePanel : MonoBehaviour
{
    [Header("Panels")]
    public GameObject initialPanel;
    public GameObject registerLoginPanel;

    [Header("Text")]
    public Text titleText;
    public Text infoText;

    [Header("Buttons")]
    public Button registerButton;
    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        initialPanel.SetActive(true);
        registerLoginPanel.SetActive(false);
        infoText.text = "";
    }

    public void activateLoginPanel()
    {
        infoText.text = "";
        registerLoginPanel.SetActive(true);
        loginButton.gameObject.SetActive(true);
        registerButton.gameObject.SetActive(false);

        titleText.text = "LOG IN";

        initialPanel.SetActive(false);
    }

    public void activateRegisterPanel()
    {
        infoText.text = "";
        initialPanel.SetActive(false);
        registerLoginPanel.SetActive(true);
        loginButton.gameObject.SetActive(false);
        registerButton.gameObject.SetActive(true);

        titleText.text = "REGISTRATION";
    }

    public void activateInitialPanel()
    {
        infoText.text = "";
        initialPanel.SetActive(true);
        registerLoginPanel.SetActive(false);
    }
}
