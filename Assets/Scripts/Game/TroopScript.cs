using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : MonoBehaviour
{
    public GameObject bullet;

    public LayerMask EnemyLayer;
    public float damage = 1f;
    public float shootCooldown = 0.67f;
    public float viewRadius = 10f;

    GameObject targetEnemy = null;

    private bool canSeeEnemy = false;
    
    void Start()
    {
        
    }

    float cooldown = 0;
    void Update()
    {
        targetEnemy = FindClosestEnemy();
        if(targetEnemy != null)
        {
            transform.LookAt(targetEnemy.transform);
            if(cooldown > shootCooldown)
            {
                Shoot(targetEnemy);
                cooldown = 0;
            }
        }

        cooldown += Time.deltaTime;
    }

    GameObject FindClosestEnemy()
    {
        GameObject closest = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, viewRadius, EnemyLayer);
        if(cols.Length != 0)
        {
            float shortestDistance = viewRadius; // default
            foreach(Collider objCol in cols)
            {
                float distance = Vector3.Distance(transform.position, objCol.gameObject.transform.position);
                if (distance < shortestDistance)
                {
                    closest = objCol.gameObject.transform.gameObject;
                    shortestDistance = distance;
                }
            }
        }

        return closest;
    }

    void Shoot(GameObject enemy)
    {
        EnemyHealth enemyhp = enemy.GetComponentInChildren<EnemyHealth>();
        enemyhp.AdjustHealth(-damage);

        GameObject _bullet = Instantiate(bullet);
        _bullet.GetComponent<BulletScript>().SetTargetPosition(transform.position, enemy.transform.position);
    }
}
