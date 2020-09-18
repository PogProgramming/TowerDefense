using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    public float destroyAfterSecs = 5f;

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyAfterSecs)
            Destroy(transform.gameObject);
    }
}
