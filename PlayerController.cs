using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum ToolType
    {
        hoe,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference actionInput;
    [SerializeField] private Animator anim;
    [SerializeField] private float toolWaitTime = 0.5f;
    [SerializeField] private Transform toolIndicator;

    private Rigidbody2D rb;
    private float toolWaitCounter;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        UIController.instance.SwitchTool((int)currentTool);
    }

    void Update()
    {
        if (toolWaitCounter > 0f)
        {
            rb.linearVelocity = Vector2.zero;
            toolWaitCounter -= Time.deltaTime;
        }
        else
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

        toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);
    }

    private void UseTool()
    {
        GrowBlock growBlock = null;

        growBlock = FindFirstObjectByType<GrowBlock>();

        toolWaitCounter = toolWaitTime;

        if (growBlock != null)
        {
            switch (currentTool)
            {
                case ToolType.hoe:
                    growBlock.PloughSoil();
                    anim.SetTrigger("useHoe");
                    break;
                case ToolType.wateringCan:
                    growBlock.WaterSoil();
                    anim.SetTrigger("useWateringCan");
                    break;
                case ToolType.seeds:
                    growBlock.PlantSeed();
                    break;
                case ToolType.basket:
                    growBlock.HarvestCrop();
                    break;
                default:
                    break;
            }
        }
    }
}
