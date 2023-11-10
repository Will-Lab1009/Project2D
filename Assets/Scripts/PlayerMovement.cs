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
    private bool readyToJump;
    private bool jumpedOnce;
    //[SerializeField] private Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readyToJump = true;
        jumpedOnce = false;
    }

    void Update()
    {
        //Move
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

        rb.velocity = new Vector2(horizontal, vertical) * speed * Time.deltaTime;

        anim.SetFloat("moveX", rb.velocity.x);
        
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        {
            //rb.AddForce(new Vector2(0, jumpForce * 10)/*, ForceMode2D.Impulse*/);
            if (isGrounded())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                //jumpedOnce = true;

            }
            /*else if (!isGrounded())
            {
                if (jumpedOnce)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    readyToJump = false;
                }
            }*/
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
                anim.SetBool("jump_start", false);
                anim.SetBool("jump_mid", false);
            }
            /*else if (!isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("jump_start", true);
                Invoke("JumpAnimRight", 0.01f);
            }*/
        }
        else
        {
            anim.SetBool("run", false);
        }
        if (horizontal < 0)
        {
            if (isGrounded())
            {
                anim.SetBool("runl", true);
                anim.SetBool("jump_startl", false);
                anim.SetBool("jump_midl", false);
            }
            /*else if (!isGrounded())
            {
                anim.SetBool("runl", false);
                anim.SetBool("jump_startl", true);
                Invoke("JumpAnimLeft", 0.01f);
            }*/
        }
        else
        {
            anim.SetBool("runl", false);
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
    
    //Anim Invoke
    private void HitRight()
    {
        anim.SetBool("hit", false);
    }

    private void HitLeft()
    {
        anim.SetBool("hitl", false);
    }

    private void JumpAnimRight()
    {
        anim.SetBool("jump_start", false);
        anim.SetBool("jump_mid", true);
    }

    private void JumpAnimLeft()
    {
        anim.SetBool("jump_startl", false);
        anim.SetBool("jump_midl", true);
    }

    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            readyToJump = true;
            jumpedOnce = false;
        }
        
        if (collision.gameObject.tag == "enemy")
        {
            //
        }
    }
}
