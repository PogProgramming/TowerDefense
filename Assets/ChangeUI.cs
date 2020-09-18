using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour
{
    public GameObject ChangeFrom;
    public GameObject ChangeTo;

    public void ChangeScreen()
    {
        ChangeTo.SetActive(true);
        ChangeFrom.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("BButton"))
        {
            ChangeScreen();
        }    
    }
}
