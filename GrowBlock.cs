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
    public SpriteRenderer cropSpriteRenderer;
    public Sprite soilTilled;
    public Sprite soilWatered;
    public Sprite cropPlanted;
    public Sprite cropGrowing1;
    public Sprite cropGrowing2;
    public Sprite cropRipe;

    public bool isWatered;

    void Start()
    {
        
    }

    void Update()
    {
#if UNITY_EDITOR

        // For testing purposes only
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif

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

    public void PlantSeed()
    {
        if (currentStage == GrowthStage.ploughed && isWatered)
        {
            currentStage = GrowthStage.planted;
            
            UpdateCropSprite();
        }
    }

    private void UpdateCropSprite()
    {
        switch (currentStage)
        {
            case GrowthStage.ploughed:
                cropSpriteRenderer.sprite = null;
                break;
            case GrowthStage.planted:
                cropSpriteRenderer.sprite = cropPlanted;
                break;
            case GrowthStage.growing1:
                cropSpriteRenderer.sprite = cropGrowing1;
                break;
            case GrowthStage.growing2:
                cropSpriteRenderer.sprite = cropGrowing2;
                break;
            case GrowthStage.ripe:
                cropSpriteRenderer.sprite = cropRipe;
                break;
        }
    }

    public void AdvanceCrop()
    {
        if(isWatered)
        {
            if(currentStage == GrowthStage.planted || currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;
                isWatered = false;
                UpdateCropSprite();
                SetSoilSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if (currentStage == GrowthStage.ripe)
        {
            currentStage = GrowthStage.ploughed;
            isWatered = false;
            cropSpriteRenderer.sprite = null;
            SetSoilSprite();
        }
    }
}
