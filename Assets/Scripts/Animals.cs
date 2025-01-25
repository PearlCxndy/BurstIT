using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
    public Transform globeCenter;
    public Transform player;
    public GameObject[] animals; // Array of animal prefabs

    float earthRadius = 1.7F; // Radius of the Earth (Collision: 4.1f sth)
    // c.f. But the character is using the value 1.7 so bear in mind.

    public float distanceFromGlobe = 1.7f; // (also from MovingControl.cs)
    // 4.064419f
    public float respawnTime = 5f; // Time between spawns (5s)
    public float spawnDist = 0.7f; // Distance between the player and the (will be spawned) animal.

    private List<GameObject> spawnedAnimals = new List<GameObject>(); // Tracked the spawned animals
    // Data structure: Queue (FIFO) - first in, first out

    // Start is called before the first frame update
    void Start()
    {   
        globeCenter = GameObject.Find("Obj_Earth").transform; // Reference to the globe's center (from MovingControl.cs)
        player = GameObject.Find("MainCharacter").transform; // Reference to the main character (player)

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
        a.transform.position = GetRandomPositionOnGlobe(); // Get a random position on the globe
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

    private Vector3 GetRandomPositionOnGlobe()
    {
        // Randomly choose an angle for spawning the animal on the globe's surface
        float angle = Random.Range(0f, 360f); // Random angle between 0 and 360 degrees

        // Use spherical coordinates to get a point on the globe's surface
        float x = globeCenter.position.x + earthRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = globeCenter.position.y; // Y stays constant (in 2D view)
        float z = globeCenter.position.z + earthRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

        return new Vector3(x, y, z); // Return the position on the surface
    }
}
