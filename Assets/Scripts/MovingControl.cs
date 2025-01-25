using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingControl : MonoBehaviour
{
    private Animation anim; // Reference to the Animation component
    private Vector3 direction = Vector3.zero; // Movement direction
    private float fVel = 2.0f; // Movement speed

    void Start()
    {
        // Get the Animation component attached to the GameObject
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        // Reset direction
        direction = Vector3.zero;

        // Check for movement input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -transform.right * (fVel * Time.deltaTime); // Move left
            anim.Play("running"); // Play running animation
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = transform.right * (fVel * Time.deltaTime); // Move right
            anim.Play("running"); // Play running animation
        }
        else
        {
            anim.Play("idle"); // Play idle animation when no keys are pressed
        }

        // Check for mouse click (trigger throwing animation)
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            anim.Play("throwing"); // Play throwing animation
        }

        // Apply movement to the GameObject
        transform.position += direction;
    }
}
