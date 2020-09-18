using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTroopSelect : MonoBehaviour
{
    public int index = 1;

    PlacementScript ps;
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlacementScript>();
    }

    public void OnClick()
    { 
        if(ps.troopList[index].GetComponent<TroopScript>() != null)
        {
            if (ps.statBlock.GetCash() >= ps.troopList[index].GetComponent<TroopScript>().GetCost())
            {
                ps.selectedTroopIndex = index;
                ps.ResetPlaced();
            }
        }
        else
        {
            if (ps.statBlock.GetCash() >= ps.troopList[index].GetComponentInChildren<TroopScript>().GetCost())
            {
                ps.selectedTroopIndex = index;
                ps.ResetPlaced();
            }
        }

    }
}
