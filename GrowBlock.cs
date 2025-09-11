using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    public enum GrowthStage
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowthStage currentStage;
    public SpriteRenderer spriteRenderer;
    public Sprite soilTilled;
    public Sprite soilWatered;

    public bool isWatered;

    void Start()
    {
        
    }

    void Update()
    {
        // For testing purposes only
        //if(Keyboard.current.eKey.wasPressedThisFrame)
        //{
        //    AdvanceStage();
        //    SetSoilSprite();
        //}
    }

    public void AdvanceStage()
    {
        currentStage++;

        if (currentStage > GrowthStage.ripe)
        {
            currentStage = GrowthStage.barren;
        }
    }

    public void SetSoilSprite()
    {
        if(currentStage == GrowthStage.barren)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            if(isWatered)
            {
                spriteRenderer.sprite = soilWatered;
            }
            else
            {
                spriteRenderer.sprite = soilTilled;
            }
        }
    }

    public void PloughSoil()
    {
        if (currentStage == GrowthStage.barren)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
        }
    }

    public void WaterSoil()
    {
        isWatered = true;

        SetSoilSprite();
    }
}
