using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Transform startPoint;
    [SerializeField] private string transitionString;

    private void Start()
    {
        if(PlayerPrefs.HasKey("TransitionString"))
        {
            if (PlayerPrefs.GetString("TransitionString") == transitionString)
            {
                PlayerController.instance.transform.position = startPoint.position;
                //PlayerPrefs.DeleteKey("TransitionString");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);

            PlayerPrefs.SetString("TransitionString", transitionString);
        }
    }
}
