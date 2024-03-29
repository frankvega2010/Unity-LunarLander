﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public string statusName;
    public float statusValue;

    private Text statusText;
    // Start is called before the first frame update
    private void Start()
    {
        statusText = GetComponent<Text>();        
    }

    // Update is called once per frame
    private void Update()
    {
        statusText.text = statusName + "  " + statusValue.ToString("0000");
    }
}
