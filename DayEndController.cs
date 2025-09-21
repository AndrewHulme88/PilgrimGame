using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DayEndController : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private string wakeUpScene;

    void Start()
    {
        if(TimeController.instance != null)
        {
            dayText.text = "Day " + TimeController.instance.currentDay;
        }
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            TimeController.instance.StartDay();

            SceneManager.LoadScene(wakeUpScene);
        }
    }
}
