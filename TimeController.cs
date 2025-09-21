using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    [SerializeField] private float currentTime;
    [SerializeField] private float dayStartTime;
    [SerializeField] private float dayEndTime;
    [SerializeField] private float timeSpeed = 0.25f;
    [SerializeField] private string dayEndScene;
    
    public int currentDay = 1;

    private bool timeActive;

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

    private void Start()
    {
        currentTime = dayStartTime;
        timeActive = true;
    }

    private void Update()
    {
        if (!timeActive) return;

        currentTime += Time.deltaTime * timeSpeed;

        if (currentTime > dayEndTime)
        {
            currentTime = dayEndTime;
            EndDay();
        }

        if(UIController.instance != null)
        {
            UIController.instance.UpdateTimeText(currentTime);
        }
    }

    public void StartDay()
    {
        timeActive = true;
        currentTime = dayStartTime;
    }

    public void EndDay()
    {
        timeActive = false;

        currentDay++;
        GridInfo.instance.GrowCrop();

        PlayerPrefs.SetString("TransitionString", "WakeUp");

        SceneManager.LoadScene(dayEndScene);
    }
}
