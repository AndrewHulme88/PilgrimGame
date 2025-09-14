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
}

[System.Serializable]
public class BlockInfo
{
    public bool isWatered;
    public GrowBlock.GrowthStage currentStage;
}

[System.Serializable]
public class InfoRow
{
    public List<BlockInfo> blocks = new List<BlockInfo>();
}