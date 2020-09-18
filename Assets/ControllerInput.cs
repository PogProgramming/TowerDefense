using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class ControllerInput : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    FreeFlyCamera cameraScript;
    public GameObject crosshair;

    public Button firstSelect;
    void Start()
    {
        cameraScript = Camera.main.GetComponent<FreeFlyCamera>();

        if (cameraScript.controllerConnected)
        {
            crosshair.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("YButton"))
        {
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            firstSelect.Select();
        }
    }
}
