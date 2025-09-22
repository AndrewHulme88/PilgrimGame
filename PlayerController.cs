using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

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
    [SerializeField] private float toolRange = 3f;

    public CropController.CropType currentSeedCropType;

    private Rigidbody2D rb;
    private float toolWaitCounter;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        UIController.instance.SwitchTool((int)currentTool);
        UIController.instance.SwitchSeed(currentSeedCropType);
    }

    void Update()
    {
        if(UIController.instance != null && UIController.instance.inventoryController != null)
        {
            if(UIController.instance.inventoryController.gameObject.activeSelf)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

        if(UIController.instance != null && UIController.instance.shopController != null)
        {
            if (UIController.instance.shopController.gameObject.activeSelf)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

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

        if (GridController.instance != null)
        {
            toolIndicator.gameObject.SetActive(true);

            if (actionInput.action.WasPressedThisFrame())
            {
                UseTool();
            }

            toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

            if (Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
            {
                toolIndicator.position = transform.position + (toolIndicator.position - transform.position).normalized * toolRange;
            }

            toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + 0.5f, Mathf.FloorToInt(toolIndicator.position.y) + 0.5f, 0f);
        }
        else
        {
            toolIndicator.gameObject.SetActive(false);
        }

        anim.SetFloat("speed", rb.linearVelocity.magnitude);
    }

    private void UseTool()
    {
        GrowBlock growBlock = null;

        growBlock = GridController.instance.GetBlock(toolIndicator.position.x - 0.5f, toolIndicator.position.y - 0.5f);

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

                    if(CropController.instance.GetCropInfo(currentSeedCropType).seedAmount > 0)
                    {
                        growBlock.PlantSeed(currentSeedCropType);
                        //CropController.instance.UseSeed(currentSeedCropType);         
                    }

                    break;

                case ToolType.basket:
                    growBlock.HarvestCrop();
                    break;

                default:
                    break;
            }
        }
    }

    public void SwitchSeed(CropController.CropType newSeed)
    {
        currentSeedCropType = newSeed;
    }
}
