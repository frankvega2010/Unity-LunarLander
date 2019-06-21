using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void onPlayerAction(string action);
    public static onPlayerAction onSuccesfullLanding;
    public static onPlayerAction onFailedLanding;

    public GameObject particlesGameObject;
    public GameObject timerUI;
    public float crashSpeed;
    public float fakeMultiplierSpeed;
    public float fuel;
    public int score;

    public LayerMask rayCastLayer;
    public float rayDistance;

    private UIDisplayTime UITimer;
    private PlayerMovement playerMovement;
    private ParticleSystem playerParticles;
    private Rigidbody2D playerRigidbody;
    private bool isPlayerOnGround;
    // Start is called before the first frame update
    void Start()
    {
        score = CurrentSessionStats.Get().score;
        UITimer = timerUI.GetComponent<UIDisplayTime>();
        playerMovement = GetComponent<PlayerMovement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerParticles = particlesGameObject.GetComponent<ParticleSystem>();
        LevelCollision.onPlayerTouch += checkCollision;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * -1, rayDistance, rayCastLayer);

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
            if (fuel <= 0)
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

        if (fuel <= 0)
        {
            playerMovement.canUseBoost = false;
        }
    }

    private void checkCollision()
    {
        float localRotZ = (transform.localEulerAngles.z + 360) % 360;

        if (localRotZ > 20 && localRotZ < 335 || localRotZ > 25 && localRotZ < 340)
        {
            if (onFailedLanding != null)
            {
                GiveScore(false);
                onFailedLanding("loss");
            }
        }
        else
        {
            if (playerRigidbody.velocity.y * fakeMultiplierSpeed < -crashSpeed || !isPlayerOnGround)
            {
                if (onFailedLanding != null)
                {
                    GiveScore(false);
                    onFailedLanding("loss");
                }
            }
            else
            {
                isPlayerOnGround = false;
                if (onSuccesfullLanding != null)
                {
                    GiveScore(true);
                    onSuccesfullLanding("win");
                }
            }
        }

    }

    private void GiveScore(bool isPlayerAlive)
    {
        if (isPlayerAlive)
        {
            score = score + 1000 + ((Mathf.FloorToInt(fuel)/5) * 5) - (Mathf.FloorToInt(UITimer.timer)*5);
        }
        else
        {
            score = score + 0;
        }

        CurrentSessionStats.Get().score = score;
    }

    private void OnDestroy()
    {
        LevelCollision.onPlayerTouch -= checkCollision;
    }
}
