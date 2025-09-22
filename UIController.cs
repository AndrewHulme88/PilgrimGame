using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolIconsActive;
    public TMP_Text timeText;
    public InventoryController inventoryController;
    public Image seedImage;
    public ShopController shopController;
    public TMP_Text moneyText;
    public GameObject pauseScreen;
    public string mainMenuScene;

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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.B))
        {
            shopController.OpenClose();
        }

#endif
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
        if (currentTime < 12f)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + " AM";
        }
        else if (currentTime < 13f)
        {
            timeText.text = "12 PM";
        }
        else if (currentTime < 24f)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12f) + " PM";
        }
        else if (currentTime < 25f)
        {
            timeText.text = "12 AM";
        }
        else
        {
            timeText.text = Mathf.FloorToInt(currentTime - 24f) + " AM";
        }
    }

    public void SwitchSeed(CropController.CropType newSeed)
    {
        seedImage.sprite = CropController.instance.GetCropInfo(newSeed).seedType;
    }

    public void UpdateMoneyText(float currentMoney)
    {
        moneyText.text = "$" + currentMoney;
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    { 
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);

        Destroy(gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GridInfo.instance.gameObject);
        Destroy(TimeController.instance.gameObject);
        Destroy(CropController.instance.gameObject);
        Destroy(CurrencyController.instance.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
