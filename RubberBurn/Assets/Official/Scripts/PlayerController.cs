using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Raycast
    private bool is_grounded;
    private Rigidbody body;
    private float distance_to_wall_left = 2f;
    private float distance_to_wall_right = 2f;
    private float distance_to_wall_forward = 2f;
    private float distance_to_wall_back = 2f;

    // Settings
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    // Variables
    private Vector3 Force;

    void Update()
    {
        Move();
     
    }

    public void Move()
    {
        // Moving
        Force += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += Force * Time.deltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * Force.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit
        
            Force *= Drag;
        Force = Vector3.ClampMagnitude(Force, MaxSpeed);

        // Traction
        Debug.DrawRay(transform.position, Force.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        Force = Vector3.Lerp(Force.normalized, transform.forward, Traction * Time.deltaTime) * Force.magnitude;
    }

    private void DistanceToWall()
    {
        RaycastHit hit;
        Ray left_ray = new Ray(transform.position, -transform.right);
        Ray front_ray = new Ray(transform.position, transform.forward);
        Ray back_ray = new Ray(transform.position, -transform.forward);
        Ray right_ray = new Ray(transform.position, transform.right);

        if (Physics.Raycast(left_ray, out hit) && !hit.collider.isTrigger)
        {
            distance_to_wall_left = hit.distance;
        }
        else
        {
            distance_to_wall_left = 3;
        }

        if (Physics.Raycast(front_ray, out hit) && !hit.collider.isTrigger)
        {
            distance_to_wall_forward = hit.distance;
        }
        else
        {
            distance_to_wall_forward = 3;
        }

        if (Physics.Raycast(back_ray, out hit) && !hit.collider.isTrigger)
        {
            distance_to_wall_back = hit.distance;
        }
        else
        {
            distance_to_wall_back = 3;
        }

        if (Physics.Raycast(right_ray, out hit) && !hit.collider.isTrigger)
        {
            distance_to_wall_right = hit.distance;
        }
        else
        {
            distance_to_wall_right = 3;
        }

        if (Physics.Raycast(transform.position, -transform.up, 1.1f))
        {
            is_grounded = true;
        }
        else
        {
            is_grounded = false;
        }
    }
}
