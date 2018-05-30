using UnityEngine;
/// <summary>
/// This class defines actions of a truck in front of the player, in the "instruction" scene.
/// </summary>
public class IntroductionTruckActions : MonoBehaviour {

	public bool isActive = false;

	private string truckAnimState = "Truck_Introduction";
	private Animator truckAnimation;
	private AudioSource sound;

	private void Start () {
		truckAnimation = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
	}
	
	private void Update () {
		ActivateTruckActions();
	}

	private void ActivateTruckActions(){
		if(!isActive) { return; }
		truckAnimation.Play(truckAnimState);
	}
}
