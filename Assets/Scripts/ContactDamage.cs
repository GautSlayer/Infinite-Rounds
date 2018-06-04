using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {

    public float attackSpeed = 1;
    public int damage;

    private float nextAttack;
    private GameObject player;

    private void Start()
    {
        nextAttack = 0f;
        player = null;
    }

    private void Update()
    {
        //if player is in the damage zone, and we can attack, we deal damage to the player
        if (player != null && Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            Health healthScript = player.GetComponentInChildren<Health>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(this.damage);
            }
            else
            {
                Debug.Log("Player is missing the Health Script");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get the root gameobject we collided with
        GameObject collisionGO = collision.transform.root.gameObject;

        //Ignore collision with the boundary
        if (collisionGO.CompareTag("Boundary") || collisionGO.CompareTag("Enemy"))
        {
            return;
        }

        //Mark that the player is in the damage zone
        if (collisionGO.CompareTag("Player"))
        {
            player = collisionGO;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Get the root gameobject we collided with
        GameObject collisionGO = collision.transform.root.gameObject;

        //Ignore collision with the boundary
        if (collisionGO.CompareTag("Boundary") || collisionGO.CompareTag("Enemy"))
        {
            return;
        }

        //Mark that the player exited the damage zone
        if (collisionGO.CompareTag("Player"))
        {
            player = null;
        }
    }
}
