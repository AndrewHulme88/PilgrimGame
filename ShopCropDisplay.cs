using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCropDisplay : MonoBehaviour
{
    public Image cropImage;
    public TMP_Text cropCountText;
    public TMP_Text priceText;
    public CropController.CropType cropType;

    public void UpdateDisplay()
    {
        CropInfo cropInfo = CropController.instance.GetCropInfo(cropType);

        cropImage.sprite = cropInfo.finalCrop;
        cropCountText.text = "x" + cropInfo.cropAmount;
        priceText.text = "$" + cropInfo.cropPrice + " each";
    }

    public void SellCrop()
    {
        CropInfo cropInfo = CropController.instance.GetCropInfo(cropType);

        if(cropInfo.cropAmount > 0)
        {
            CurrencyController.instance.AddMoney(cropInfo.cropAmount * cropInfo.cropPrice);
            CropController.instance.RemoveCrop(cropType);
            UpdateDisplay();
        }
    }
}
