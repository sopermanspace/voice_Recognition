using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
 public GameObject Enemys;
    public Transform[] SpawnPoints;
    public float spawnTime = 5f;
    private float timer; 
    public bool spawnEnemies = true;

    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemies)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        if (timer <= 0f)
        {
            int randomIndex = Random.Range(0, SpawnPoints.Length);
            Instantiate(Enemys, SpawnPoints[randomIndex].position, Quaternion.identity);
            timer = spawnTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void StartSpawning()
    {
        spawnEnemies = true;
    }

    public void StopSpawning()
    {
        spawnEnemies = false;
    }


}//class
