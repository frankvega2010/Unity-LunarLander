using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayResult : MonoBehaviour
{
    public Text resultText;
    public Text scoreText;

    public void UpdateText(string newResultText,Color resultTextColor)
    {
        resultText.text = newResultText;
        resultText.color = resultTextColor;
        scoreText.text = "YOUR SCORE: " + CurrentSessionStats.Get().score.ToString();
    }
}
