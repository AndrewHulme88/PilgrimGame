using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform minPoint;
    public Transform maxPoint;
    public GrowBlock growBlockPrefab;

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

        Instantiate(growBlockPrefab, startPoint, Quaternion.identity);
    }
}
