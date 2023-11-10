using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private Animator animator;

    private float timer;
    [SerializeField] private float timerlimit=2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timerlimit)
        {
            timer = 0;
            shoot();
            
        }
    }

    void shoot()
    {
        GameObject go = Instantiate(bullet, bulletPosition.position, Quaternion.identity) ;
        Vector3 direction = new Vector3(transform.localScale.x, 0);
       // go.GetComponent<EnemyFireBall>().Setup(direction);
    }


}
