using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossType bossType;
    public Transform player;
    public bool flipped = false;
    public Transform attackPoint;
    public LayerMask attackMask;
    public BossHealthBar healthBar;

    private int damage;
    private int enragedDamage;
    private int currentLife;
    private Animator animator;

    private void Start()
    {
        currentLife = bossType.startHealth;
        damage = bossType.damage;
        enragedDamage = bossType.damage * 2;
        healthBar.SetMaxHealth(bossType.startHealth);
        animator = gameObject.GetComponentInParent<Animator>();
    }

    public void LookAtPlayer()
    {
        Vector3 flip = transform.localScale;
        flip.z *= -1f;

        if (transform.position.x > player.position.x && !flipped)
        {
            transform.localScale = flip;
            transform.Rotate(0f, 180f, 0f);
            flipped = true;
        }
        else if (transform.position.x < player.position.x && flipped)
        {
            transform.localScale = flip;
            transform.Rotate(0f, 180f, 0f);
            flipped = false;
        }
    }

    public void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, bossType.minDistance, attackMask);
        if (collider != null)
        {
            collider.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void EnragedAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, bossType.minDistance, attackMask);
        if (collider != null)
        {
            collider.GetComponent<PlayerHealth>().TakeDamage(enragedDamage);
        }
    }

    public void TakeDamage(int attackDamage)
    {
        currentLife -= attackDamage;
        animator.SetTrigger("GetHurt");
        healthBar.SetHealth(currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        StartCoroutine(StartVictoryAfterDelay(5));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, bossType.minDistance);
    }

    IEnumerator StartVictoryAfterDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.GetInstance().setVictory();
    }
}
