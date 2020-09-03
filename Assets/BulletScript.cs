using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    public float speed = 30f;
    public void SetTargetPosition(Vector3 start, Vector3 target)
    {
        transform.position = start;
        targetPosition = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != Vector3.zero)
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
                Destroy(transform.gameObject);
        }
    }
}
