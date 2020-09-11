using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundToggle : MonoBehaviour
{
    public GameObject soundOff;
    public GameObject soundOn;

    private void Awake() {
        if (PlayerPrefs.GetInt("SoundToggle", 1) == 1)
        {
            PlayerPrefs.SetInt("SoundToggle", 1); // On
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("SoundToggle", 0); // Off
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }

    public void Toggler()
    {
        if (PlayerPrefs.GetInt("SoundToggle", 0) == 0)
        {
            PlayerPrefs.SetInt("SoundToggle", 1); // On
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("SoundToggle", 0); // Off
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }
}
