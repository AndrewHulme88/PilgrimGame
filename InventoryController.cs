using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public SeedDisplay[] seedDisplays;
    public CropDisplay[] cropDisplays;

    public void OpenClose()
    {
        if(UIController.instance.shopController.gameObject.activeSelf == false)
        {
            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
                UpdateSeedDisplays();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void UpdateSeedDisplays()
    {
        foreach (SeedDisplay seedDisplay in seedDisplays)
        {
            seedDisplay.UpdateDisplay();
        }

        foreach (CropDisplay cropDisplay in cropDisplays)
        {
            cropDisplay.UpdateDisplay();
        }
    }
}
