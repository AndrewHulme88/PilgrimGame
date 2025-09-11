using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform clampMin;
    [SerializeField] private Transform clampMax;

    private Transform target;
    private Camera targetCamera;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
        targetCamera = GetComponent<Camera>();

        clampMin.SetParent(null);
        clampMax.SetParent(null);
        halfHeight = targetCamera.orthographicSize;
        halfWidth = halfHeight * targetCamera.aspect;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPosition;
    }
}
