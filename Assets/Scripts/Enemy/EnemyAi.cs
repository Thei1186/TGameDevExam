using Pathfinding;
using System;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform attackPoint;
    public Transform target;
    public Transform enemyGFX;
    public EnemyType enemyType;

    public float maxSpeed;
    public float nextWaypointDistance;
    private float currentSpeed;
    
    private float attackTimer = 5f;
    private int currentHealth;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Path path;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector2 direction;
    private Seeker seeker;
    private Rigidbody2D rb;
    private bool isTargetInRange = false;
    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
    void Start()
    {
        currentSpeed = maxSpeed;
        currentHealth = enemyType.StartHealth;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = enemyGFX.GetComponent<SpriteRenderer>();
        animator = enemyGFX.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator.runtimeAnimatorController = enemyType.RuntimeAnimatorController;


        // This is for managing the change in perspective, mass is mainly to avoid excessive shoving while the game is in top down mode
        if (GameManager.GetInstance().isPlatformer)
        {
            rb.mass = 1;
            rb.gravityScale = 1;
        }
        else
        {
            rb.mass = 100;
            rb.gravityScale = 0;
        }

    }

    // Used for the enemy to take take and also calls the die method when health reaches zero or below
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            animator.SetTrigger("GetHurt");
        } else
        {
            Die();
        }
    }

   // Checks if the path is done being calculated or is null and calls startPath if it is.
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceToPlayer <= enemyType.DetectRange)
        {
            if (isTargetInRange == false)
            {
                isTargetInRange = true;
                InvokeRepeating("UpdatePath", 0f, 2f);
            }
        }
        else
        {
            isTargetInRange = false;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

         direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;


        if (direction.x >= 0.01f)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        else if (direction.x <= -0.01f)
        {
            transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        }


        if (Vector2.Distance(enemyGFX.transform.position, target.transform.position) > enemyType.MinDistance)
        {
            currentSpeed = maxSpeed;
            Movement();
        }
        else if (attackTimer >= enemyType.AttackSpeed)
        {          
            TryToAttack();
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    // Handles movement depending on whether it is currently platformer movement
    private void Movement()
    {
        animator.SetFloat("MoveSpeed", currentSpeed);
        if (GameManager.GetInstance().isPlatformer != true)
        {
            rb.MovePosition(rb.position + direction * currentSpeed * Time.fixedDeltaTime);
        } else
        {
            Vector3 platformMovement = new Vector3(direction.x, 0f, 0f);
            transform.position += platformMovement * Time.deltaTime * currentSpeed;
        }
        
    }

    // Shoots a Raycast to see there if there is a target in the AI's path and attacks if there is.
    // Causes some issues in regards to pathfinding, but otherwise works as inteded
    private void TryToAttack()
    {
        currentSpeed = 0;
        animator.SetFloat("MoveSpeed", currentSpeed);
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, direction, enemyType.MinDistance);

        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                attackTimer = 0;
                animator.SetTrigger("Shoot");
                float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Instantiate(enemyType.ProjectilePrefab, attackPoint.position, Quaternion.Euler(0f, 0f, rotation))
                    .GetComponent<Rigidbody2D>().AddForce(target.position * enemyType.ShootingPower * Time.fixedDeltaTime);
            }
        }
    
    }

    // Disables the enemy and plays the death animation
    private void Die()
    {

        animator.SetTrigger("Die");
        seeker.enabled = false;
        this.enabled = false;
        rb.gravityScale = 0;
        CancelInvoke();
        GetComponent<Collider2D>().enabled = false;
        
        if (sprite)
        {
            sprite.sortingOrder = 2;
        }
        Destroy(gameObject, 3f);
    }

 
}
