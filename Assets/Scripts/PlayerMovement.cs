using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerSoundScript pss;
    private bool readyToJump;
    private bool jumpedOnce;
    private bool dead;
    private bool run;
    private int lives;
    //[SerializeField] private Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readyToJump = true;
        jumpedOnce = false;
        dead = false;
        lives = 3;
        run = false;
    }

    void Update()
    {
        //Move
        if (!dead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if (dead)
        {
            horizontal = 0;
            rb.bodyType = RigidbodyType2D.Static;
        }
        vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        {
            //rb.AddForce(new Vector2(0, jumpForce * 10)/*, ForceMode2D.Impulse*/);
            if (isGrounded())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumpedOnce = true;

            }
            else if (!isGrounded())
            {
                if (jumpedOnce)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    readyToJump = false;
                }
            }
        }

        //Idle Anim
        if (!Mathf.Approximately(horizontal, 0))
        {
            anim.SetFloat("lastMoveX", horizontal);
        }

        //Move Anim
        if (horizontal > 0)
        {
            if (isGrounded())
            {
                anim.SetBool("run", true);
                anim.SetBool("runl", false);
                anim.SetBool("jump_mid", false);
                run = true;
            }
            else if (!isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("runl", false);
                anim.SetBool("jump_mid", true);
                run = false;
            }
        }
        else
        {
            anim.SetBool("run", false);
            if (isGrounded())
            {
                anim.SetBool("jump_mid", false);
            }
            else if (!isGrounded() && anim.GetFloat("lastMoveX") > 0)
            {
                anim.SetBool("jump_mid", true);
            }
            run = false;
        }

        if (horizontal < 0)
        {
            if (isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("runl", true);
                anim.SetBool("jump_midl", false);
                run = true;
            }
            else if (!isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("runl", false);
                anim.SetBool("jump_midl", true);
                run = false;
            }
        }
        else
        {
            anim.SetBool("runl", false);
            if (isGrounded())
            {
                anim.SetBool("jump_midl", false);
            }
            else if (!isGrounded() && anim.GetFloat("lastMoveX") < 0)
            {
                anim.SetBool("jump_midl", true);
            }
            run = false;
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public int GetLives()
    {
        return lives;
    }
     public bool GetReady()
    {
        return readyToJump;
    }

    public bool isRunning()
    {
        return run;
    }
    //Anim Invoke
    private void HitRight()
    {
        anim.SetBool("hit", false);
    }

    private void HitLeft()
    {
        anim.SetBool("hitl", false);
    }

    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            readyToJump = true;
            jumpedOnce = false;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (lives > 1)
            {
                lives -= 1;
                if (anim.GetFloat("lastMoveX") < 0)
                {
                    anim.SetBool("hitl", true);
                    Invoke("HitLeft", 0.5f);
                }
                else if (anim.GetFloat("lastMoveX") > 0)
                {
                    anim.SetBool("hit", true);
                    Invoke("HitRight", 0.5f);
                }
            }else if(lives == 1)
            {
                lives -= 1;
                Die();
            }
        }
    }

    private void Die()
    {
        //rb.bodyType = RigidbodyType2D.Static;
        dead = true;
        if (anim.GetFloat("lastMoveX") < 0)
        {
            //anim.SetBool("deathl", true);
            anim.SetTrigger("killl");
        }
        else if (anim.GetFloat("lastMoveX") > 0)
        {
            //anim.SetBool("death", true);
            anim.SetTrigger("kill");
        }
    }
}
