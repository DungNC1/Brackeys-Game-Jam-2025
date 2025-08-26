using UnityEngine;

[RequireComponent(typeof(Collectible))]
public class Cookie : MonoBehaviour
{
    public int cookieAmount = 1;

    public void CollectCookie()
    {
        CookieCounter.instance.AddCookie(1);
    }
}
