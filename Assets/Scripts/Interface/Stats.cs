using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private int placedTroops = 0;
    [SerializeField] private int maxPlacedTroops = 20;

    public Text txt_AvailableTroops;

    public Text txt_Wave;
    int wave = 0;

    public Text txt_Cash;
    public long cash = 0;

    public Text txt_ActiveEnemies;
    public long activeEnemies = 0;

    public Text txt_KilledEnemies;
    public long killedEnemies = 0;

    int lastViewUpgrade = 0;
    // Update is called once per frame
    void Update()
    {
        txt_Wave.text = "Wave " + wave;
        txt_Cash.text = "Cash: " + cash;
        txt_ActiveEnemies.text = "Active Enemies: " + activeEnemies;
        txt_KilledEnemies.text = "Killed Enemies: " + killedEnemies;

        txt_AvailableTroops.text = "Available Troops: " + (maxPlacedTroops - placedTroops);
        if (lastViewUpgrade != wave)
        {
            if (wave % 5 == 0)
            {
                maxPlacedTroops++;
                lastViewUpgrade = wave;
            }
        }
    }

    public long GetCash() { return cash; }

    public void AdjustCash(long _incrementation)
    {
        cash += _incrementation;
    }

    public void RemoveCash(long _decrementation) {
        Debug.Log("ADJUSTING CASH BY " + _decrementation); 
        cash -= _decrementation; 
    }

    public void SetWave(int wave) { this.wave = wave; }

    public int GetPlacedTroops() { return placedTroops; }
    public int GetMaxTroops() { return maxPlacedTroops; }
    public void IncrementPlacedTroops(int _increment) { placedTroops += _increment; }
    public void SetMaxPlacedTroops(int _set) { maxPlacedTroops = _set; }
}
