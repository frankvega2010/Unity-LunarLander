using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGetHighscore : MonoBehaviour
{
    private Text highscoreText;

    // Start is called before the first frame update
    private void Start()
    {
        highscoreText = GetComponent<Text>();
        highscoreText.text = "HIGHSCORE: " + Highscore.Get().highscore.ToString();
    }
}
