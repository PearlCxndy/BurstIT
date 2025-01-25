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

        // Check for movement input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(globeCenter.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            anim.Play("running");
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(globeCenter.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
            anim.Play("running");
            isMoving = true;
        }

        // Reset to default position and rotation if idle
        if (!isMoving)
        {
            anim.Play("idle");
            ResetToDefaultRotation();
        }

        // Align to the globe's surface
        AlignWithGlobeSurface();
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
