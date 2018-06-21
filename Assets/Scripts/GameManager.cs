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
    private bool dying = false;
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
        maxMobAtATIme = Data.mobWaves.maxMobAtATime;
        numbOfEnemyPerRound = Data.mobWaves.numberOfEnemyInTheRound;
        roundNumber = Data.mobWaves.roundNumber;
        spawnRate = Data.mobWaves.spawnRate;
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
                    StartCoroutine("SpawnEnemy");
                }
            }
            else // All enemy have spawned
            {
                if(enemyKilledCount == numbOfEnemyPerRound) // Round won
                {
                    Data.mobWaves.Buff();
                    Data.enemyStats.Buff();
                    Data.SaveData();
                    Round_End.lastRoundWin = true;
                    SceneManager.LoadScene("Round_End");
                }
            }
        }
        else if(!dying) //Round lost
        {
            // Death animation
            StartCoroutine("DieBeforeNewRound");
            dying = true;
        }
	}

    private IEnumerator SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;

        while (Vector2.Distance(player.transform.position, spawnPosition) < 7f)
        {
            if(Random.value > .5f) // on the left or right side of the map
            {
                if(Random.value > .5f) // left
                {
                    spawnPosition.x = -11f;
                }
                else // right
                {
                    spawnPosition.x = 9f;
                }

                spawnPosition.y = Random.Range(-11, 1);
            }
            else // on the top or bottom side of the map
            {
                if (Random.value > .5f) // top
                {
                    spawnPosition.y = 1f;
                }
                else // bottom
                {
                    spawnPosition.y = -11f;
                }

                spawnPosition.x = Random.Range(-11, 9);
            }
        }

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        yield return null;
    }

    private IEnumerator DieBeforeNewRound()
    {
        // Trigger the death animation
        Animator playerAnimator = player.GetComponentInChildren<Animator>();
        if (playerAnimator)
        {
            playerAnimator.SetTrigger("Die");
        }
        else
        {
            Debug.LogError("Player retrieved from GameManager has no Animator !");
        }

        // Disable player movements during death animation
        player.GetComponent<PlayerControler>().PlayerDying();

        // Wait for the death animation to run
        yield return new WaitForSeconds(3f);

        // Reload the scene
        Round_End.lastRoundWin = false;
        SceneManager.LoadScene("Round_End");
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
