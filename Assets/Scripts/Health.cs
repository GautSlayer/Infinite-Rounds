using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int maxHealth = 100;
    public Slider healthBar;
    bool isInvicible=false;
    private int currentHealth;
    [SerializeField] AudioSource audioHit;
    private bool isDead;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        isDead = false;
	}

    public void TakeDamage(int damage)
    {
        if(isDead)
        {
            return;
        }
        if(!isInvicible){
            currentHealth -= damage;
            healthBar.value = currentHealth;
            audioHit.Play();
            //if Health point = 0, destroy the root gameobject (temporary solution)
            //need a proper death animation
            if (currentHealth <= 0)
            {
                isDead = true;
                GameManager gameManager = GameManager.instance;
                if(gameManager != null)
                {
                    if (this.transform.root.gameObject.CompareTag("Player")) //Player died
                    {
                        gameManager.PlayerDied();
                    }
                    else if (this.transform.root.gameObject.CompareTag("Enemy")) //Enemy died
                    {
                        gameManager.EnemyKilled();
                        Destroy(this.transform.root.gameObject);
                    }
                }
                else
                {
                    Debug.Log("Error, no GameManager found");
                }
            }
        }
    }

    public void Heal(int heal){
        if(currentHealth+heal>maxHealth){
            currentHealth=maxHealth;
        }
        else{
            currentHealth+=heal;
        }
        healthBar.value = currentHealth;
    }

    public void SwitchInviciblity(){
        isInvicible=!isInvicible;
    }
}
