using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollision : MonoBehaviour
{
    public delegate void OnLevelCollision();
    static public OnLevelCollision onPlayerTouch;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(onPlayerTouch != null)
            {
                onPlayerTouch();
            }
        }
    }
}
