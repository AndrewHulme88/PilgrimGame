using JetBrains.Annotations;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public ShopSeedDisplay[] shopSeedDisplays;
    public ShopCropDisplay[] cropTypes;

    public void OpenClose()
    {
        if(UIController.instance.inventoryController.gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);       
            
            if(gameObject.activeSelf == true)
            {
                foreach (ShopSeedDisplay shopSeedDisplay in shopSeedDisplays)
                {
                    shopSeedDisplay.UpdateDisplay();
                }

                foreach (ShopCropDisplay cropType in cropTypes)
                {
                    cropType.UpdateDisplay();
                }
            }
        }
    }
}
