using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    } 

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed,0);

    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
