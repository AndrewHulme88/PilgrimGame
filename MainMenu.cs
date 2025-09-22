using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
