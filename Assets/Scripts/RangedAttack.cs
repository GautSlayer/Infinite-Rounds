using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {

    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    public enum Weapon {DEFAULT ,SHOTGUN,MACHINEGUN,AR,ROCKETLAUNCHER,FLAMETHROWER};
    
    [SerializeField] List<AudioClip> audioLib;
    
    //// Damage
    [Header("Damage")]
    [SerializeField]int damageMG= 20;
    [SerializeField]int damageShotgun= 20;
    [SerializeField]int damageAR= 20;
    [SerializeField]int damageDefault= 20;
    //// FireRate
    [Header("FireRate")]
    [SerializeField]float fireRateMG= 0.1f;
    [SerializeField]float fireRateShotgun= 1.0f;
    [SerializeField]float fireRateAR= 0.2f;
    [SerializeField]float fireRateDefault= 0.5f;
    //// Boost
    float damageBoost= 0 ;
    float FireRateBoost = 0 ;
    
    [SerializeField]Weapon actualWeapon = Weapon.DEFAULT;
    public float fireRate = 1;
    public bool handicap=false;
    private float nextFire;

    public Weapon ActualWeapon{get{return actualWeapon;}set{actualWeapon = value;}}

    [SerializeField] AudioSource audio;
    // Use this for initialization
    void Start () {
        nextFire = 0f;
        //audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        if (!handicap&&(Input.GetButton("AimHorizontal") || Input.GetButton("AimVertical")) && Time.time >= nextFire)
        {
            Vector3 t = projectileSpawn.localEulerAngles;
            Projectile ammo =projectilePrefab.GetComponent<Projectile>();
            switch(ActualWeapon){
                
                case Weapon.AR:
                break;
                case Weapon.SHOTGUN:
                    ammo.damage=(int)((damageShotgun)*(1+damageBoost));
                    Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.Euler(0,0,t.z));
                    
                    Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.Euler(0,0,t.z+20));
                    
                    Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.Euler(0,0,t.z-20));
                    audio.clip=audioLib[0];
                    audio.Play();
                    nextFire = Time.time + fireRateShotgun*(1-FireRateBoost);
                break;
                case Weapon.FLAMETHROWER:
                break;
                case Weapon.MACHINEGUN:
                    ammo.damage=(int)((damageMG)*(1+damageBoost));
                    Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.Euler(0,0,t.z+Random.Range(-25,25)));
                    nextFire = Time.time + fireRateMG*(1-FireRateBoost);
                     audio.clip=audioLib[2];
                    audio.Play();
                    //nextFire = Time.time + fireRate;
                break;
                case Weapon.ROCKETLAUNCHER:
                break;
                case Weapon.DEFAULT:
                    ammo.damage=(int)((damageDefault)*(1+damageBoost));
                    Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
                    nextFire = Time.time + fireRateDefault*(1-FireRateBoost);
                     audio.clip=audioLib[1];
                    audio.Play();
                break;
                default:
                //nextFire = Time.time + fireRate;
                break;
            }

            //Set available time for next shoot
            

            
        }
	}
    public void BoostDamage(float value){
        //projectilePrefab.GetComponent<Projectile>().damage+=value;
        damageBoost=value;
    }

    public void UnboostDamage(float value){
        //projectilePrefab.GetComponent<Projectile>().damage-=value;
        damageBoost=0;
    }

    public void BoostFireRate(float value){
        FireRateBoost=value;
    }

    public void UnboostFireRate(float value){
        FireRateBoost=0;
    }

    public void ChangeWeapon(Weapon wp){
        actualWeapon=wp;
    }
}
