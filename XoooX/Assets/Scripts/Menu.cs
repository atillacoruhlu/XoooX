using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Camera menuCamera;
    private void Awake() {
        Instantiate(menuCamera, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Offline_Scene"); //Loads Mirror
    }

    public void PracticeButton()
    {
        SceneManager.LoadScene("MiniMax_Scene"); //Loads AI Scene
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial_Scene"); //Loads tutorial
    }

    public void InfoButton()
    {
        SceneManager.LoadScene("Info_Scene"); //Loads credits
    }

    public void ExitGame()
    {
        Application.Quit(); //Stops existing
    }
}
