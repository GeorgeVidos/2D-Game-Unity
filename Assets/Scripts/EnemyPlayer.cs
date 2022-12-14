using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private int EnemyHealth = 100;
    public int currentHealth;
    private void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = EnemyHealth;
    }


    //collide form the side -20 health
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {


            currentHealth = currentHealth - 20;

            checkHealth();
        }
    }


    //top collider instant death
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Die();
        }
    }


    //damage from the bullet
    public void TakeDamageFromBullet(int damage)
    {
        currentHealth -= damage;

        checkHealth();
    }



    private void checkHealth()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("DestroyEnemy", 0.3f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


}
