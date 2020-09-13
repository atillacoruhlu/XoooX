using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour {

    public void ReturnButton () {
        Initiate.Fade ("Menu_Scene", Color.black, 2); //Loads menu
    }
}