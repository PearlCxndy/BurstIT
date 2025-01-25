using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
    public Transform globeCenter; // Reference to the globe's center (from MovingControl.cs)
    public Transform player; // Reference to the main character (player)
    public GameObject[] animals; // Array of animal prefabs

    float earthRadius = 4.064419F; // Radius of the Earth

    public float distanceFromGlobe = 5.1f; // (also from MovingControl.cs)
    public float respawnTime = 5f; // Time between spawns (5s)
    public float spawnDist = 2f; // Distance between the player and the (will be spawned) animal.

    private List<GameObject> spawnedAnimals = new List<GameObject>(); // Tracked the spawned animals
    // Data structure: Queue (FIFO) - first in, first out

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(respawnTime);
        }
    }
    
    void Spawn()
    {
        // Randomly choose the animal (S, M, L)
        GameObject ani = animals[Random.Range(0, animals.Length)];

        Vector3 loc;
        do
        {
            loc = Random.onUnitSphere * distanceFromGlobe;
        }
        while (Vector3.Distance(loc, player.position) < spawnDist);

        // Now bring out the selected prefab with the location
        GameObject animal = Instantiate(ani, loc, Quaternion.identity);
        align(animal); // Make sure it's aligned to the Earth's surface

        spawnedAnimals.Add(animal);
        StartCoroutine(ResetAnimals(animal, respawnTime));
    }  

    // Update is called once per frame
    void Update()
    {
        
    }

    void align(GameObject a)
    {   
        // Calculate the direction vector pointing outward from the globe center 
        Vector3 outwards = (a.transform.position - globeCenter.position).normalized;
        a.transform.position = globeCenter.position + outwards * earthRadius;
        a.transform.up = outwards;

        float randomAngle = Random.Range(0f, 360f);
        a.transform.Rotate(0, 0, randomAngle);
    }

    IEnumerator ResetAnimals(GameObject a, float t)
    {
        yield return new WaitForSeconds(t);
        spawnedAnimals.Remove(a);
        Destroy(a);

        Spawn(); // When you destroyed one, spawn another.
    }
}
