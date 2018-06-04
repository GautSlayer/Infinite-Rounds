using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {

    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    public float fireRate = 1;

    private float nextFire;

	// Use this for initialization
	void Start () {
        nextFire = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            //Set available time for next shoot
            nextFire = Time.time + fireRate;

            //Instanciate the projectile
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        }
	}
}
