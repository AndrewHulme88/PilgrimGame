using UnityEngine;
using System.Collections.Generic;

public class CropController : MonoBehaviour
{
    public static CropController instance;

    public enum CropType
    {
        pumpkin,
        lettuce,
        carrot,
        hay,
        potato,
        strawberry,
        tomato,
        avocado
    }

    public List<CropInfo> cropList = new List<CropInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

     public CropInfo GetCropInfo(CropType cropToGet)
    {
        int position = -1;

        for (int  i = 0;  i < cropList.Count;  i++)
        {
            if(cropList[i].cropType == cropToGet)
            {
                position = i;
                break;
            }
        }

        if(position >= 0)
        {
            return cropList[position];
        }
        else
        {
            return null;
        }
    }

    public void UseSeed(CropType seedToUse)
    {
        foreach (CropInfo cropInfo in cropList)
        {
            if(cropInfo.cropType == seedToUse)
            {
                cropInfo.seedAmount--;
            }
        }
    }

    public void AddCrop(CropType cropToAdd)
    {
        foreach (CropInfo cropInfo in cropList)
        {
            if (cropInfo.cropType == cropToAdd)
            {
                cropInfo.cropAmount++;
            }
        }
    }
}

[System.Serializable]
public class CropInfo
{
    public CropController.CropType cropType;
    public Sprite finalCrop;
    public Sprite seedType;
    public Sprite planted;
    public Sprite growStage1;
    public Sprite growStage2;
    public Sprite ripe;
    public int seedAmount;
    public int cropAmount;
    public float seedPrice;
    public float cropPrice;

    [Range(0f, 100f)]
    public float growFailChance;
}
