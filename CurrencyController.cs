using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController instance;

    public float currentMoney;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpendMoney(float amountToSpend)
    {
        currentMoney -= amountToSpend;
    }

    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd;
    }

    public bool CheckMoney(float amount)
    {
        if(currentMoney >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
