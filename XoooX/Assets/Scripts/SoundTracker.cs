using UnityEngine;

public class SoundTracker : MonoBehaviour
{
    private void Update()
    {
        if (PlayerPrefs.GetInt("SoundToggle", 1) == 1)
        {
            PlayerPrefs.SetInt("SoundToggle", 1); // On
            AudioListener.volume = 1.0f;
        }
        else
        {
            PlayerPrefs.SetInt("SoundToggle", 0); // Off
            AudioListener.volume = 0.0f;
        }
    }
}
