using UnityEngine;
using System.Collections.Generic;

public class GridInfo : MonoBehaviour
{
    public static GridInfo instance;

    public List<InfoRow> grid;
    public bool hasGrid;

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

    public void CreateGrid()
    {
        hasGrid = true;

        for(int y = 0; y < GridController.instance.blockRows.Count; y++)
        {
            grid.Add(new InfoRow());
            for (int x = 0; x < GridController.instance.blockRows[y].blocks.Count; x++)
            {
                grid[y].blocks.Add(new BlockInfo());
            }
        }
    }

    public void UpdateInfo(GrowBlock growBlock, int xPos, int yPos)
    {
        grid[yPos].blocks[xPos].isWatered = growBlock.isWatered;
        grid[yPos].blocks[xPos].currentStage = growBlock.currentStage;
        grid[yPos].blocks[xPos].cropType = growBlock.cropType;
    }

    public void GrowCrop()
    {
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].blocks.Count; x++)
            {
                if (grid[y].blocks[x].currentStage == GrowBlock.GrowthStage.planted || grid[y].blocks[x].currentStage == GrowBlock.GrowthStage.growing1 || grid[y].blocks[x].currentStage == GrowBlock.GrowthStage.growing2)
                {
                    if (grid[y].blocks[x].isWatered)
                    {
                        grid[y].blocks[x].currentStage++;
                        grid[y].blocks[x].isWatered = false;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class BlockInfo
{
    //TODO: Implement proper saving/loading of grid info

    public bool isWatered;
    public GrowBlock.GrowthStage currentStage;

    public CropController.CropType cropType;
}

[System.Serializable]
public class InfoRow
{
    public List<BlockInfo> blocks = new List<BlockInfo>();
}