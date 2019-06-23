using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float verticalForce;
    public float horizontalForce;
    public float gravity;
    public bool isMoving;
    public bool canUseBoost;

    private Rigidbody2D playerRig;
    // Start is called before the first frame update
    private void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
        playerRig.gravityScale = gravity;
        playerRig.AddForce(Vector3.right * 10, ForceMode2D.Impulse);
        canUseBoost = true;
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal");
        float verticalSpeed = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) || verticalSpeed > 0)
        {
            if(canUseBoost)
            {
                Debug.Log("is moving");
                playerRig.AddForce(transform.up * verticalForce, ForceMode2D.Force);
                isMoving = true;
            }
        }
        else
        {
            isMoving = false;
        }

        if(horizontalSpeed < 0)
        {
            playerRig.AddTorque(horizontalForce);
            CheckPlayerRotation(90, 265, 90);
        }
        else if (horizontalSpeed > 0)
        {
            playerRig.AddTorque(horizontalForce*-1);
            CheckPlayerRotation(95, 270, 270);
        }
        else
        {
            playerRig.angularVelocity = 0;
        }
    }

    private void CheckPlayerRotation(int angleRange1,int angleRange2,int newAngle)
    {
        float localRotZ = (transform.localEulerAngles.z + 360) % 360;
        if (localRotZ > angleRange1 && localRotZ < angleRange2)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newAngle);
            playerRig.angularVelocity = 0;
        }
    }
}
