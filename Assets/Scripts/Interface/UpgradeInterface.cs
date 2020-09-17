using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour
{
    public bool upgrading = false;

    public Stats stats;

    public GameObject editPanel;

    public GameObject upgradePanel;
    public Text txt_level;
    public Text txt_damage;
    public Text txt_cooldown;
    public Text txt_view_radius;
    public Text txt_upgradecost;

    public GameObject troop;
    bool changed = false;
    public GameObject viewDistanceHighlighter;

    void Start()
    {

    }

    void Update()
    {
        if (upgradePanel.activeSelf)
        {
            float scale = troop.GetComponent<TroopScript>().GetViewRadius() * 2;
            viewDistanceHighlighter.transform.localScale = new Vector3(scale, 0.001f, scale);

            Vector3 position = new Vector3(troop.transform.position.x, 1f, troop.transform.position.z);
            viewDistanceHighlighter.transform.position = position;
            viewDistanceHighlighter.SetActive(true);
        }
    }

    public void OpenPurchaseButtons() { editPanel.SetActive(true); changed = true; }
    public void HidePurchaseButtons() { editPanel.SetActive(false); changed = true; }
    public void OpenUpgradeOptions() { upgradePanel.SetActive(true); changed = true; upgrading = true; }
    public void HideUpgradeOptions() { upgradePanel.SetActive(false); changed = true; upgrading = false; }

    public void HideUpgradeAndOpenPurchase()
    {
        HideUpgradeOptions();
        OpenPurchaseButtons();
        changed = true;

        viewDistanceHighlighter.SetActive(false);
    }

    public void SetTroop(GameObject _troop)
    {
        troop = _troop;

        SetTroopStats();
    }
    public void SetTroopStats()
    {
        TroopScript ts = troop.GetComponent<TroopScript>();

        txt_level.text = "Level: " + ts.GetLevel();
        txt_damage.text = "Damage: " + ts.GetDamage();
        txt_cooldown.text = "Cooldown: " + ts.GetShootCooldown();
        txt_view_radius.text = "View Radius: " + ts.GetViewRadius();
        txt_upgradecost.text = "Upgrade $" + ts.GetUpgradeCost();
    }

    public void LevelUp()
    {
        TroopScript ts = troop.GetComponent<TroopScript>();
        long cost = ts.GetUpgradeCost();
        Debug.Log(cost);
        if (stats.GetCash() >= cost && ts.GetLevel() != 69)
        {
            stats.AdjustCash(-cost);
            ts.LevelUp();
        }

        SetTroopStats();

        float scale = troop.GetComponent<TroopScript>().GetViewRadius() * 2;
        viewDistanceHighlighter.transform.localScale = new Vector3(scale, 0.001f, scale);
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
