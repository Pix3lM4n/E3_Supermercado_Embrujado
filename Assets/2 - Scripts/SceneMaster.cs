using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Martin");
        print("Game Started");
    }
    public void ChangeScene(int sceneToLoad)
    {
        if (sceneToLoad == 1)
        {
            SceneManager.LoadScene("EndScreen");
            print("Win");
        }
        else if (sceneToLoad == 2)
        {
            SceneManager.LoadScene("EndScreen");
            print("Lose");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        print("Game Quitted");
    }
}
