using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : MonoBehaviour
{
    public TroopScript TS;
    public UpgradeInterface upgradeUI;

    public Text txt_Mode;

    void Start()
    {
        txt_Mode.text = "First";
    }

    int index = 0;

    public void CycleMode()
    {
        TS = upgradeUI.troop.GetComponent<TroopScript>();
        if(index == 0)
        {
            TS.ChangeAttackMode(TroopScript.AttackMode.Closest);

            txt_Mode.text = "Closest";
            index++;
        }
        else if(index == 1)
        {
            TS.ChangeAttackMode(TroopScript.AttackMode.Strongest);

            txt_Mode.text = "Strongest";
            index++;
        }
        else if(index == 2)
        {
            TS.ChangeAttackMode(TroopScript.AttackMode.First);

            txt_Mode.text = "First";
            index = 0;
        }
    }
}
