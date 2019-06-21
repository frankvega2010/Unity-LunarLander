using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject particlesGameObject;
    public float crashSpeed;
    public float fakeMultiplierSpeed;
    public float fuel;
    public int score;

    public LayerMask rayCastLayer;
    public float rayDistance;

    private PlayerMovement playerMovement;
    private ParticleSystem playerParticles;
    private Rigidbody2D playerRigidbody;
    private bool isPlayerOnGround;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerParticles = particlesGameObject.GetComponent<ParticleSystem>();
        LevelCollision.onPlayerTouch += checkCollision;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * -1, rayDistance,rayCastLayer);

        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.up * -1 * rayDistance, Color.white);
            string layerHitted = LayerMask.LayerToName(hit.collider.gameObject.layer);

            switch (layerHitted)
            {
                case "floor":
                    Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
                    Debug.DrawRay(transform.position, transform.up * -1 * hit.distance, Color.yellow);
                    isPlayerOnGround = true;
                    break;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * -1 * rayDistance, Color.white);
        }


        if (playerMovement.isMoving)
        {
            fuel = fuel - 0.1f;
            if(fuel <= 0)
            {
                playerMovement.isMoving = false;
                fuel = 0;
            }
            playerParticles.Play();
        }
        else
        {
            playerParticles.Stop();
        }

        if(fuel <= 0)
        {
            playerMovement.canUseBoost = false;
        }
    }

    private void checkCollision()
    {
        float localRotZ = (transform.localEulerAngles.z + 360) % 360;

        if (localRotZ > 20 && localRotZ < 335 || localRotZ > 25 && localRotZ < 340)
        {
                Debug.Log("Failed Landing");
        }
        else
        {
            if (playerRigidbody.velocity.y * fakeMultiplierSpeed < -crashSpeed || !isPlayerOnGround)
            {
                Debug.Log("Failed Landing");
            }
            else
            {
                Debug.Log("Succesfull Landing");
                isPlayerOnGround = false;
            }
        }
        
    }

    private void OnDestroy()
    {
        LevelCollision.onPlayerTouch -= checkCollision;
    }
}
