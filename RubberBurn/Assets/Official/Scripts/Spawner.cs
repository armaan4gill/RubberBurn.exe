using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    //vars
    public bool goingLeft;
    public float timeBetweenShots;
    public float startDelay;
    public GameObject projectilePrefab;
    public Transform laserSpawn;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", startDelay, timeBetweenShots);
    }

    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, laserSpawn.rotation);
    }
}
