  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; 

    public int startingHealth = 100;
    public Slider healthSlider;
    public int currentHealth;
    public AudioClip deadSFX;
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

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

            PlayerDies();
        }
        Debug.Log("Current health " + currentHealth);
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead...");
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);

        transform.Rotate(-90, 0, 0, Space.Self);
        if (currentHealth <= 0)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }


    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }

        Debug.Log("Current health with loot " + currentHealth);
    }
    public void ResetHealth()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

}
