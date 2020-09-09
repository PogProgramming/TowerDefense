using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Stats statSystem;
    public GameObject spawnType;

    void Start()
    {
        statSystem = GetComponent<Stats>();
        statSystem.SetWave(1);
    }

    [SerializeField] int amountOfTroops = 0;
    [SerializeField] float timeBetweenSpawns = 0;

    void RunWave(int wave)
    {
        switch (wave)
        {
            case 1:
                amountOfTroops = 10;
                timeBetweenSpawns = 3;
                break;
            case 2:
                amountOfTroops = 12;
                timeBetweenSpawns = 3;
                break;
            case 3:
                amountOfTroops = 15;
                timeBetweenSpawns = 3;
                break;
            case 4:
                amountOfTroops = 18;
                timeBetweenSpawns = 3;
                break;
            case 5:
                amountOfTroops = 23;
                timeBetweenSpawns = 3;
                break;
            case 6:
                amountOfTroops = 28;
                timeBetweenSpawns = 3;
                break;
            case 7:
                amountOfTroops = 40;
                timeBetweenSpawns = 3;
                break;
            case 8:
                amountOfTroops = 50;
                timeBetweenSpawns = 3;
                break;
            case 9:
                amountOfTroops = 59;
                timeBetweenSpawns = 3;
                break;
            case 10:
                amountOfTroops = 66;
                timeBetweenSpawns = 3;
                break;
            case 11:
                amountOfTroops = 78;
                timeBetweenSpawns = 3;
                break;
            case 12:
                amountOfTroops = 88;
                timeBetweenSpawns = 3;
                break;
            case 13:
                amountOfTroops = 99;
                timeBetweenSpawns = 3;
                break;
            case 14:
                amountOfTroops = 108;
                timeBetweenSpawns = 3;
                break;
            case 15:
                amountOfTroops = 114;
                timeBetweenSpawns = 3;
                break;
            case 16:
                amountOfTroops = 119;
                timeBetweenSpawns = 3;
                break;
            case 17:
                amountOfTroops = 130;
                timeBetweenSpawns = 3;
                break;
            case 18:
                amountOfTroops = 145;
                timeBetweenSpawns = 3;
                break;
            case 19:
                amountOfTroops = 158;
                timeBetweenSpawns = 3;
                break;
            case 20:
                amountOfTroops = 170;
                timeBetweenSpawns = 3;
                break;
            case 21:
                amountOfTroops = 190;
                timeBetweenSpawns = 3;
                break;
            case 22:
                amountOfTroops = 212;
                timeBetweenSpawns = 3;
                break;
            case 23:
                amountOfTroops = 235;
                timeBetweenSpawns = 3;
                break;
            case 24:
                amountOfTroops = 255;
                timeBetweenSpawns = 3;
                break;
            case 25:
                amountOfTroops = 287;
                timeBetweenSpawns = 3;
                break;
            case 26:
                amountOfTroops = 310;
                timeBetweenSpawns = 3;
                break;
            case 27:
                amountOfTroops = 339;
                timeBetweenSpawns = 3;
                break;
            case 28:
                amountOfTroops = 357;
                timeBetweenSpawns = 3;
                break;
            case 29:
                amountOfTroops = 372;
                timeBetweenSpawns = 3;
                break;
            case 30:
                amountOfTroops = 391;
                timeBetweenSpawns = 3;
                break;
            case 31:
                amountOfTroops = 425;
                timeBetweenSpawns = 3;
                break;
            case 32:
                amountOfTroops = 464;
                timeBetweenSpawns = 3;
                break;
            case 33:
                amountOfTroops = 499;
                timeBetweenSpawns = 3;
                break;
            case 34:
                amountOfTroops = 540;
                timeBetweenSpawns = 3;
                break;
            case 35:
                amountOfTroops = 584;
                timeBetweenSpawns = 3;
                break;
            case 36:
                amountOfTroops = 633;
                timeBetweenSpawns = 3;
                break;
            case 37:
                amountOfTroops = 663;
                timeBetweenSpawns = 3;
                break;
            case 38:
                amountOfTroops = 700;
                timeBetweenSpawns = 3;
                break;
            case 39:
                amountOfTroops = 745;
                timeBetweenSpawns = 3;
                break;
            case 40:
                amountOfTroops = 800;
                timeBetweenSpawns = 3;
                break;
        }
        timeBetweenSpawns = 3.0f / (float)wave;
        waveSet = true;
    }

    [SerializeField] int currentWave = 1;
    [SerializeField] int maxWaves = 40;
    [SerializeField] bool waveSet = false;
    [SerializeField] bool waveRunning = false;

    [SerializeField] float timer = 0;

    int spawnedCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ManualAdjust(currentWave + 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ManualAdjust(currentWave - 1);
        }

        if(currentWave != 0 && waveSet == false)
        {
            RunWave(currentWave);
            waveRunning = true;
        }

        if (waveRunning)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenSpawns && spawnedCount < amountOfTroops)
            {
                GameObject en = Instantiate(spawnType);
                en.GetComponent<EnemyHealth>().SetHealth(Time.deltaTime * (1000 * currentWave));
                en.GetComponent<EnemyHealth>().reward = (int)(Time.deltaTime * (10000 * currentWave));

                spawnedCount++;
                statSystem.activeEnemies++;
                timer = 0;
            }

            if(statSystem.activeEnemies == 0 && spawnedCount == amountOfTroops)
            {
                spawnedCount = 0;
                currentWave++;
                statSystem.SetWave(currentWave);

                waveRunning = false;
                waveSet = false;
            }
        }
    }

    void ManualAdjust(int _wave)
    {
        _wave = _wave - 1;
        currentWave = _wave;
        statSystem.SetWave(_wave);
        RunWave(_wave);
        spawnedCount = amountOfTroops;

        waveRunning = false;
        waveSet = false;
    }
}
