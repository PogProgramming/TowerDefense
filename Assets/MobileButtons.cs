using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtons : MonoBehaviour
{
    public GameObject btn_Edit;
    public GameObject btn_View;

    public List<GameObject> mobileButtons = new List<GameObject>();

    void Start()
    {
        foreach (GameObject btn in GameObject.FindGameObjectsWithTag("MobileControls"))
        {
            mobileButtons.Add(btn);
        }
    }
    public void RunEditMode()
    {
        btn_Edit.SetActive(false);
        foreach (GameObject btn in mobileButtons)
        {
            btn.SetActive(false);
        }
        btn_View.SetActive(true);
    }

    public void RunViewMode()
    {
        btn_Edit.SetActive(true);
        btn_View.SetActive(false);
        foreach (GameObject btn in mobileButtons)
        {
            btn.SetActive(true);
        }
    }
}
