using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody rb;
    //projectile vars
    public float speed;
    public float lifetime;
    public bool isFacingLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("ChaserEnemy"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
