using UnityEngine;
/// <summary>
/// This manages the second block of the Guiding (second) part;
/// the player is navigated to a certain area
/// </summary>
public class NavigatePlayer : MonoBehaviour {

	public CanvasGroup playerPosNavigation;

	void Start () {
		playerPosNavigation.alpha = 0f;
	}
	
	void Update () {
		DisplayPlayerPosNavigation();
	}

	private void DisplayPlayerPosNavigation (){
		// if the player is outside of the target zone,
		// display truck notification
		playerPosNavigation.alpha = Mathf.Min(playerPosNavigation.alpha + Time.deltaTime, 1f);
	} 
}
