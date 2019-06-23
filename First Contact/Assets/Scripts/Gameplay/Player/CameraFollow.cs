using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float maxPosition;
    public float minPosition;
    public float lerpPositionSpeed;
    public float lerpSizeSpeed;
    public float cameraMinSize;

    private Vector3 targetPos;
    private Vector3 cameraNewPos;
    private float targetPosX;
    private float targetPosY;
    private float cameraNewPosX;
    private float cameraNewPosY;
    private float timerX;
    private float timerY;
    private float sizeTimer;
    private int maxLerpSize;
    private bool isLerpXReady;
    private bool isLerpYReady;
    private bool canChangeToNewSize;
    public bool switchOnce;
    private Camera currentCamera;

    private void Start()
    {
        currentCamera = GetComponent<Camera>();
        sizeTimer = currentCamera.orthographicSize;
        maxLerpSize = (int)sizeTimer - 5;
    }

    // Update is called once per frame
    private void Update()
    {
        targetPos = Camera.main.WorldToViewportPoint(target.transform.position);
        targetPosX = targetPos.x;
        targetPosY = targetPos.y;
        cameraNewPos = Camera.main.ViewportToWorldPoint(targetPos);
        cameraNewPosX = cameraNewPos.x;
        cameraNewPosY = cameraNewPos.y;

        if (targetPosX > maxPosition || targetPosX < minPosition)
        {
            isLerpXReady = true;
        }

        if(targetPosY > maxPosition || targetPosY < minPosition)
        {
            isLerpYReady = true;

            if (!switchOnce && targetPosY < minPosition)
            {
                isLerpXReady = true;
                canChangeToNewSize = true;
                switchOnce = true;
            }
        }

        if(isLerpXReady)
        {
            timerX += Time.deltaTime * lerpPositionSpeed;
            transform.position = Vector3.Lerp(transform.position, new Vector3(cameraNewPosX, transform.position.y, transform.position.z), timerX);
        }

        if (isLerpYReady)
        {
            timerY += Time.deltaTime * lerpPositionSpeed;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,  cameraNewPosY, transform.position.z), timerY);
        }

        if (canChangeToNewSize)
        {
            sizeTimer -= Time.deltaTime * lerpSizeSpeed;
            if (sizeTimer <= maxLerpSize)
            {
                sizeTimer = currentCamera.orthographicSize;
                canChangeToNewSize = false;
                if (maxLerpSize > cameraMinSize)
                {
                    maxLerpSize = (int)sizeTimer - 5;
                }
            }
            currentCamera.orthographicSize = sizeTimer;
        }

        if (timerX >= 1)
        {
            timerX = 0;
            isLerpXReady = false;
        }

        if (timerY >= 1)
        {
            timerY = 0;
            isLerpYReady = false;
            switchOnce = false;
        }
    }
}
