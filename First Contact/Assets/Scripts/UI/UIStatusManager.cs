using System.Collections;
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
    public GameObject UICurrentLevel;
    public float fakeMultiplierSpeed;
    public float fakeMultiplierAltitude;

    private PlayerController playerStatus;
    private UIStatus fuel;
    private UIStatus score;
    private UIStatus altitude;
    private UIStatus horizontalSpeed;
    private UIStatus verticalSpeed;
    private UIStatus currentLevel;

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
        currentLevel = UICurrentLevel.GetComponent<UIStatus>();
        playerStatus = player.GetComponent<PlayerController>();

        fuel.statusName = "FUEL";
        score.statusName = "SCORE";
        altitude.statusName = "ALTITUDE";
        horizontalSpeed.statusName = "HORIZONTAL SPEED";
        verticalSpeed.statusName = "VERTICAL SPEED";
        currentLevel.statusName = "LEVEL";

        playerRigidbody = player.GetComponent<Rigidbody2D>();
        currentLevel.statusValue = CurrentSessionStats.Get().level;
    }

    // Update is called once per frame
    void Update()
    {
        hSpeed = playerRigidbody.velocity.x * fakeMultiplierSpeed;
        vSpeed = playerRigidbody.velocity.y * fakeMultiplierSpeed;
        fuel.statusValue = playerStatus.fuel;
        score.statusValue = playerStatus.score;
        altitude.statusValue = player.transform.position.y* fakeMultiplierAltitude;
        horizontalSpeed.statusValue = Mathf.Abs(hSpeed);
        verticalSpeed.statusValue = Mathf.Abs(vSpeed);
    }
}
