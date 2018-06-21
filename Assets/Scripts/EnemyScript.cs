using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [SerializeField] GameObject item;
    [SerializeField] int seuilDrop=90;
    private MoveTowardPlayer moveTowardPlayerScript;
    private ContactDamage contactDamageScript;
    private Health healthScript;

	// Use this for initialization
	void Start () {
        //Get the componants
        moveTowardPlayerScript = GetComponent<MoveTowardPlayer>();
        contactDamageScript = GetComponent<ContactDamage>();
        healthScript = GetComponentInChildren<Health>();

        //Set the stats of the enemt with values from the EnemyStats class
        moveTowardPlayerScript.movementSpeed = Data.enemyStats.movementSpeed;
        contactDamageScript.attackSpeed = Data.enemyStats.attackSpeed;
        contactDamageScript.damage = Data.enemyStats.damage;
        healthScript.maxHealth = Data.enemyStats.maxHealth;
	}

    private void OnDestroy()
    {
        int rand = Random.Range(0,101);
        if(rand>seuilDrop){
            Instantiate(item,transform.position,transform.rotation);
        }
    }
}
