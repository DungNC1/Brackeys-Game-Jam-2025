using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public UnityEvent OnCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(OnCollect != null)
        {
            OnCollect.Invoke();
        }

        Destroy(gameObject);
    }
}
