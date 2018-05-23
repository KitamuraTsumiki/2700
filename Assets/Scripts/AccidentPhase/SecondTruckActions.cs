using UnityEngine;
/// <summary>
/// This class defines all actions of the second truck,
/// which hits the player
/// </summary>
public class SecondTruckActions : MonoBehaviour {

	public bool isActive = false;
	public bool isGoingToHit = false;
	public bool isContacting = false;
	public AudioSource truckSoundBeforeHit;
	public AudioSource truckSoundAfterHit;
	public GuidingContentManager contentManager;

	private bool soundBeforeHitPlayed = false;
	private bool soundAfterHitPlayed = false;
	private Animator truckAnimation;
	private string truckAnimHit = "Truck_Lane2_Hit";
	private string truckAnimStop = "Truck_Lane2_Stop";

	private void Start () {
		truckAnimation = GetComponent<Animator>();
		gameObject.SetActive(false);
	}
	
	private void Update () {
		if(!isActive) { return; }

		var activateTruckActions = contentManager.secondTruckComing.enabled;
		if(activateTruckActions) {
			ActivateActionsBeforeHit();
		}
	}

	private void ActivateActionsBeforeHit(){
		// animate the truck
		if(!isGoingToHit) {
			truckAnimation.Play(truckAnimStop);
			return;
		}

		truckAnimation.Play(truckAnimHit);
		// activate car horn, brake sound
		var soundMustBePlayed = truckSoundBeforeHit.clip != null && !truckSoundBeforeHit.isPlaying && !soundBeforeHitPlayed;
		if(soundMustBePlayed) {
			truckSoundBeforeHit.Play();
			soundBeforeHitPlayed = true;
		}

	}

	public void ActivateActionsAfterHit(){
		// activate sound
		var soundMustBePlayed = truckSoundAfterHit.clip != null && !truckSoundAfterHit.isPlaying && !soundAfterHitPlayed;
		if(soundMustBePlayed) {
			truckSoundAfterHit.Play();
			soundAfterHitPlayed = true;
		}
	}

	private void OnCollisionEnter(Collision collision){
		Debug.Log("collision with: " + collision.gameObject);
		// when the collider contacts player's head, "hitting" is activated
		if(collision.gameObject.tag != "MainCamera") { return; }
		isContacting = true;
		Rigidbody rbd = GetComponent<Rigidbody>();
		rbd.isKinematic = true;
		rbd.useGravity = false;
		
	}
}
