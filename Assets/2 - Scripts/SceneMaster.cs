using UnityEngine;

public class SceneMaster : MonoBehaviour
{
    public void StartGame()
    {
        print("Game Started");
    }
    public void QuitGame()
    {
        Application.Quit();
        print("Game Quitted");
    }
}
