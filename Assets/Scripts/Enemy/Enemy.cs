using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private int expToGive;


    private Vector3 direction;
    [SerializeField] private GameObject destroyEffect;



    void FixedUpdate()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {
            //Assume enemy sprite is faced right, face playe
        if (PlayerController.Instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        //walk towards player
        direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);

        } 
        // else if (collision.gameObject.CompareTag("Attack"))
        // {
        //     Destroy(gameObject);
        //     Instantiate(destroyEffect, transform.position, transform.rotation);
        // }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation); 
            PlayerController.Instance.GetExperience(expToGive);
        }
    }
}
