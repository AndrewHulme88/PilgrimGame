using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolIconsActive;

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

    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolIconsActive)
        {
            icon.SetActive(false);
        }

        toolIconsActive[selected].SetActive(true);
    }
}
