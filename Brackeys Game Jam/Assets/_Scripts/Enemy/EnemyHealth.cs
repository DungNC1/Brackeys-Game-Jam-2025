using UnityEngine;

[RequireComponent(typeof(LootSystem))]
public class EnemyHealth : MonoBehaviour
{
    public int health;
    private int currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            GetComponent<LootSystem>().DropLoot();
            Destroy(gameObject);
        }
    }
}
