using UnityEngine;
/// <summary>
/// This class defines actions of a crane in front of the player, in the "instruction" scene.
/// </summary>
public class IntroductionCraneActions : MonoBehaviour {

	public bool isActive = false;

	[SerializeField]
	private IntroductionTruckActions truckAction;
	private bool craneIsAttached = true;

	private string truckAnimStateUp = "Crane_Up_Temp";
	private Animator craneAnimation;
	private AudioSource sound;

	private void Start () {
		craneAnimation = GetComponent<Animator>();
		sound = GetComponent<AudioSource>();
	}
	
	private void Update () {
		ActivateCraneActions();
		ActivateTruckAnimation();
		Debug.Log("craneIsAttached: " + craneIsAttached);
	}

	private void ActivateCraneActions(){
		if(!isActive) { return; }
		craneAnimation.Play(truckAnimStateUp);
	}

	private void ActivateTruckAnimation(){
		if(truckAction.isActive || craneIsAttached) { return; }
		truckAction.isActive = true;
	}

	private void OnTriggerExit(Collider other){
		if(other.gameObject.tag != "container") { return; }
		craneIsAttached = false;
	}
}
