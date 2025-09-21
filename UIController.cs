using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolIconsActive;
    public TMP_Text timeText;
    public InventoryController inventoryController;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryController.OpenClose();
        }
    }

    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolIconsActive)
        {
            icon.SetActive(false);
        }

        toolIconsActive[selected].SetActive(true);
    }

    public void UpdateTimeText(float currentTime)
    {
        if(currentTime < 12f)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + " AM";
        }
        else if (currentTime < 13f)
        {
            timeText.text = "12 PM";
        }
        else if(currentTime < 24f)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12f) + " PM";
        }
        else if(currentTime < 25f)
        {
            timeText.text = "12 AM";
        }
        else
        {
             timeText.text = Mathf.FloorToInt(currentTime - 24f) + " AM";
        }
    }
}
