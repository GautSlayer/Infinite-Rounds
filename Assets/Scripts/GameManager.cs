using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject enemyPrefab;//Will be replaced with list of enemies in waves
    public GameObject player;
    public float spawnRate = 1;
    public float randomRadiusSpawn;

    private float nextSpawn;

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
        nextSpawn = 0f;
    }

    // Update is called once per frame
    void Update () {
        //Temporary infinite spawning of enemies
		while(player != null && Time.time >= nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Vector3 randonOnSphere = Random.onUnitSphere * randomRadiusSpawn;
            randonOnSphere.z = 0;
            Instantiate(enemyPrefab, player.transform.position + randonOnSphere, Quaternion.identity);
        }
	}
}
