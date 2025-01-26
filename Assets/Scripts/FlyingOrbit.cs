using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingOrbit : MonoBehaviour
{
    public GameObject BubblePrefab; // Prefab for the bubble
    public Transform target;       // Target object to orbit around
    public float speed = 2f;       // Speed of orbiting
    public float radius = 1f;      // Radius of the orbit
    public float angle = 0f;       // Current angle of rotation

    private GameObject bubble;     // Reference to the instantiated bubble

    void Start()
    {
        // Instantiate the bubble and make it a child of the orbiting object
        bubble = Instantiate(BubblePrefab, transform.position, Quaternion.identity);
        bubble.transform.parent = transform;
    }

    void Update()
    {
        // Calculate the position of the orbiting object
        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = target.position.y + Mathf.Sin(angle) * radius;
        float z = target.position.z;

        // Update position of the orbiting object
        transform.position = new Vector3(x, y, z);

        // Update angle based on speed
        angle += speed * Time.deltaTime;

        // Ensure angle wraps around (0 to 2π)
        angle = Mathf.Repeat(angle, Mathf.PI * 2);
    }
}