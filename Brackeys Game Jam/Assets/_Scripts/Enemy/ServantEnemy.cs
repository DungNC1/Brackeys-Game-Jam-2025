using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ServantEnemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 3f;
    public float moveSpeed = 2f;
    public float attackRange = 6f;
    public float attackCooldown = 2f;

    [Header("Refs")]
    public Transform firePoint;               // place slightly in front of the servant
    public GameObject projectilePrefab;
    public GameObject spilledFoodPrefab;

    Transform player;
    Rigidbody2D rb;
    float lastAttackTime;
    float moveDir; // -1 left, 0 idle, 1 right

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        FacePlayer();

        if (dist > attackRange)
        {
            moveDir = Mathf.Sign(player.position.x - transform.position.x);
        }
        else
        {
            moveDir = 0f;
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
    }

    void FacePlayer()
    {
        if (player == null) return;
        Vector3 sc = transform.localScale;
        sc.x = (player.position.x < transform.position.x) ? -Mathf.Abs(sc.x) : Mathf.Abs(sc.x);
        transform.localScale = sc;
    }

    void Attack()
    {
        if (projectilePrefab == null || firePoint == null || player == null) return;

        Vector2 dir = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null) p.Initialize(dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f) Die();
    }

    void Die()
    {
        if (spilledFoodPrefab != null)
            Instantiate(spilledFoodPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        if (firePoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, 0.1f);
        }
    }
}
