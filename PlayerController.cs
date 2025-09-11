using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference actionInput;
    [SerializeField] private Animator anim;

    private Rigidbody2D rb;

    public enum ToolType
    {
        hoe,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;

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

        bool hasSwitchedTool = false;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;
            if ((int)currentTool > System.Enum.GetValues(typeof(ToolType)).Length - 1)
            {
                currentTool = ToolType.hoe;
            }
            
            hasSwitchedTool = true;
        }

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.hoe;
            hasSwitchedTool = true;
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.wateringCan;
            hasSwitchedTool = true;
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.seeds;
            hasSwitchedTool = true;
        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = ToolType.basket;
            hasSwitchedTool = true;
        }

        if (hasSwitchedTool)
        {
            UIController.instance.SwitchTool((int)currentTool);
        }

        if (actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }

        anim.SetFloat("speed", rb.linearVelocity.magnitude);
    }

    private void UseTool()
    {
        GrowBlock growBlock = null;

        growBlock = FindFirstObjectByType<GrowBlock>();

        //growBlock.PloughSoil();

        if(growBlock != null)
        {
            switch (currentTool)
            {
                case ToolType.hoe:
                    growBlock.PloughSoil();
                    break;
                case ToolType.wateringCan:
                    // Water the plant
                    break;
                case ToolType.seeds:
                    // Plant seeds
                    break;
                case ToolType.basket:
                    // Harvest the plant
                    break;
                default:
                    break;
            }
        }
    }
}
