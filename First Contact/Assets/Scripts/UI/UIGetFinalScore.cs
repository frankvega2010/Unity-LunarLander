using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGetFinalScore : MonoBehaviour
{
    private Text scoreText;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "FINAL SCORE: " + CurrentSessionStats.Get().score.ToString();
        CurrentSessionStats.Get().score = 0;
    }
}
