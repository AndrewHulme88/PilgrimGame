using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    public Transform minPoint;
    public Transform maxPoint;
    public GrowBlock growBlockPrefab;
    public List<BlockRow> blockRows = new List<BlockRow>();
    public LayerMask gridBlockerLayer;

    private Vector2Int gridSize;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);

        Vector3 startPoint = minPoint.position + new Vector3(0.5f, 0.5f, 0f);

        //Instantiate(growBlockPrefab, startPoint, Quaternion.identity);

        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x), Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

        for (int y = 0; y < gridSize.y; y++)
        {
            blockRows.Add(new BlockRow());

            for (int x = 0; x < gridSize.x; x++)
            {
                GrowBlock newBlock = Instantiate(growBlockPrefab, startPoint + new Vector3(x, y, 0f), Quaternion.identity);

                newBlock.transform.SetParent(transform);
                newBlock.spriteRenderer.sprite = null;

                newBlock.SetGridPosition(x, y);

                blockRows[y].blocks.Add(newBlock);

                if (Physics2D.OverlapBox(newBlock.transform.position, new Vector2(0.9f, 0.9f), 0f, gridBlockerLayer))
                {
                    newBlock.spriteRenderer.sprite = null;
                    newBlock.preventUse = true;
                }

                if(GridInfo.instance.hasGrid)
                {
                    BlockInfo savedBlock = GridInfo.instance.grid[y].blocks[x];
                    newBlock.currentStage = savedBlock.currentStage;
                    newBlock.isWatered = savedBlock.isWatered;
                    newBlock.SetSoilSprite();
                    newBlock.UpdateCropSprite();
                }
            }
        }

        if(!GridInfo.instance.hasGrid)
        {
            GridInfo.instance.CreateGrid();
        }
    }

    public GrowBlock GetBlock(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        x -= minPoint.position.x;
        y -= minPoint.position.y;

        int intX = Mathf.RoundToInt(x);
        int intY = Mathf.RoundToInt(y);

        if(intX < gridSize.x && intY < gridSize.y)
        {
            return blockRows[intY].blocks[intX];
        }

        return null;
    }
}

[System.Serializable]
public class BlockRow
{
    public List<GrowBlock> blocks = new List<GrowBlock>();
}