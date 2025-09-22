using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropDisplay : MonoBehaviour
{
    public Image cropImage;
    public TMP_Text cropCountText;
    public CropController.CropType cropType;

    public void UpdateDisplay()
    {
        CropInfo cropInfo = CropController.instance.GetCropInfo(cropType);

        cropImage.sprite = cropInfo.finalCrop;
        cropCountText.text = "x" + cropInfo.cropAmount;
    }
}
