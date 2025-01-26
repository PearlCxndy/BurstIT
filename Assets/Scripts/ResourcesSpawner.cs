using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject resourcePrefab_S;
    public GameObject resourcePrefab_M;
    public GameObject resourcePrefab_L;
    public float spawnChance;

    [Header("Circle Area")]
    public float innerRadius; // Radius of the inner circle (no spawn area)
    public float outerRadius; // Radius of the outer circle (max spawn area)
    public float distanceBetweenCheck; // Distance between spawn points

    private void Start()
    {
        SpawnResources();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeleteResources();
            SpawnResources();
        }
    }

    void SpawnResources()
    {
        // Loop through radii from innerRadius to outerRadius
        for (float radius = innerRadius; radius < outerRadius; radius += distanceBetweenCheck)
        {
            float circumference = 2 * Mathf.PI * radius; // Circumference at this radius
            int numPoints = Mathf.FloorToInt(circumference / distanceBetweenCheck); // Number of points to check along the circle

            // Loop through points around the circle
            for (int i = 0; i < numPoints; i++)
            {
                float angle = (i / (float)numPoints) * 2 * Mathf.PI; // Angle in radians
                float x = radius * Mathf.Cos(angle); // Convert polar to Cartesian (X)
                float y = radius * Mathf.Sin(angle); // Convert polar to Cartesian (Y)

                Vector3 spawnPosition = new Vector3(x, y, 0); // Z is set to 0 for 2D

                // Check if the position qualifies for spawning based on spawnChance
                if (Random.Range(0f, 100f) < spawnChance)
                {
                    GameObject prefabToSpawn = GetRandomPrefab();
                    Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);
                }
            }
        }
    }

    GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, 3); // Random number between 0 (inclusive) and 3 (exclusive)
        switch (randomIndex)
        {
            case 0: return resourcePrefab_S;
            case 1: return resourcePrefab_M;
            case 2: return resourcePrefab_L;
            default: return resourcePrefab_S;
        }
    }

    void DeleteResources()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}