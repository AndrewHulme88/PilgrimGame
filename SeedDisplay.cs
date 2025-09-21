using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedDisplay : MonoBehaviour
{
    public CropController.CropType cropType;
    public Image seedImage;
    public TMP_Text seedCountText;

    public void UpdateDisplay()
    {
        CropInfo cropInfo = CropController.instance.GetCropInfo(cropType);
        seedImage.sprite = cropInfo.seedType;
        seedCountText.text = "x" + cropInfo.seedAmount;
    }

    public void SelectSeed()
    {

    }
}
