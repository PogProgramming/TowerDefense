using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text txt_ActiveEnemies;
    public Text txt_KilledEnemies;

    public long activeEnemies = 0;
    public long killedEnemies = 0;

    // Update is called once per frame
    void Update()
    {
        txt_ActiveEnemies.text = "Active Enemies: " + activeEnemies;
        txt_KilledEnemies.text = "Killed Enemies: " + killedEnemies;
    }
}
