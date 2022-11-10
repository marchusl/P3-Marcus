using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] enemiesRemaining;
    public float enemyCount;
    public float enemySpeed;
    private float enemyMax;
    private float rdnNumber;
    private float countDown = 5f;
    private bool spawnrate = false;
    private Transform north;
    private Transform south;
    private Transform east;
    private Transform west;

    public TMP_Text waveInfo;

    void Start()
    {
        FindSpawnPoints();
        enemyMax = 5;
        enemySpeed = 5f;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartWave();
        }
        CheckForEnemies();
        WaveDone();
        if (enemyCount <= 0)
        {
            waveInfo.text = "Start wave with M button!".ToString();
        }
        else if (enemyCount > 0)
        {
            waveInfo.text = "There is " + enemyCount + " enemies remaining".ToString();
        }
        if (spawnrate == true)
        {
            countDown -= Time.deltaTime;
        }
        if (countDown <= 0)
        {
            StartWave();
            countDown = 5;
        }
    }

    void FindSpawnPoints()
    {
        north = GameObject.Find("SpawnNorth").transform;
        south = GameObject.Find("SpawnSouth").transform;
        east = GameObject.Find("SpawnEast").transform;
        west = GameObject.Find("SpawnWest").transform;
    }

    void StartWave()
    {
        spawnrate = true;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemyCount < enemyMax)
        {
            DecidePalcement();
            enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy");
            yield return new WaitForSeconds(0.3f);
        }
        enemySpeed += 1;
        enemyMax += Random.Range(2, 5);
        StopCoroutine(SpawnEnemies());
    }

    void DecidePalcement()
    {
        rdnNumber = Random.Range(1, 5);
        if (rdnNumber == 1)
        {
            SpawnNorth();
        }
        if (rdnNumber == 2)
        {
            SpawnSouth();
        }
        if (rdnNumber == 3)
        {
            SpawnEast();
        }
        if (rdnNumber == 4)
        {
            SpawnWest();
        }
        else
        {
            return;
        }
    }

    public void SpawnNorth()
    {
        Instantiate(enemy, new Vector3(north.position.x, 0.5f, north.position.z), Quaternion.identity);
        enemyCount += 1;
    }

    void SpawnSouth()
    {
        Instantiate(enemy, new Vector3(south.position.x, 0.5f, south.position.z), Quaternion.identity);
        enemyCount += 1;
    }
    void SpawnWest()
    {
        Instantiate(enemy, new Vector3(west.position.x, 0.5f, west.position.z), Quaternion.identity);
        enemyCount += 1;
    }
    void SpawnEast()
    {
        Instantiate(enemy, new Vector3(east.position.x, 0.5f, east.position.z), Quaternion.identity);
        enemyCount += 1;
    }

    public void CheckForEnemies()
    {
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void WaveDone()
    {
        if (enemiesRemaining.Length == 0)
        {
            waveInfo.text = "Start wave with M button!".ToString();
            enemyCount = 0;
            StopCoroutine(SpawnEnemies()); 
        }
    }
}
