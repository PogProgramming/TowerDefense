using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour
{
    public GameObject editPanel;

    public GameObject upgradePanel;
    public Text txt_level;
    public Text txt_damage;
    public Text txt_cooldown;
    public Text txt_view_radius;
    public Text txt_upgradecost;

    public GameObject troop;

    void Start()
    {

    }


    void Update()
    {
        if (troop != null)
        {
            Debug.Log("troopname: " + troop.name);
        }
    }
    public void OpenPurchaseButtons() { editPanel.SetActive(true); }
    public void HidePurchaseButtons() { editPanel.SetActive(false); }
    public void OpenUpgradeOptions() { upgradePanel.SetActive(true); }
    public void HideUpgradeOptions() { upgradePanel.SetActive(false); }

    public void SetTroopStats(GameObject _troop)
    {
        troop = _troop;
        TroopScript ts = _troop.GetComponent<TroopScript>();

        txt_level.text = "Level: " + ts.GetLevel();
        txt_damage.text = "Damage: " + ts.GetDamage();
        txt_cooldown.text = "Cooldown: " + ts.GetShootCooldown();
        txt_view_radius.text = "View Radius: " + ts.GetViewRadius();
        txt_upgradecost.text = "Upgrade $" + ts.GetUpgradeCost();
    }

    public void LevelUp()
    {
        troop.GetComponent<TroopScript>().LevelUp();
    }

    private bool updated = false;
    public bool IsUpdated() { return updated; }
    public GameObject GetUpdatedTroop()
    {
        updated = false; // reset it
        return troop;
    }

    public ref GameObject Returntroop()
    {
        return ref troop;
    }
}
