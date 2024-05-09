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
    private Vector3 tempDrag;

    // Settings
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;
    public float lerpDrag = 1;

    // Variables
    private Vector3 Force;
    public GameObject stunIcon;
    public bool stunned;

    void Update()
    {
        Move();
        DistanceToWall();

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

        //Drift using LERP
        float targetDrag = Input.GetKey(KeyCode.Space) ? Drag + 2 : 0.992f; // Set target drag based on Space key
        tempDrag = Vector3.Lerp(tempDrag, new Vector3(targetDrag, 0, 0), Time.deltaTime * lerpDrag); // Lerp temporary Vector3 with target drag value
        Drag = tempDrag.x; // Extract the x component (assuming targetDrag only affects x)

        // Traction
        Debug.DrawRay(transform.position, Force.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        Force = Vector3.Lerp(Force.normalized, transform.forward, Traction * Time.deltaTime) * Force.magnitude;

    }
    public void StunOn()
    {
        stunIcon.gameObject.SetActive(true);
        Invoke(nameof(StunOff), 0.5f);
    }
    public void StunOff()
    {
        stunIcon.gameObject.SetActive(false);
        if (stunned) Invoke(nameof(StunOn), 0.5f);
    }

    IEnumerator Stun()//handles stunned status
    {
        stunned = true;
        StunOn();
        float currentPlayerSpeed = 100;
        float currentDrag = Drag;
        MoveSpeed = 0;
        Drag = 0;
        yield return new WaitForSeconds(3);
        MoveSpeed = currentPlayerSpeed;
        Drag = currentDrag;
        stunned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")//detects the laser colliding w/ player
        {
            StartCoroutine(Stun());
        }
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
