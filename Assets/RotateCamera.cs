using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private GameObject cam;

    void Start()
    {
        cam = GameObject.Find("Player");
    }

    public void TurnLeft()
    {
        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y - 25, cam.transform.eulerAngles.z);
    }

    public void TurnRight()
    {
        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y + 25, cam.transform.eulerAngles.z);
    }
}
