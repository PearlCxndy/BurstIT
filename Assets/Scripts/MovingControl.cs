using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingControl : MonoBehaviour
{
    private Animation anim; // Reference to the Animation component
    public Transform globeCenter; // Reference to the globe's center
    public Transform mainCamera; // Reference to the main camera
    public float rotationSpeed = 50f; // Speed of movement around the globe
    public float distanceFromGlobe = 5.1f; // Desired distance from the globe's surface
    private bool isMoving = false; // Track if the player is moving

    void Start()
    {
        // Get the Animation component attached to the GameObject
        anim = GetComponent<Animation>();
        if (anim == null)
        {
            Debug.LogError("No Animation component found on the player!");
        }

        // Set the player to the correct distance from the globe at the start
        AlignWithGlobeSurface();
    }

    void Update()
    {
        isMoving = false; // Reset movement flag

        // Check for movement input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate counter-clockwise around the globe
            transform.RotateAround(globeCenter.position, Vector3.forward, rotationSpeed * Time.deltaTime);

            // Play the running animation for counter-clockwise movement
            anim.Play("running");
            isMoving = true;

            // Face left
            FaceDirection(-1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate clockwise around the globe
            transform.RotateAround(globeCenter.position, Vector3.forward, -rotationSpeed * Time.deltaTime);

            // Play the running animation for clockwise movement
            anim.Play("running");
            isMoving = true;

            // Face right
            FaceDirection(1);
        }

        // If not moving, reset to face the main camera
        if (!isMoving)
        {
            anim.Play("idle"); // Play idle animation
            FaceCamera();
        }

        // Align the player to the globe's surface after movement
        AlignWithGlobeSurface();
    }

    void AlignWithGlobeSurface()
    {
        // Calculate the direction toward the globe's center
        Vector3 directionToGlobe = (transform.position - globeCenter.position).normalized;

        // Adjust the player's position to stay at the desired distance from the globe
        transform.position = globeCenter.position + directionToGlobe * distanceFromGlobe;

        // Align the player's "up" direction to face outward along the globe's curvature
        transform.up = directionToGlobe;
    }

    void FaceDirection(int direction)
    {
        // Adjust the local rotation to face clockwise (1) or counter-clockwise (-1)
        if (direction == -1) // Face left
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0); // Rotate to face left
        }
        else if (direction == 1) // Face right
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0); // Rotate to face right
        }
    }

    void FaceCamera()
    {
        // Calculate the direction to face the main camera
        Vector3 directionToCamera = (mainCamera.position - transform.position).normalized;

        // Update rotation to look at the camera
        Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera, transform.up);
        transform.rotation = targetRotation;
    }
}
