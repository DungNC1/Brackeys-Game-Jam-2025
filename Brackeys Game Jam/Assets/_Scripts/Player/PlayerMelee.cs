using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public KeyCode attackKey = KeyCode.Mouse0;
    public float attackCooldown = 0.3f;
    public LayerMask enemyLayer;

    [Header("References")]
    public Transform attackPoint;
    public Vector2 attackBoxSize = new Vector2(1f, 1f);

    float lastAttackTime;

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackBoxSize, 0f, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<EnemyHealth>()?.TakeDamage(1);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackBoxSize);
    }
}
