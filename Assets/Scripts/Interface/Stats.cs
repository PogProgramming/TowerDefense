using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text txt_Wave;
    int wave = 0;

    public Text txt_Cash;
    public long cash = 0;

    public Text txt_ActiveEnemies;
    public long activeEnemies = 0;

    public Text txt_KilledEnemies;
    public long killedEnemies = 0;

    // Update is called once per frame
    void Update()
    {
        txt_Wave.text = "Wave " + wave;
        txt_Cash.text = "Cash: " + cash;
        txt_ActiveEnemies.text = "Active Enemies: " + activeEnemies;
        txt_KilledEnemies.text = "Killed Enemies: " + killedEnemies;
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
}
