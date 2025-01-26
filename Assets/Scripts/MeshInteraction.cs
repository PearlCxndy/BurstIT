using UnityEngine;

public class MeshInteraction : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Reference to the player
    public SpawnArrow spawnArrow; // Reference to the arrow script
    public Transform globeCenter; // Reference to the globe's center
    public float distanceFromGlobe = 5.1f; // Desired distance from the globe's surface
    public Animation playerAnimation; // Reference to the player's Animation component

    [Header("Throwing Settings")]
    public KeyCode throwKey = KeyCode.T; // Key to throw the object
    public float throwForce = 10f; // Forward throwing force
    public float throwUpwardForce = 2f; // Upward throwing force

    private Rigidbody rb; // Rigidbody for physics
    private bool isAttached = true; // Start as attached to the player
    private Vector3 stopArrowDirection; // Final direction of the arrow when stopped
    private bool isThrowing = false; // Is the player currently throwing?

    private void Start()
    {
        // Cache the Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on this object.");
        }

        // Disable gravity and physics initially
        rb.isKinematic = true;
        rb.useGravity = false;

        // Check if the playerAnimation is assigned
        if (playerAnimation == null)
        {
            Debug.LogError("No Animation component found on the player!");
        }
    }

    private void Update()
{
    // Allow throwing if the object is attached and the player presses the throw key
    if (Input.GetKeyDown(throwKey) && isAttached && !isThrowing)
    {
        PrepareToThrow();
    }
}

private void PrepareToThrow()
{
    isThrowing = true; // Prevent further inputs during the throw sequence

    // Stop the arrow's movement and freeze it in its current direction
    if (spawnArrow != null)
    {
        spawnArrow.StopArrowOscillation(); // Stop oscillating the arrow
        stopArrowDirection = spawnArrow.GetArrowDirection(); // Get the arrow's final direction
    }

    // Play the player's throwing animation
    if (playerAnimation != null)
    {
        Debug.Log("Playing throwing animation...");
        playerAnimation.Play("throwing"); // Assumes "throwing" is an animation clip
    }

    // Call the ThrowObject method after the animation delay
    float throwingAnimationDuration = 1.0f; // Adjust based on your animation length
    Invoke(nameof(ThrowObject), throwingAnimationDuration);
}

private void ThrowObject()
{
    Debug.Log("Throwing the object...");

    // Detach from the player
    isAttached = false;
    transform.SetParent(null);

    // Enable physics and gravity
    if (rb != null)
    {
        rb.isKinematic = false;
        rb.useGravity = true;

        // Apply the throwing force in the direction the arrow was pointing
        Vector3 forceToAdd = stopArrowDirection * throwForce + player.up * throwUpwardForce;
        rb.AddForce(forceToAdd, ForceMode.Impulse);

        Debug.Log($"Object thrown with force: {forceToAdd}");
    }

    // Hide the arrow after throwing
    if (spawnArrow != null)
    {
        Debug.Log("Hiding the arrow...");
        spawnArrow.SetArrowState(false); // Disable the arrow
    }

    // Reset the throwing state after the throw is completed
    Invoke(nameof(ResetThrowState), 0.5f);
}

private void ResetThrowState()
{
    isThrowing = false; // Allow new interactions
}

}