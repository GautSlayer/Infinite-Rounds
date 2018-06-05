using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

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
        moveTowardPlayerScript.movementSpeed = EnemyStats.movementSpeed;
        contactDamageScript.attackSpeed = EnemyStats.attackSpeed;
        contactDamageScript.damage = EnemyStats.damage;
        healthScript.maxHealth = EnemyStats.maxHealth;
	}
}
