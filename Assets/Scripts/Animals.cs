using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
    public Transform globeCenter; // Reference to the globe's center (from MovingControl.cs)
    public Transform player; // Reference to the main character (player)
    public GameObject[] animals; // Array of animal prefabs
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
    }  

    // Update is called once per frame
    void Update()
    {
        
    }
}
