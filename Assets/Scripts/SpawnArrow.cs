using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    private Vector3 initialDirection; // Initial direction to the player
    private float rotationSpeed = 2.0f; // Speed of oscillation
    private float arrowDistanceFromGlobe = 4.0f; // How far away from the globe is the arrow
    public Transform globeCenter; // Reference to the center of the globe
    public Transform playerLocation; // Reference to the player's position
    private float oscillationAngle = 45f; // Maximum oscillation angle (±45 degrees)
    private float timeElapsed = 0f; // Tracks time for oscillation
    private Quaternion targetRotation; // Rotation pointing to the player
    private enum ArrowState { Hidden, Oscillating, Static }
    private ArrowState currentState = ArrowState.Hidden;
    private Renderer arrowRenderer; // To control visibility of the arrow

    void Start()
    {
        // Get the Renderer component to show/hide the arrow
        arrowRenderer = GetComponent<Renderer>();
        if (arrowRenderer == null)
        {
            Debug.LogError("Arrow Renderer not found!");
        }

        // Initially hide the arrow
        arrowRenderer.enabled = false;
    }

    void Update()
    {
        // Handle user input
        HandleInput();

        // Perform actions based on the current state
        switch (currentState)
        {
            case ArrowState.Hidden:
                // Arrow is hidden, do nothing
                break;

            case ArrowState.Oscillating:
                FollowPlayerAroundGlobe(); // Arrow follows the player’s movement around the globe
                OscillateArrow(); // Oscillation effect
                break;

            case ArrowState.Static:
                // FollowPlayerAroundGlobe(); // Arrow follows the player’s movement but stays static
                // StopOscillation();
                break;
        }
    }

    void HandleInput()
    {
        // Detect when the player clicks (left mouse button or touch)
        if (Input.GetMouseButtonDown(0)) // Left mouse button or first touch
        {
            if (currentState == ArrowState.Hidden)
            {
                // Show the arrow and start oscillating
                arrowRenderer.enabled = true;
                currentState = ArrowState.Oscillating;
                timeElapsed = 0f; // Reset oscillation time
            }
            else if (currentState == ArrowState.Oscillating)
            {
                // Stop the oscillation and keep the arrow static
                currentState = ArrowState.Static;
            }
            else if (currentState == ArrowState.Static)
            {
                // Stop rotations and hide the arrow
                currentState = ArrowState.Hidden;
                arrowRenderer.enabled = false;
            }
        }
    }


    void OscillateArrow()
    {
        // Increment time for the sine wave
        timeElapsed += Time.deltaTime * rotationSpeed;

        // Calculate the oscillation angle offset
        float offsetAngle = Mathf.Sin(timeElapsed) * oscillationAngle;

        // Add the oscillation offset to the arrow's rotation
        Quaternion oscillationRotation = Quaternion.Euler(0, 0, offsetAngle);

        transform.rotation = oscillationRotation * transform.rotation;
    }

    void StopOscillation()
    {
        // Reset or stop oscillation if necessary
    }

    void FollowPlayerAroundGlobe()
    {
        // Get the direction vector from the globe center to the player
        Vector3 directionToPlayer = playerLocation.position - globeCenter.position;

        // Only use the X and Y components of the direction vector, ignore Z
        Vector3 relativePosition2D = new Vector3(directionToPlayer.x, directionToPlayer.y, 0).normalized;

        // Keep the arrow's Z position constant
        Vector3 newArrowPosition = globeCenter.position + relativePosition2D * arrowDistanceFromGlobe;
        newArrowPosition.z = transform.position.z;

        // Update the arrow's position
        transform.position = newArrowPosition;
        transform.up = relativePosition2D;
    }
}
