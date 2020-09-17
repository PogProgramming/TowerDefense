using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePopTransition : MonoBehaviour
{
    public float speed = 1.3f;

    float currentXScale = 1;
    public float maxXScale = 1.5f;

    float currentYScale = 1;
    public float maxYScale = 1.5f;

    bool adding = true;

    void Update()
    {
        if (adding)
        {
            currentXScale += Time.deltaTime * speed;
            currentYScale += Time.deltaTime * speed;
        }
        else
        {
            currentXScale -= Time.deltaTime * speed;
            currentYScale -= Time.deltaTime * speed;
        }

        if (currentXScale > maxXScale) adding = false;
        else if (currentXScale < 1f) adding = true;

        transform.localScale = new Vector2(currentXScale, currentYScale);
    }
}
