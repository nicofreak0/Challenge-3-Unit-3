using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    //array of object prefabs to spawn
    public GameObject[] objectPrefabs;
    //initial delay before spawning objects
    private float spawnDelay = 2;
    //time interval between successive spawns
    private float spawnInterval = 1.5f;

    //reference to the player controller script
    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        //start invoking the spawn object method repeatedly at specified intervals
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        //find the player game object and its player controller component
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Spawn obstacles
    void SpawnObjects ()
    {
        // Set random spawn location and random object index. random y position between 5 and 15
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        //random index for the prefab
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!playerControllerScript.gameOver)
        {
            //spawn the selected prefab
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }

    }
}
