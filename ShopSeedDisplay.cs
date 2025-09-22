using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CropController;

public class ShopSeedDisplay : MonoBehaviour
{
    public CropController.CropType seedType;
    public Image seedImage;
    public TMP_Text seedAmountText;
    public TMP_Text seedCostText;

    public void UpdateDisplay()
    {
        CropInfo cropInfo = CropController.instance.GetCropInfo(seedType);
        seedImage.sprite = cropInfo.seedType;
        seedAmountText.text = "x" + cropInfo.seedAmount;
        seedCostText.text = "$" + cropInfo.seedPrice + " each";
    }

    public void BuySeed(int amount)
    {
        CropInfo cropInfo = CropController.instance.GetCropInfo(seedType);

        if(CurrencyController.instance.CheckMoney(cropInfo.seedPrice * amount))
        {
            CropController.instance.AddSeed(seedType, amount);
            CurrencyController.instance.SpendMoney(cropInfo.seedPrice * amount);
            UpdateDisplay();
        }
    }
}
