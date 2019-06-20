using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UIFuel;

    private PlayerController playerStatus;
    private UIStatus fuel;
    // Start is called before the first frame update
    void Start()
    {
        fuel = UIFuel.GetComponent<UIStatus>();
        playerStatus = player.GetComponent<PlayerController>();

        fuel.statusName = "FUEL";
        fuel.statusValue = playerStatus.fuel;
    }

    // Update is called once per frame
    void Update()
    {
        fuel.statusValue = playerStatus.fuel;
    }
}
