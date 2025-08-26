using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public GameObject[] lootTable;
    [Range(1, 100)]public int lootChance = 10;

    public void DropLoot()
    {
        int lootChanceIndex = Random.Range(0, 100);
        if(lootChanceIndex <= lootChance)
        {
            int randomIndex = Random.Range(0, lootTable.Length);
            Instantiate(lootTable[randomIndex].gameObject, transform.position, Quaternion.identity);
        }
    }
}
