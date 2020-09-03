using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    void Start()
    {
        
    }

    void Update()
    {
        if (health < 0) {
            Debug.Log("Yoo I fucking died bro - " + gameObject.name);
            Destroy(transform.gameObject);
        }
    }

    public float GetHealth() { return health; }
    public void SetHealth(float newHealth) { health = newHealth; }
    public void AdjustHealth(float adjustment) { health += adjustment; }
}
