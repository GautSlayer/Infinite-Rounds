using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 1;
    public int damage = 0;

    private Rigidbody2D myRb;

	// Use this for initialization
	void Start () {
        myRb = GetComponent<Rigidbody2D>();

        //Apply a force (impulse) to give velocity to the projectile
        if(myRb != null)
        {
            myRb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Projectile miss a rigidbody2D");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         //Get the root gameobject we collided with
        GameObject collisionGO = collision.transform.root.gameObject;
        //Ignore collision with the boundary and contactZone (we want the usual collider for the enemy, not their contactZone)
        if(collisionGO.tag=="Projectile"||collisionGO.tag=="Item"||collisionGO.CompareTag("Boundary") || collision.CompareTag("ContactDamageZone") || collisionGO.CompareTag("Hole"))
        {
            return;
        }

        //If the projectile collided with an enemy, we apply damage
        if (collisionGO.CompareTag("Enemy"))
        {
            Health healthScript = collisionGO.GetComponentInChildren<Health>();
            if(healthScript != null)
            {
                healthScript.TakeDamage(this.damage);
            }
            else
            {
                Debug.Log("GameObject Tagged Enemy miss the HeatlhScript");
            }
        }

        //Destroy the projectile after a hit
        Destroy(this.gameObject);
       }
    
    }

