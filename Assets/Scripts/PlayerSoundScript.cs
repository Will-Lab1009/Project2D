using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource playerHit, playerDeath, playerRun, playerJump;
    private PlayerMovement pmove;
    // Start is called before the first frame update
    void Start()
    {
        pmove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pmove.isRunning())
        {
            playerRun.Play();
        }
        else
        {
            playerRun.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space) && pmove.GetReady())
        {
            playerJump.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (pmove.GetLives() > 0)
            {
                playerHit.Play();
            }
            else if(pmove.GetLives() == 0)
            {
                playerDeath.Play();
            }
        }
    }
}
