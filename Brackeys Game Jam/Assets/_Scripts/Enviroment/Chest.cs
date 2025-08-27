using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] chestLoot;
 
    public void Open()
    {
        for(int i = 0; i < chestLoot.Length; i++)
        {
            Vector2 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-2, 2);
            Instantiate(chestLoot[i], spawnPosition, Quaternion.identity);
        }
    }
}
