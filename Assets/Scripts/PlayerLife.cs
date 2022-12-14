using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] AudioSource DeathSoundEffect;
    private int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
           
            currentHealth = currentHealth - 20;

            checkHealth();
        } else if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            if (currentHealth == maxHealth)
            {
                currentHealth = maxHealth;
                checkHealth();
            }
            else
            {
                currentHealth = currentHealth + 20;
                checkHealth();

            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth = currentHealth + 20;
            checkHealth();
        }

    }
    private void checkHealth()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    private void Die()
    {
        healthBar.SetHealth(0);
        DeathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("RestartLevel", 1.5f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
