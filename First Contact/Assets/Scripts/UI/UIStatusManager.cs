﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UIFuel;
    public GameObject UIScore;
    public GameObject UIAltitude;
    public GameObject UIHorizontalSpeed;
    public GameObject UIVerticalSpeed;

    private PlayerController playerStatus;
    private UIStatus fuel;
    private UIStatus score;
    private UIStatus altitude;
    private UIStatus horizontalSpeed;
    private UIStatus verticalSpeed;

    private Rigidbody2D playerRigidbody;
    private float hSpeed;
    private float vSpeed;
    // Start is called before the first frame update
    void Start()
    {
        fuel = UIFuel.GetComponent<UIStatus>();
        score = UIScore.GetComponent<UIStatus>();
        altitude = UIAltitude.GetComponent<UIStatus>();
        horizontalSpeed = UIHorizontalSpeed.GetComponent<UIStatus>();
        verticalSpeed = UIVerticalSpeed.GetComponent<UIStatus>();
        playerStatus = player.GetComponent<PlayerController>();

        fuel.statusName = "FUEL";
        score.statusName = "SCORE";
        altitude.statusName = "ALTITUDE";
        horizontalSpeed.statusName = "HORIZONTAL SPEED";
        verticalSpeed.statusName = "VERTICAL SPEED";

        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hSpeed = playerRigidbody.velocity.x;
        vSpeed = playerRigidbody.velocity.y;
        fuel.statusValue = playerStatus.fuel;
        score.statusValue = playerStatus.score;
        altitude.statusValue = player.transform.position.y*20;
        horizontalSpeed.statusValue = Mathf.Abs(hSpeed * 40);
        verticalSpeed.statusValue = Mathf.Abs(vSpeed * 40);
    }
}
