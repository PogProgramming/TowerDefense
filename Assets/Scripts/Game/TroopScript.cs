using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : MonoBehaviour
{
    public GameObject bullet;
    public LayerMask EnemyLayer;

    readonly double costMultiplier = 1.6;

    private int level = 0;

    [SerializeField] private int cost = 250;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float shootCooldown = 1.2f;

    [SerializeField] private float viewRadius = 10f;
    GameObject targetEnemy = null;

    private bool displayTroop = false;

    public void SetIsDisplayTroop(bool _set) { displayTroop = _set; }
    public int GetCost() { return cost; }

    float cooldown = 0;
    void Update()
    {
        if (displayTroop) return; // for the highlighter

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

    public void LevelUp()
    {
        this.level++;
        cost = (int)(cost * costMultiplier);
        damage = (int)(damage * costMultiplier);
        cooldown = cooldown / (float)costMultiplier;
    }

    public void SetLevel(int level)
    {
        this.level = level;
        cost = level * (int)(cost * costMultiplier);
        damage = level * (int)(damage * costMultiplier);
        cooldown = level * (cooldown / (float)costMultiplier);
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
