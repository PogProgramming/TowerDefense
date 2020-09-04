using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTroopSelect : MonoBehaviour
{
    public int index = 1;

    PlacementScript ps;
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlacementScript>();
    }

    public void OnClick()
    {
        Debug.Log("Yes");
        ps.selectedTroopIndex = index;
        ps.ResetPlaced();
    }
}
