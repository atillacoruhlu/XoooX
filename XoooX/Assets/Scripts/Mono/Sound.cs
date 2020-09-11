using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //To see it in the editor
public class Sound {

	public string name;

	public AudioClip clip;

	public bool loop;

	[Range(0f,1f)]
	public float volume;

	[Range(.1f,3f)]
	public float pitch;

	//[HideInInspector]
	public AudioSource source;
}
