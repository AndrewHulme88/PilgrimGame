using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference actionInput;
    [SerializeField] private Animator anim;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

        if (rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.linearVelocity.x > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }

        anim.SetFloat("speed", rb.linearVelocity.magnitude);
    }

    private void UseTool()
    {
        GrowBlock growBlock = null;

        growBlock = FindFirstObjectByType<GrowBlock>();

        growBlock.PloughSoil();
    }
}
