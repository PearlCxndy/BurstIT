using UnityEngine;

public class AttachToHead : MonoBehaviour
{
    [Header("References")]
    public Transform playerHead; // The player's head where the object will attach
    public float heightOffset = 1.5f; // Height offset for the attachment
    public MeshInteraction meshInteraction; // Reference to the MeshInteraction script on the same object
    public SpawnArrow spawnArrow; // Reference to the SpawnArrow script

    private void Start()
    {
        // Ensure the MeshInteraction script is initially disabled
        if (meshInteraction != null)
        {
            meshInteraction.enabled = false;
        }

        // Ensure the arrow is initially hidden
        if (spawnArrow != null)
        {
            spawnArrow.SetArrowState(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collides with the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone. Attaching object to the head.");

            // Attach the object to the player's head
            AttachToPlayerHead();

            // Enable the MeshInteraction script for throwing
            if (meshInteraction != null)
            {
                meshInteraction.enabled = true;
            }

            // Show and oscillate the arrow
            if (spawnArrow != null)
            {
                spawnArrow.SetArrowState(true);
                spawnArrow.StartOscillation();
            }

            // Disable this script to prevent reattachment
            this.enabled = false;
        }
    }

    private void AttachToPlayerHead()
    {
        // Attach the object to the player's head and adjust its position
        transform.SetParent(playerHead);
        transform.localPosition = new Vector3(0, heightOffset, 0); // Adjust position on the head
        transform.localRotation = Quaternion.identity; // Reset rotation

        Debug.Log("Object successfully attached to the player's head.");
    }
}
