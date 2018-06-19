using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    //public static int roundNumber = 1;

    public GameObject enemyPrefab;//Will be replaced with list of enemies in waves
    public int maxMobAtATIme = 100;
    public int numbOfEnemyPerRound = 10;
    public float spawnRate = 1;
    public GameObject player;
    public float randomRadiusSpawn;

    //Gestion perturbation
    public float initialPerturbationOffset=5;
    public float perturbationFrequency=20;
    public float lastPerturbation=0;

    public Text killcount;
    public Text roundNumberText;

    private float nextSpawn;
    private int enemySpawnedCount;
    private int enemyKilledCount;
    private bool playerAlive;
    private int roundNumber;

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
        lastPerturbation=Time.time+initialPerturbationOffset;
        maxMobAtATIme = MobWaves.maxMobAtATime;
        numbOfEnemyPerRound = MobWaves.numberOfEnemyInTheRound;
        roundNumber = MobWaves.roundNumber;
        spawnRate = MobWaves.spawnRate;
        roundNumberText.text = "Round " + roundNumber;
        killcount.text = "0 / " + numbOfEnemyPerRound;
    }

    // Update is called once per frame
    void Update () {
        // Gestion dysfonctionnement
        if(Time.time>=lastPerturbation+perturbationFrequency+Random.Range(-5.0f,5.0f)){
            Debug.Log("   ");
            lastPerturbation=Time.time;
            player.GetComponent<PlayerControler>().StartPerturbation(Random.Range(1,6));
            
        }
        //Temporary infinite spawning of enemies
        if(playerAlive)
        {
            if(enemySpawnedCount < numbOfEnemyPerRound)
            {
                if(Time.time >= nextSpawn && enemySpawnedCount - enemyKilledCount < maxMobAtATIme)
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
                    MobWaves.Buff();
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
    }

    public void EnemyKilled()
    {
        enemyKilledCount++;
        killcount.text = enemyKilledCount + " / " + numbOfEnemyPerRound;
    }

    public void PlayerDied()
    {
        playerAlive = false;
    }
}
