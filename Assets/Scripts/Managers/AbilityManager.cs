using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public bool canAffectProjectiles;
    public bool canAffectEnemies;

    public int abilityStength;
    public float abilityDuration;
    public float abilitySpeed;
    public float abilityCooldown;

    private void Start()
    {
        Destroy(gameObject, abilityDuration);

        transform.Translate(Vector3.left * abilitySpeed * Time.deltaTime);
    }
    private void Update()
    {
        if (abilityStength <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAffectProjectiles && collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            abilityStength -= collision.gameObject.GetComponent<EnemyProjectile>().damage;
        }

        if (canAffectEnemies && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAi>().TakeDamage(abilityStength);
        }
    }
}
