using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    // 1.71, 1.71 (positivePosition)
    // -1.71, -1.71 (negativePosition)

    [Header("Spawn Settings")]
    public GameObject animalPrefab;
    public float spawnChance;

    [Header("Raycast Settings")]
    public float distanceBetweenChecks;
    public float heightOfCheck = 10f, rangeOfCheck = 30f;
    public LayerMask layerMask;

    public Vector2 positivePosition, negativePosition;

    private void Start()
    {
        SpawnAnimals();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeleteAnimals();
            SpawnAnimals();
        }
    }

    void SpawnAnimals()
    {
        for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks)
        {
            for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, layerMask))
                {
                    if (spawnChance > Random.Range(0, 3))
                    {
                        Instantiate(animalPrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                    }
                }
            }
        }
    }

    void DeleteAnimals()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
