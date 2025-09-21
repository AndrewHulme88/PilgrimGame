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
}
