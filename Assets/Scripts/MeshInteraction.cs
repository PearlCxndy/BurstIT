using UnityEngine;

public class MeshInteraction : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Reference to the player
    public Transform globeCenter; // Reference to the globe's center
    public float distanceFromGlobe = 5.1f; // Desired distance from the globe's surface

    [Header("Throwing Settings")]
    public KeyCode throwKey = KeyCode.T; // Key to throw the object
    public float throwForce = 10f; // Forward throwing force
    public float throwUpwardForce = 2f; // Upward throwing force

    private Rigidbody rb; // Rigidbody for physics
    private bool isAttached = true; // Start as attached to the player

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
    }

    private void Update()
    {
        // Allow throwing if the object is attached
        if (Input.GetKeyDown(throwKey) && isAttached)
        {
            ThrowObject();
        }
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

            // Apply the throwing force
            Vector3 forceDirection = player.forward; // Forward direction of the player
            Vector3 forceToAdd = forceDirection * throwForce + player.up * throwUpwardForce;

            rb.AddForce(forceToAdd, ForceMode.Impulse);
            Debug.Log($"Object thrown with force: {forceToAdd}");
        }

        // Optionally, disable the script after throwing
        this.enabled = false;
    }
}
