using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    public float speed;

    private float timeBackground;
    private MeshRenderer backgroundRenderer;
    private void Start()
    {
        backgroundRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        timeBackground += Time.deltaTime;
        backgroundRenderer.material.mainTextureOffset = new Vector2(0, timeBackground * speed);
    }
}
