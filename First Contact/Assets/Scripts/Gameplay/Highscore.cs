﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviourSingleton<Highscore>
{
    public int highscore;

    // Start is called before the first frame update
    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    public void UpdateHighscore(int newScore)
    {
        GameObject database = GameObject.Find("DatabasePHP");
        DatabasePHP dbphp = database.GetComponent<DatabasePHP>();
        dbphp.loadpb();
        int playerpb = int.Parse(dbphp.scoreText.text);

        if (newScore >= highscore)
        {
            highscore = newScore;
            PlayerPrefs.SetInt("highscore", highscore);
        }

        if(newScore >= playerpb)
        {
            dbphp.score = newScore;
            dbphp.savepb();
        }
    }
}
