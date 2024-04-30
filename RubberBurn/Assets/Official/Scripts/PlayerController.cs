using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool is_grounded;
    private Rigidbody body;
    private float distance_to_wall_left = 2f;
    private float distance_to_wall_right = 2f;
    private float distance_to_wall_forward = 2f;
    private float distance_to_wall_back = 2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Tank movement
        float translation = Input.GetAxis("Vertical") * 10 * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * 100 * Time.deltaTime;

        // Tank rotation
        transform.Rotate(0, rotation, 0);

        // Tank forward movement
        if (translation > 0 && distance_to_wall_forward > 0.6f)
        {
            body.AddForce(transform.forward * translation, ForceMode.Force);
        }

        // Tank backward movement
        if (translation < 0 && distance_to_wall_back > 0.6f)
        {
            body.AddForce(transform.forward * translation,ForceMode.Force);
        }

        // Unlock cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        DistanceToWall();
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
