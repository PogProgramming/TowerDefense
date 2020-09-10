using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopScript : MonoBehaviour
{
    public GameObject bullet;
    public LayerMask EnemyLayer;

    readonly float costMultiplier = 1.6f;

    private int level = 0;
    [SerializeField] private long cost = 250;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float shootCooldown = 1.2f;

    [SerializeField] private float viewRadius = 5f;
    GameObject targetEnemy = null;

    private bool displayTroop = false;

    public void SetIsDisplayTroop(bool _set) { displayTroop = _set; }
    public long GetCost() { return cost; }
    public int GetUpgradeCost() { return (int)(cost * costMultiplier); }
    public float GetDamage() { return damage; }
    public float GetShootCooldown() { return shootCooldown; }
    public float GetViewRadius() { return viewRadius; }
    public float GetLevel() { return level; }

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
        GameObject.Find("UpdateSystem").GetComponent<Stats>().AdjustCash(-cost); 

        this.level++;
        cost = (long)(cost * costMultiplier);
        damage = (long)(damage * costMultiplier);
        cooldown = cooldown / 1.1f;
        viewRadius++;
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
