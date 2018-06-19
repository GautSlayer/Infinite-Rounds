using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {

    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    enum Weapon {DEFAULT ,SHOTGUN,AR,MACHINEGUN,ROCKETLAUNCHER,FLAMETHROWER};
    
    Weapon actualWeapon = Weapon.DEFAULT;
    public float fireRate = 1;
    public bool handicap=false;
    private float nextFire;

	// Use this for initialization
	void Start () {
        nextFire = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!handicap&&(Input.GetButton("AimHorizontal") || Input.GetButton("AimVertical")) && Time.time >= nextFire)
        {
            //Set available time for next shoot
            nextFire = Time.time + fireRate;

            //Instanciate the projectile
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        }
	}
    public void BoostDamage(int value){
        projectilePrefab.GetComponent<Projectile>().damage+=value;
    }

    public void UnboostDamage(int value){
        projectilePrefab.GetComponent<Projectile>().damage-=value;
    }

    public void BoostFireRate(float value){
        fireRate-=value;
    }

    public void UnboostFireRate(float value){
         fireRate+=value;
    }

}
