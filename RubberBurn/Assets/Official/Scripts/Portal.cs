using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gill, Armaan
 * Machus, Ryan
 * Last updated: 04/22/2024
 * This script teleports player to teleport point
 */
public class Portal : MonoBehaviour
{
    private Transform teleportPoint;


    private void Awake()
    {
        teleportPoint = transform.GetChild(0).transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportPoint.transform.position;  
    }
}
