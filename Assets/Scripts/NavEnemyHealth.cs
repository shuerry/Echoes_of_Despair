using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NavEnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public  int currentHealth;
    public AudioClip deadSFX;
    public Slider healthSlider;

    Animator animator;
    Transform deadTransform;
    bool isDead;


    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }
    // public LevelManager levelManager; // Reference to the LevelManager

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        animator = GetComponent<Animator>();

    }



    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
           
        }
        Debug.Log("Current health: " + currentHealth);

    }



    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }

        Debug.Log("Current health with loot: " + currentHealth);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            //hit by the projectile that is attached to the player 
            TakeDamage(10);


        }
    }
}
