﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public Camera menuCamera;

    private void Awake () {
        Instantiate (menuCamera, new Vector3 (0, 0, 0), Quaternion.identity);
    }
    public void PlayButton () {
        Initiate.Fade ("Offline_Scene", Color.black, 2); //Loads Mirror
    }

    public void TutorialButton () {
        Initiate.Fade ("Tutorial_Scene", Color.black, 2); //Loads tutorial
    }

    public void InfoButton () {
        Initiate.Fade ("Info_Scene", Color.black, 2); //Loads credits
    }

    public void EasterEggButton () {
        if (PlayerPrefs.GetInt ("Egg", 1) == 0) {
            PlayerPrefs.SetInt ("Egg", 1); // Off
        } else {
            PlayerPrefs.SetInt ("Egg", 0); // On
        }
    }

    public void ReturnButton () {
        Initiate.Fade ("Menu_Scene", Color.black, 2); //Loads menu
    }

    public void ExitGame () {
        Application.Quit (); //Stops existing
    }
}