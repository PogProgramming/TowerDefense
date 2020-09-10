using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeInterface ui;
    public void UpgradeTroop()
    {
        ui.HideUpgradeOptions();
        ui.OpenPurchaseButtons();

        ui.LevelUp();
    }
}
