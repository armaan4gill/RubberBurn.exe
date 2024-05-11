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

        // if no target specified, assume the player
        if (target == null)
        {

            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    void Update()
    {
        GapCloser();
        transform.LookAt(target);
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
    private void OnCollisionEnter(Collision collision)//handles the player taking damage
    {
        if (collision.gameObject.GetComponent<DamageController>() != null)
        {
            collision.gameObject.GetComponent<DamageController>().TakeDamage(enemyDamage);
            StartCoroutine(StopMovement());
        }
    }
    IEnumerator StopMovement()
    {
        float Currentspeed = speed;
        yield return new WaitForSeconds(1.5f);
        speed = Currentspeed;
    }

}
