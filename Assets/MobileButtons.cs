using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtons : MonoBehaviour
{
    public GameObject btn_Edit;
    public GameObject btn_View;

    public GameObject editPanel;

    public List<GameObject> mobileButtons = new List<GameObject>();

    void Start()
    {
        foreach (GameObject btn in GameObject.FindGameObjectsWithTag("MobileControls"))
        {
            mobileButtons.Add(btn);
        }

        if (Application.isMobilePlatform) editPanel.SetActive(false);
    }
    public void RunEditMode()
    {
        editPanel.SetActive(true);
        btn_Edit.SetActive(false);
        foreach (GameObject btn in mobileButtons)
        {
            btn.SetActive(false);
        }
        btn_View.SetActive(true);

        Camera.main.GetComponent<FreeFlyCamera>().isInViewMode = false;
    }

    public void RunViewMode()
    {
        editPanel.SetActive(false);
        btn_Edit.SetActive(true);
        btn_View.SetActive(false);
        foreach (GameObject btn in mobileButtons)
        {
            btn.SetActive(true);
        }

        Camera.main.GetComponent<FreeFlyCamera>().isInViewMode = true;
    }
}
