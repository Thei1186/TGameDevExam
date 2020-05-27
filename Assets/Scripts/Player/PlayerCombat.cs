using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 15;
    public float attackRate = 0.7f;

    // Used to determing who can be hurt by the player's attack
    public LayerMask enemyLayers;

    // The player's available abilities
    public GameObject[] abilities;
    private float nextAttackTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime && !EventSystem.current.IsPointerOverGameObject())
        {
            attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    // Uses an ability from the player's array of abilities based on index, this is called from the action bar
    public void useAbility(int abilityIndex)
    {
        GameObject chosenAbility = abilities[abilityIndex];
        Vector3 direction = attackPoint.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Instantiate(chosenAbility, attackPoint.position, Quaternion.Euler(0f, 0f, rotation));
    }

    // Attacks all enemies in range
    private void attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyAi target = enemy.GetComponent<EnemyAi>();
            Boss bossTarget = enemy.GetComponent<Boss>();
            if (target)
            {
                target.TakeDamage(attackDamage);
            } else if (bossTarget)
            {
                bossTarget.TakeDamage(attackDamage);
            }
        }
    }

    // Used in the editor to show the range of attack
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
