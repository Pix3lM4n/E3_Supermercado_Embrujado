using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public int sceneToLoad;
    public TextMeshProUGUI endScreenBox;
    public Canvas canvas;
    public AudioSource victorySound;
    public AudioSource loseSound;
    [TextArea] public string victoryText, defeatText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneToLoad = GameMaster.Instance.isListCorrect;
        if (sceneToLoad == 1)
        {
            endScreenBox.text = victoryText;
            victorySound.Play();
        }
        else if (sceneToLoad == 2)
        {
            endScreenBox.text = defeatText;
            loseSound.Play();
        }
    }
    public void StartGame(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
        print("Game Started");
    }
    public void ChangeScene()
    {
        if (sceneToLoad == 1) //Win
        {
            SceneManager.LoadScene("EndScreen");
            print("Win");
        }
        else if (sceneToLoad == 2) //Lose
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
