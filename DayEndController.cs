using TMPro;
using UnityEngine;

public class DayEndController : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;

    void Start()
    {
        if(TimeController.instance != null)
        {
            dayText.text = "Day " + TimeController.instance.currentDay;
        }
    }
}
