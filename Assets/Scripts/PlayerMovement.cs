using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Joystick joystick;
    private bool readyToJump;
    private bool jumpedOnce;
    private bool dead;
    private int lives;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readyToJump = true;
        jumpedOnce = false;
        dead = false;
        lives = 3;
        UIManejador.Instance.AgregarVidas(lives);
    }

    void Update()
    {
        //Move
        if (!dead)
        {
            //teclado
            //horizontal = Input.GetAxisRaw("Horizontal");
            //joystick
            horizontal = joystick.Horizontal;
        }
        else if (dead)
        {
            horizontal = 0;
            rb.bodyType = RigidbodyType2D.Static;
        }
        //teclado
        vertical = Input.GetAxisRaw("Vertical");
        //joystick
        vertical = joystick.Vertical;
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        {
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
            }
            else if (!isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("runl", false);
                anim.SetBool("jump_mid", true);
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
        }

        if (horizontal < 0)
        {
            if (isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("runl", true);
                anim.SetBool("jump_midl", false);
            }
            else if (!isGrounded())
            {
                anim.SetBool("run", false);
                anim.SetBool("runl", false);
                anim.SetBool("jump_midl", true);
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
                UIManejador.Instance.QuitarVidas();
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
                UIManejador.Instance.QuitarVidas();
                Die();
                StartCoroutine(WaitForSceneLoad());
            }
        }
    }

    private void Die()
    {
        dead = true;
        if (anim.GetFloat("lastMoveX") < 0)
        {
            anim.SetTrigger("killl");
        }
        else if (anim.GetFloat("lastMoveX") > 0)
        {
            anim.SetTrigger("kill");
        }
    }



    public void JumpButton()
    {
        if (readyToJump)
        {
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
    }


    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(3);

    }


}
