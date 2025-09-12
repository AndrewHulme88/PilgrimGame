using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform minPoint;
    public Transform maxPoint;
    public GrowBlock growBlockPrefab;

    private Vector2Int gridSize;

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);

        Vector3 startPoint = minPoint.position + new Vector3(0.5f, 0.5f, 0f);

        //Instantiate(growBlockPrefab, startPoint, Quaternion.identity);

        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x), Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                GrowBlock newBlock = Instantiate(growBlockPrefab, startPoint + new Vector3(x, y, 0f), Quaternion.identity);

                newBlock.transform.SetParent(transform);
            }
        }
    }
}
