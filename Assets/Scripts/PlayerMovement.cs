using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    //private float vertical;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    //[SerializeField] private Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            //rb.AddForce(new Vector2(0, jumpForce * 10)/*, ForceMode2D.Impulse*/);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if (horizontal > 0)
        {
            anim.SetBool("run", true);
        }
        if (horizontal < 0)
        {
            anim.SetBool("runl", true);
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
}
