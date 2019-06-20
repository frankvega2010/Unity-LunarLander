using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UIFuel;
    public GameObject UIScore;

    private PlayerController playerStatus;
    private UIStatus fuel;
    private UIStatus score;
    // Start is called before the first frame update
    void Start()
    {
        fuel = UIFuel.GetComponent<UIStatus>();
        score = UIScore.GetComponent<UIStatus>();
        playerStatus = player.GetComponent<PlayerController>();

        fuel.statusName = "FUEL";
        score.statusName = "SCORE";
    }

    // Update is called once per frame
    void Update()
    {
        fuel.statusValue = playerStatus.fuel;
        score.statusValue = playerStatus.score;
    }
}
