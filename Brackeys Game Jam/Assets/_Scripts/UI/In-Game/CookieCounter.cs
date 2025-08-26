using TMPro;
using UnityEngine;

public class CookieCounter : MonoBehaviour
{
    public int cookieAmount;
    public TextMeshProUGUI cookieCounter;

    public static CookieCounter instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateText();
    }

    public void AddCookie(int amount)
    {
        cookieAmount += amount;
        UpdateText();
    }

    private void UpdateText()
    {
        cookieCounter.text = "Cookie: " + cookieAmount.ToString();
    }
}
