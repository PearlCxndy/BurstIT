using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnRadius = 10.0f; // Default value
    public int numberOfObjects = 10; // Default value

    public bool randomOrientation = false;
    public bool orientToSurface = false;

    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // What we will spawn
            GameObject objectToSpawn = objectsToSpawn[UnityEngine.Random.Range(0, objectsToSpawn.Length)];

            // Vector2 spawnPositionV2 = UnityEngine.Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPositionV2 = UnityEngine.Random.onUnitSphere * spawnRadius;

            // Vector3 spawnPosition = new Vector3(spawnPositionV2.x, 0.0f, spawnPositionV2.y);
            Vector3 spawnPosition = new Vector3(spawnPositionV2.x, spawnPositionV2.y, spawnPositionV2.z);

            Vector3 transformOffsetSpawnPosition = transform.position + spawnPosition;

            // Quaternion.identity

            RaycastHit hit;
            if (Physics.Raycast(transformOffsetSpawnPosition, Vector3.down, out hit))
            {
                Vector3 finalSpawnPosition = hit.point;

                Quaternion orientation;

                if (randomOrientation)
                {
                    orientation = UnityEngine.Random.rotation;
                }
                else if (orientToSurface)
                {
                    orientation = Quaternion.LookRotation(hit.normal);
                }
                else
                {
                    orientation = objectToSpawn.transform.rotation;
                }


                Instantiate(objectToSpawn, finalSpawnPosition, orientation);
                Debug.Log("Spawned " + objectToSpawn.name + " at " + finalSpawnPosition);
            }

        }
    }
}
