using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

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
    [SerializeField] private float toolWaitTime = 0.5f;
    [SerializeField] private Transform toolIndicator;
    [SerializeField] private float toolRange = 3f;

    [Header("Directional Rigs")]
    [SerializeField] private Transform rigUp;
    [SerializeField] private Transform rigDown;
    [SerializeField] private Transform rigRight;

    [Header("Animators")]
    [SerializeField] private Animator animUp;
    [SerializeField] private Animator animDown;
    [SerializeField] private Animator animRight;

    public CropController.CropType currentSeedCropType;

    private Rigidbody2D rb;
    private float toolWaitCounter;

    // Active rig and animator
    private SpriteRenderer[] srUp, srDown, srRight;
    private Animator activeAnim;
    private enum FacingDirection { up, down, right }
    private FacingDirection currentFacingDirection = FacingDirection.down;
    private Vector2 lastDir = Vector2.down;
    private Vector3 rightRigBaseScale;

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

        // Cache renderers of each rig
        srRight = rigRight.GetComponentsInChildren<SpriteRenderer>(true);
        srUp = rigUp.GetComponentsInChildren<SpriteRenderer>(true);
        srDown = rigDown.GetComponentsInChildren<SpriteRenderer>(true);

        // Keep animating even when hidden so swaps are seamless
        if (animRight) animRight.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        if (animUp) animUp.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        if (animDown) animDown.cullingMode = AnimatorCullingMode.AlwaysAnimate;

        // Start facing down
        SetRigVisible(srDown, true);
        SetRigVisible(srRight, false);
        SetRigVisible(srUp, false);
        activeAnim = animDown;
        currentFacingDirection = FacingDirection.down;
        rightRigBaseScale = rigRight.localScale;
        rightRigBaseScale.x = Mathf.Abs(rightRigBaseScale.x);

        UIController.instance.SwitchTool((int)currentTool);
        UIController.instance.SwitchSeed(currentSeedCropType);
    }

    void Update()
    {
        if(UIController.instance != null)
        {
            if(UIController.instance.inventoryController != null)
            {
                if(UIController.instance.inventoryController.gameObject.activeSelf)
                {
                    rb.linearVelocity = Vector2.zero;
                    return;
                }
            }

            if(UIController.instance.shopController != null)
            {
                if (UIController.instance.shopController.gameObject.activeSelf)
                {
                    rb.linearVelocity = Vector2.zero;
                    return;
                }
            }

            if(UIController.instance.pauseScreen != null)
            {
                if (UIController.instance.pauseScreen.activeSelf)
                {
                    rb.linearVelocity = Vector2.zero;
                    return;
                }
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

            Vector2 dir = rb.linearVelocity.sqrMagnitude > 0f ? rb.linearVelocity : lastDir;

            //if (rb.linearVelocity.x < 0f)
            //{
            //    transform.localScale = new Vector3(-1f, 1f, 1f);
            //}
            //else if (rb.linearVelocity.x > 0f)
            //{
            //    transform.localScale = new Vector3(1f, 1f, 1f);
            //}

            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            {
                // Horizontal dominates: show Right rig and flip for Left
                SwitchTo(FacingDirection.right);
                SetRightRigFacing(dir.x < 0f);
            }
            else if (dir.y > 0f)
            {
                SwitchTo(FacingDirection.up);
                SetRightRigFacing(false);
            }
            else
            {
                SwitchTo(FacingDirection.down);
                SetRightRigFacing(false);
            }

            if (rb.linearVelocity.sqrMagnitude > 0f) lastDir = dir;
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

        float spd = rb.linearVelocity.magnitude;
        if (animRight) animRight.SetFloat("speed", spd);
        if (animUp) animUp.SetFloat("speed", spd);
        if (animDown) animDown.SetFloat("speed", spd);
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
                    //anim.SetTrigger("useHoe");
                    break;

                case ToolType.wateringCan:
                    growBlock.WaterSoil();
                    //anim.SetTrigger("useWateringCan");
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

    private static void SetRigVisible(SpriteRenderer[] list, bool show)
    {
        if (list == null) return;
        for (int i = 0; i < list.Length; i++)
            if (list[i] != null) list[i].enabled = show;
    }

    private void SwitchTo(FacingDirection target)
    {
        if (currentFacingDirection == target) return;

        // Copy normalized time from old to new so the walk cycle feels continuous
        Animator src = activeAnim;
        Animator dst = target switch
        {
            FacingDirection.right => animRight,
            FacingDirection.up => animUp,
            _ => animDown
        };

        if (src != null && dst != null)
        {
            var info = src.GetCurrentAnimatorStateInfo(0);
            dst.Play(info.fullPathHash, 0, info.normalizedTime % 1f);
        }

        // Toggle visibility
        SetRigVisible(srRight, target == FacingDirection.right);
        SetRigVisible(srUp, target == FacingDirection.up);
        SetRigVisible(srDown, target == FacingDirection.down);

        activeAnim = dst;
        currentFacingDirection = target;
    }

    private void SetRightRigFacing(bool left)
    {
        // Mirror ONLY the Right rig container/root, not the player root
        rigRight.localScale = new Vector3(
            (left ? -1f : 1f) * Mathf.Abs(rightRigBaseScale.x),
            rightRigBaseScale.y,
            rightRigBaseScale.z
        );
    }
}
