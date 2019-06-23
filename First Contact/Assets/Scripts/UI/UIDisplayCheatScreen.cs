using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayCheatScreen : MonoBehaviour
{
    public delegate void OnCheatSelected();
    public static OnCheatSelected OnCheatFuel;
    public static OnCheatSelected OnCheatPassLevel;
    public static OnCheatSelected OnCheatSwitchWindZones;

    public GameObject cheatsScreen;

    private bool isScreenActivated;
    // Start is called before the first frame update
    private void Start()
    {
        isScreenActivated = false;
        cheatsScreen.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCheatScreen();
        }

        if(isScreenActivated)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(OnCheatFuel != null)
                {
                    OnCheatFuel();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (OnCheatPassLevel != null)
                {
                    OnCheatPassLevel();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (OnCheatSwitchWindZones != null)
                {
                    OnCheatSwitchWindZones();
                }
            }
        }
    }

    private void SwitchCheatScreen()
    {
        isScreenActivated = !isScreenActivated;
        if (isScreenActivated)
        {
            cheatsScreen.SetActive(true);
        }
        else
        {
            cheatsScreen.SetActive(false);
        }
    }
}
