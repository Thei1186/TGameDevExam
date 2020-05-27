using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // The speed with which the projectile moves
    public float speed;

    Rigidbody2D rb;

    // The player
    GameObject target;

    // The direction the projectile moves
    Vector2 moveDirection;

    // The amount damage the projectile inflict
    public int damage;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.1f);
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage);  
        }
    }
   
}
