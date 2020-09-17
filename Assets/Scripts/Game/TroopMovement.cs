using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovement : MonoBehaviour
{
    public GameHealth gameHealth;
    public float distanceMade = 0;

    [SerializeField] private float movementSpeed = 2.5f;
    void Start()
    {
        AddDefaultPositions();
        gameHealth = GameObject.Find("UpdateSystem").GetComponent<GameHealth>();
    }


    List<Vector3> nodes = new List<Vector3>();

    int index = 0;
    void Update()
    {
        if (index == nodes.Count) return;

        transform.position = Vector3.MoveTowards(transform.position, nodes[index], movementSpeed * Time.deltaTime);
        distanceMade += 2.5f * Time.deltaTime;
        if (transform.position == nodes[index]) 
        {
            index++;
        }

        if (index == 11)
        {
            gameHealth.ReduceHealth((int)GetComponent<EnemyHealth>().GetHealth());
            GetComponent<EnemyHealth>().health = -1;
        }
    }

































































    void AddDefaultPositions()
    {
        nodes.Add(new Vector3(30, 1.5f, -1));
        nodes.Add(new Vector3(30, 1.5f, 6));
        nodes.Add(new Vector3(39, 1.5f, 6));
        nodes.Add(new Vector3(39, 1.5f, 10));
        nodes.Add(new Vector3(25, 1.5f, 10));
        nodes.Add(new Vector3(25, 1.5f, 15));
        nodes.Add(new Vector3(36, 1.5f, 15));
        nodes.Add(new Vector3(36, 1.5f, 24));
        nodes.Add(new Vector3(42, 1.5f, 24));
        nodes.Add(new Vector3(42, 1.5f, 16));
        nodes.Add(new Vector3(54, 1.5f, 16));
    }
}
