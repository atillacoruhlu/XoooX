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
        Initiate.Fade("Offline_Scene",Color.black,2); //Loads Mirror
    }

    public void PracticeButton()
    {
        Initiate.Fade("MiniMax_Scene",Color.black,2); //Loads AI Scene
    }

    public void TutorialButton()
    {
        Initiate.Fade("Tutorial_Scene",Color.black,2); //Loads tutorial
    }

    public void InfoButton()
    {
        Initiate.Fade("Info_Scene",Color.black,2); //Loads credits
    }

    public void ExitGame()
    {
        Application.Quit(); //Stops existing
    }
}
