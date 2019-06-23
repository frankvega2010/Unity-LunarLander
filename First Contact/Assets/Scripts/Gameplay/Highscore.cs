﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviourSingleton<Highscore>
{
    public int highscore;

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    public void updateHighscore(int newScore)
    {
        if(newScore >= highscore)
        {
            highscore = newScore;
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }
}