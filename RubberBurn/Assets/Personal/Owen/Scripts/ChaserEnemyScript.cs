using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    int enemyHealth = 1;
    int enemyDamage = 25;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        rb.AddForce(speed, 0, 0, ForceMode.Acceleration);
        print("adding force");
    }
}
