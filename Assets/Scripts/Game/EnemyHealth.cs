using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int reward = 250;

    public Stats statSystem;
    public float health = 100;

    public 
    void Start()
    {
        statSystem = GameObject.Find("UpdateSystem").GetComponent<Stats>();
    }

    void Update()
    {
        if (health < 0) {
            statSystem.activeEnemies--;
            statSystem.killedEnemies++;

            Destroy(transform.gameObject);
        }
    }

    public float GetHealth() { return health; }
    public void SetHealth(float newHealth) { health = newHealth; }
    public void AdjustHealth(float adjustment) { health += adjustment; }
}
