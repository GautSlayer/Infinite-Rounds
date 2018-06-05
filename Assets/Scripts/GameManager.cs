using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public static int roundNumber = 0;

    public GameObject enemyPrefab;//Will be replaced with list of enemies in waves
    public int numbOfEnemyPerRound = 10;
    public GameObject player;
    public float spawnRate = 1;
    public float randomRadiusSpawn;

    private float nextSpawn;
    private int enemySpawnedCount;
    private int enemyKilledCount;
    private bool playerAlive;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        NewRound();
    }

    // Update is called once per frame
    void Update () {
        //Temporary infinite spawning of enemies
        if(playerAlive)
        {
            if(enemySpawnedCount < numbOfEnemyPerRound)
            {
                if(Time.time >= nextSpawn)
                {
                    nextSpawn = Time.time + spawnRate;
                    enemySpawnedCount++;
                    Vector3 randonOnCircle = Random.insideUnitCircle.normalized * randomRadiusSpawn;
                    Instantiate(enemyPrefab, player.transform.position + randonOnCircle, Quaternion.identity);
                }
            }
            else // All enemy have spawned
            {
                if(enemyKilledCount == numbOfEnemyPerRound) // Round won
                {
                    EnemyStats.Buff();
                    Round_End.lastRoundWin = true;
                    SceneManager.LoadScene("Round_End");
                }
            }
        }
        else //Round lost
        {
            Round_End.lastRoundWin = false;
            SceneManager.LoadScene("Round_End");
        }
	}

    private void NewRound()
    {
        nextSpawn = 0f;
        enemySpawnedCount = 0;
        enemyKilledCount = 0;
        playerAlive = true;
        roundNumber++;
    }

    public void EnemyKilled()
    {
        enemyKilledCount++;
    }

    public void PlayerDied()
    {
        playerAlive = false;
    }
}
