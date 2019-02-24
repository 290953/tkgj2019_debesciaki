using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
