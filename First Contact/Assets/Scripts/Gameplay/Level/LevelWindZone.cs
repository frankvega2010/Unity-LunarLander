using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWindZone : MonoBehaviour
{
    public int minWindTime;
    public int maxWindTime;
    public float minWindSpeed;
    public float maxWindSpeed;

    private int waitingTime;
    private int newWindTime;
    private float newWindSpeed;
    private float windTimer;
    private float waitingTimer;
    private AreaEffector2D effector;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<AreaEffector2D>();
        sprite = GetComponent<SpriteRenderer>();
        newWindTime = Random.Range(minWindTime,maxWindTime+1);
        newWindSpeed = Random.Range(minWindSpeed, maxWindSpeed);
        waitingTime = Random.Range(minWindTime, maxWindTime + 1);
        windTimer = 0;
        waitingTimer = -1;
        effector.enabled = true;
        sprite.material.color = Color.green;
        effector.forceMagnitude = newWindSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingTimer < 0)
        {
            windTimer += Time.deltaTime;
        }
        else
        {
            waitingTimer += Time.deltaTime;
        }
        

        if(windTimer >= newWindTime)
        {
            effector.enabled = false;
            sprite.material.color = Color.white;
            windTimer = 0;
            waitingTimer = 0;
            newWindTime = Random.Range(minWindTime, maxWindTime + 1);
            newWindSpeed = Random.Range(minWindSpeed, maxWindSpeed + 1);
            effector.forceMagnitude = newWindSpeed;
        }

        if(waitingTimer >= waitingTime)
        {
            effector.enabled = true;
            sprite.material.color = Color.green;
            windTimer = 0;
            waitingTimer = -1;
        }
    }
}
