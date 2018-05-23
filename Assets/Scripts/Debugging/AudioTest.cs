using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this is for testing of audio funciton
/// </summary>
public class AudioTest : MonoBehaviour {

	public Text testText;
	public bool audioIsTriggered = false;
	private AudioSource audio;

	private void Start(){
		audio = GetComponent<AudioSource>();
	}

	private void Update () {
		if(!audioIsTriggered) { return; }

		Debug.Log("audio is triggered " + audio.isPlaying);
		if(!audio.isPlaying) {
			audio.Play();
		}
		if(!audio.isPlaying) {
			testText.text = "audio has end";
		}
	}
}
