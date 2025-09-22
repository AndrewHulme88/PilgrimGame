using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public SeedDisplay[] seedDisplays;

    public void OpenClose()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            UpdateSeedDisplays();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateSeedDisplays()
    {
        foreach (SeedDisplay seedDisplay in seedDisplays)
        {
            seedDisplay.UpdateDisplay();
        }
    }
}
