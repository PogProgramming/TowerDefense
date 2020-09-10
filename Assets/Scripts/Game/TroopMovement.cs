using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovement : MonoBehaviour
{
    public float distanceMade = 0;
    void Start()
    {
        AddDefaultPositions();
    }


    List<Vector3> nodes = new List<Vector3>();

    int index = 0;
    void Update()
    {

        if (index == nodes.Count) return;

        transform.position = Vector3.MoveTowards(transform.position, nodes[index], 2.5f * Time.deltaTime);
        distanceMade += 2.5f * Time.deltaTime;
        if (transform.position == nodes[index]) 
        {
            index++;
        }

        if (index == 11)
        {
            transform.position = new Vector3(30, 1.5f, -1);
            index = 0;
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
