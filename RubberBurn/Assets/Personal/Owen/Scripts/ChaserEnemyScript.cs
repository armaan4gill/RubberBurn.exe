using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public Transform target;
    public float withinRange;

    int enemyHealth = 1;
    int enemyDamage = 25;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GapCloser();
    }

    private void GapCloser()
    {
        //get the distance between the player and enemy (this object)
        float dist = Vector3.Distance(target.position, transform.position);

        //check if it is within the range you set
        if (dist <= withinRange)
        {
            //move to target(player) 
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
    }
}
