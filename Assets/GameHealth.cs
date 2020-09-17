using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHealth : MonoBehaviour
{
    public Text healthText;
    public Image healthImage;
    private RectTransform healthRect;

    public int health = 300;

    private bool gameOver = false;
    public GameObject gameOverObj;

    void Start()
    {
        healthRect = healthImage.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + " / 300";
        healthRect.sizeDelta = new Vector2(healthImage.rectTransform.rect.width, health);

        if (health < 0)
        {
            gameOver = true;
            health = 0;
            KillGame();
        }
    }

    public void ReduceHealth(int reduceAmount) { health -= reduceAmount; }
    public bool IsGameOver() { return gameOver; }
    public void KillGame()
    {
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in existingEnemies)
        {
            enemy.GetComponent<EnemyHealth>().Kill(false);
        }

        GetComponent<Spawner>().StopWaves();
        gameOverObj.SetActive(true);
    }

    public void RestartGame()
    {
        health = 300;

        Stats stats = GetComponent<Stats>();
        stats.cash = 550;
        stats.activeEnemies = 0;
        stats.killedEnemies = 0;
        stats.SetWave(0);

        Spawner spawn = GetComponent<Spawner>();
        spawn.ManualAdjust(1);
        spawn.StartWaves();

        gameOverObj.SetActive(false);

        GameObject[] existingTroops = GameObject.FindGameObjectsWithTag("Troop");
        foreach (GameObject troop in existingTroops)
        {
            Destroy(troop);
        }
    }
}
