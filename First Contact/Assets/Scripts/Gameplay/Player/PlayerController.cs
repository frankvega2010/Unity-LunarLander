using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject particlesGameObject;
    public float gas;

    private PlayerMovement playerMovement;
    private ParticleSystem playerParticles;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerParticles = particlesGameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isMoving)
        {
            gas = gas - 0.1f;
            if(gas <= 0)
            {
                playerMovement.isMoving = false;
                gas = 0;
            }
            playerParticles.Play();
        }
        else
        {
            playerParticles.Stop();
        }

        if(gas <= 0)
        {
            playerMovement.canUseBoost = false;
        }
    }
}
