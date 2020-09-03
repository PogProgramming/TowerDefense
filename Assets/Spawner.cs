using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnType;
    public float spawnCooldown = 10;

    float spawnTimer = 0;
    void Start()
    {

    }

    
    void Update()
    {
        spawnCooldown -= Time.deltaTime / 250;
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnCooldown)
        {
            if (spawnType != null)
            {
                Instantiate(spawnType);
            }

            spawnTimer = 0;
        }
    }
}
