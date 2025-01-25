using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingControl : MonoBehaviour
{
    private Animation playerAnimation; // Reference to the Animation component
    public Transform globeCenter; // Reference to the globe's center
    public float rotationSpeed = 50f; // Speed of movement around the globe
    public float distanceFromGlobe = 5.1f; // Desired distance from the globe's surface
    private Quaternion defaultRotation; // Default idle rotation
    private bool isMoving = false; // Track if the player is moving

    void Start()
    {
        // Get the Animation component attached to the GameObject
        playerAnimation = GetComponent<Animation>();
        if (playerAnimation == null)
        {
            Debug.LogError("No Animation component found on the player!");
        }

        // Set the player's initial position and rotation
        transform.position = new Vector3(-0.8611059f, 3.92f, 1.940774f);
        transform.rotation = Quaternion.Euler(15.792f, -178.773f, -0.648f);

        // Save the default rotation as the idle rotation
        defaultRotation = transform.rotation;

        // Align the player to the globe's surface
        AlignWithGlobeSurface();
    }

    void Update()
    {
        isMoving = false; // Reset movement flag
        Vector3 movementDirection = Vector3.zero; // Track the movement direction

        // Check for movement input and set movement direction
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(globeCenter.position, Vector3.up, rotationSpeed * Time.deltaTime);
            playerAnimation.Play("running");
            isMoving = true;
            movementDirection = Vector3.left; // Moving left
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(globeCenter.position, Vector3.up, -rotationSpeed * Time.deltaTime);
            playerAnimation.Play("running");
            isMoving = true;
            movementDirection = Vector3.right; // Moving right
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(globeCenter.position, Vector3.right, rotationSpeed * Time.deltaTime);
            playerAnimation.Play("running");
            isMoving = true;
            movementDirection = Vector3.forward; // Moving forward
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.RotateAround(globeCenter.position, Vector3.right, -rotationSpeed * Time.deltaTime);
            playerAnimation.Play("running");
            isMoving = true;
            movementDirection = Vector3.back; // Moving backward
        }

        // If moving, rotate player to face movement direction
        if (isMoving && movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 500f); // Smooth rotation
        }
        else if (!isMoving)
        {
            // Reset to default animation and rotation
            playerAnimation.Play("idle");
            ResetToDefaultRotation();
        }

        // Align to the globe's surface
        AlignWithGlobeSurface();
    }


    void AlignWithMovementDirection()
    {

    }
    void AlignWithGlobeSurface()
    {
        Vector3 directionToGlobe = (transform.position - globeCenter.position).normalized;
        transform.position = globeCenter.position + directionToGlobe * distanceFromGlobe;
        transform.up = directionToGlobe;
    }

    void ResetToDefaultRotation()
    {
        transform.rotation = defaultRotation;
    }
}
