using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingControl : MonoBehaviour
{
    private Animation anim; // Reference to the Animation component
    public Transform globeCenter; // Reference to the globe's center
    public float rotationSpeed = 50f; // Speed of movement around the globe
    public float distanceFromGlobe = 5.1f; // Desired distance from the globe's surface
    private Quaternion defaultRotation; // Default idle rotation
    private bool isMoving = false; // Track if the player is moving

    void Start()
    {
        // Get the Animation component attached to the GameObject
        anim = GetComponent<Animation>();
        if (anim == null)
        {
            Debug.LogError("No Animation component found on the player!");
        }

        // Save the default rotation as the idle rotation
        defaultRotation = transform.rotation;

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
            SetFixedTilt(-90);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate clockwise around the globe
            transform.RotateAround(globeCenter.position, Vector3.forward, -rotationSpeed * Time.deltaTime);

            // Play the running animation for clockwise movement
            anim.Play("running");
            isMoving = true;

            // Face right
            SetFixedTilt(90);
        }

        // If not moving, reset to the default idle rotation
        if (!isMoving)
        {
            anim.Play("idle"); // Play idle animation
            ResetToDefaultRotation(); // Ensure the rotation stays fixed
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

    void SetFixedTilt(float angle)
    {
        // Tilt the player to the left or right by a fixed angle
        Quaternion tiltRotation = Quaternion.Euler(0, angle, 0); // Tilt along Y-axis only
        transform.rotation = tiltRotation;
    }

    void ResetToDefaultRotation()
    {
        // Immediately reset to the default idle rotation
        transform.rotation = defaultRotation;
    }
}
