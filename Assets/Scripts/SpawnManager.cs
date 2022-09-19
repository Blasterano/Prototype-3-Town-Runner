using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    Vector3 spawnpos;
    PlayerController playerControllerScript;
    float startDelay = 2, repeatRate = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        int spawnId;
        spawnId = Random.Range(0, obstaclePrefab.Length);
        spawnpos = new Vector3(35, obstaclePrefab[spawnId].transform.position.y, obstaclePrefab[spawnId].transform.position.z);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[spawnId], spawnpos, obstaclePrefab[spawnId].transform.rotation);
        }

    }
}
