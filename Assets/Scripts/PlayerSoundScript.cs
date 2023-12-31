using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource playerHit, playerDeath, playerRun, playerJump;
    private PlayerMovement pmove;
    private bool run;
    [SerializeField] private Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        pmove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pmove.GetReady())
        {
            playerJump.Play();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && pmove.isGrounded() || joystick.Horizontal != 0 && pmove.isGrounded())
        {
            run = true;
        }
        else
        {
            run = false;
        }

        if (run && !playerRun.isPlaying)
        {
            playerRun.Play();
        }
        else if(!run)
        {
            playerRun.Stop();
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


    public void JumpButtonSound()
    {
        if (pmove.GetReady())
        {
            playerJump.Play();
        }
    }

}
