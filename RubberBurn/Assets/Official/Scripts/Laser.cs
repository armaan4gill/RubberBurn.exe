using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
