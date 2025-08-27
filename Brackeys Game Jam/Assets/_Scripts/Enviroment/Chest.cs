using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] chestLoot;
 
    public void Open()
    {
        for(int i = 0; i < chestLoot.Length; i++)
        {
            Vector2 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-2f, 2f);
            Instantiate(chestLoot[i], spawnPosition, Quaternion.identity);
        }
    }
}
