using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    [Header("Arrow Settings")]
    public Transform globeCenter; // Reference to the globe's center
    public Transform playerLocation; // Reference to the player's position
    public float arrowDistanceFromGlobe = 4.0f; // How far away from the globe is the arrow
    public float rotationSpeed = 2.0f; // Speed of oscillation
    public float oscillationAngle = 45f; // Maximum oscillation angle (±45 degrees)

    private Renderer arrowRenderer; // To control visibility of the arrow
    private float timeElapsed = 0f; // Tracks time for oscillation
    private bool isOscillating = false; // Controls whether the arrow oscillates
    private Vector3 currentDirection; // Current direction the arrow is pointing

    private void Start()
    {
        // Cache the Renderer component
        arrowRenderer = GetComponent<Renderer>();
        if (arrowRenderer == null)
        {
            Debug.LogError("Arrow Renderer not found!");
        }

        // Hide the arrow at the start
        SetArrowState(true); // false
    }

    private void Update()
    {
        // Follow the player and optionally oscillate if enabled
        if (arrowRenderer.enabled)
        {
            FollowPlayerAroundGlobe();

            if (isOscillating)
            {
                OscillateArrow();
            }
        }
    }

    public void SetArrowState(bool state)
    {
        // Show or hide the arrow
        if (arrowRenderer != null)
        {
            arrowRenderer.enabled = state;
        }

        // Stop oscillation if hiding
        if (!state)
        {
            isOscillating = false;
            timeElapsed = 0f; // Reset oscillation
        }
    }

    public void StartOscillation()
    {
        isOscillating = true;
        timeElapsed = 0f; // Reset oscillation
    }

    public void StopArrowOscillation()
    {
        // Stops the arrow's movement
        isOscillating = false;
    }

    public Vector3 GetArrowDirection()
    {
        // Return the current direction the arrow is pointing
        return transform.up.normalized;
    }

    private void OscillateArrow()
    {
        // Increment time for the sine wave
        timeElapsed += Time.deltaTime * rotationSpeed;

        // Calculate the oscillation angle offset
        float offsetAngle = Mathf.Sin(timeElapsed) * oscillationAngle;

        // Apply the oscillation rotation
        Quaternion oscillationRotation = Quaternion.Euler(0, 0, offsetAngle);
        transform.rotation = oscillationRotation * transform.rotation;

        // Update the arrow's current direction
        currentDirection = transform.up.normalized;
    }

    private void FollowPlayerAroundGlobe()
    {
        // Align the arrow's position around the globe based on the player
        if (globeCenter == null || playerLocation == null)
        {
            Debug.LogError("GlobeCenter or PlayerLocation is not assigned!");
            return;
        }

        // Direction from the globe center to the player
        Vector3 directionToPlayer = (playerLocation.position - globeCenter.position).normalized;

        // Position the arrow slightly above the globe surface
        transform.position = globeCenter.position + directionToPlayer * arrowDistanceFromGlobe;

        // Align the arrow's up direction with the player
        transform.up = directionToPlayer;

        // Update the arrow's current direction
        currentDirection = transform.up.normalized;
    }
}
