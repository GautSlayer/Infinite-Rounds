using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int maxHealth = 100;
    public Slider healthBar;

    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        //if Health point = 0, destroy the root gameobject (temporary solution)
        //need a proper death animation
        if(currentHealth <= 0)
        {
            Destroy(this.transform.root.gameObject);
        }
    }
}
