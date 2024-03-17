using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public int currentHealth;
    public AudioClip deadSFX;

    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
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

            AudioSource.PlayClipAtPoint(deadSFX, transform.position);

        }

    }


    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Pipe"))
        {
            TakeDamage(10);
        }
    }
    public void ResetHealth()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }
}
