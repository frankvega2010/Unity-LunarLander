using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayTime : MonoBehaviour
{
    public Text timeText;
    public float timer;

    private string time;
    private int minutes;
    private int seconds;
    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = "TIME  " + time;
    }
}
