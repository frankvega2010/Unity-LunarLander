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
    public float crashSpeedX;
    public float fakeMultiplierSpeed;
    public float fuel;
    public int score;
    public bool[] arePlatformsOnGround;

    public LayerMask rayCastLayer;
    public float rayDistance;

    private UIDisplayTime UITimer;
    private PlayerMovement playerMovement;
    private ParticleSystem playerParticles;
    private Rigidbody2D playerRigidbody;
    
    // Start is called before the first frame update
    private void Start()
    {
        score = CurrentSessionStats.Get().score;
        UITimer = timerUI.GetComponent<UIDisplayTime>();
        playerMovement = GetComponent<PlayerMovement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerParticles = particlesGameObject.GetComponent<ParticleSystem>();
        LevelCollision.onPlayerTouch += CheckCollision;
        UIDisplayCheatScreen.OnCheatFuel += CheatAddFuel;
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(0.2f,0,0), transform.up * -1, rayDistance, rayCastLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0.2f, 0, 0), transform.up * -1, rayDistance, rayCastLayer);

        CheckRaycastCollision(hitLeft, 0, transform.position - new Vector3(0.2f, 0, 0));
        CheckRaycastCollision(hitRight, 1, transform.position + new Vector3(0.2f, 0, 0));

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

    private void CheckRaycastCollision(RaycastHit2D hit,int index,Vector3 position)
    {
        if (hit.collider != null)
        {
            Debug.DrawRay(position, transform.up * -1 * rayDistance, Color.white);
            string layerHitted = LayerMask.LayerToName(hit.collider.gameObject.layer);

            switch (layerHitted)
            {
                case "floor":
                    Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
                    Debug.DrawRay(transform.position, transform.up * -1 * hit.distance, Color.yellow);
                    arePlatformsOnGround[index] = true;
                    break;
            }
        }
        else
        {
            Debug.DrawRay(position, transform.up * -1 * rayDistance, Color.white);
            arePlatformsOnGround[index] = false;
        }
    }

    private void CheckCollision()
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
            if (playerRigidbody.velocity.y * fakeMultiplierSpeed < -crashSpeed || Mathf.Abs(playerRigidbody.velocity.x * fakeMultiplierSpeed) > crashSpeedX)
            {
                if (onFailedLanding != null)
                {
                    GiveScore(false);
                    onFailedLanding("loss");
                }
            }
            else
            {
                bool isFinalCollisionCorrect = false;
                int platformsOnGround = 0;

                for (int i = 0; i < arePlatformsOnGround.Length; i++)
                {
                    if (arePlatformsOnGround[i])
                    {
                        platformsOnGround++;
                    }
                }

                if(platformsOnGround >= arePlatformsOnGround.Length)
                {
                    isFinalCollisionCorrect = true;
                }

                if(isFinalCollisionCorrect)
                {
                    if (onSuccesfullLanding != null)
                    {
                        GiveScore(true);
                        onSuccesfullLanding("win");
                    }
                }
                else
                {
                    if (onFailedLanding != null)
                    {
                        GiveScore(false);
                        onFailedLanding("loss");
                    }
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
        Highscore.Get().UpdateHighscore(score);
    }

    private void CheatAddFuel()
    {
        fuel = fuel + 100;
    }

    private void OnDestroy()
    {
        LevelCollision.onPlayerTouch -= CheckCollision;
        UIDisplayCheatScreen.OnCheatFuel -= CheatAddFuel;
    }
}
