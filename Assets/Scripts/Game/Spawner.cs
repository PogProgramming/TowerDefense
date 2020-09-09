using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Stats statSystem;

    public GameObject spawnType;
    public float spawnCooldown = 10;

    float spawnTimer = 0;
    void Start()
    {
        statSystem = GetComponent<Stats>();
    }

    
    void Update()
    {
        spawnCooldown -= Time.deltaTime / 250;
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnCooldown)
        {
            if (spawnType != null)
            {
                statSystem.activeEnemies++;
                Instantiate(spawnType);
            }

            spawnTimer = 0;
        }
    }
}
