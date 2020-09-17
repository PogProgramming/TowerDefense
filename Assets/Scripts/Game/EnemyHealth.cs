﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int reward = 250;

    public Stats statSystem;
    public float health = 100;

    void Start()
    {
        statSystem = GameObject.Find("UpdateSystem").GetComponent<Stats>();
    }

    void Update()
    {
        if (health < 0) {
            Kill(true);
        }
    }

    public float GetHealth() { return health; }
    public void SetHealth(float newHealth) { health = newHealth; }
    public void AdjustHealth(float adjustment) { health += adjustment; }
    public void Kill(bool giveReward)
    {
        if (giveReward)
        {
            statSystem.AdjustCash(reward);
            statSystem.killedEnemies++;
        }

        statSystem.activeEnemies--;

        Destroy(transform.gameObject);
    }
}
