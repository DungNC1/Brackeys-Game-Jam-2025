using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelSystem : MonoBehaviour
{
    private int cookieAmountUntilNextLevel = 10;
    public TextMeshProUGUI cookieAmountUntilNextLevelText;
    public UnityEvent OnLevelUp;

    private void Start()
    {
        cookieAmountUntilNextLevelText.text = cookieAmountUntilNextLevel.ToString();
    }

    private void Update()
    {
        if(CookieCounter.instance.cookieAmount >= cookieAmountUntilNextLevel)
        {
            OnLevelUp.Invoke();
        }

        cookieAmountUntilNextLevelText.text = "Next level: " + CookieCounter.instance.cookieAmount + "/" + cookieAmountUntilNextLevel.ToString();
    }
}
