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
		ActivateActionsBeforeHit();
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
		if(!soundMustBePlayed) { return; }
		float fadingPeriod = 0.2f;
		truckAnimation.CrossFade(truckAnimStop, fadingPeriod);
		Debug.Log("truck 02 animation has to played");
		truckSoundAfterHit.Play();
		soundAfterHitPlayed = true;
		
	}

	private void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != "MainCamera") { return; }
		isContacting = true;

		// get contact point with the player
		Vector3 ColliderCenterInWorld = transform.TransformPoint(GetComponent<BoxCollider>().center);
		Vector3 DirTruckToPlayer = other.transform.position - ColliderCenterInWorld;
		RaycastHit hit;

		if(!Physics.Raycast(ColliderCenterInWorld, DirTruckToPlayer, out hit)) { return; }
		if(hit.transform.gameObject.tag != "MainCamera") { return; }
		Vector3 contactPointWithPlayer = hit.point;
		
		// activate CameraPosModification on the Camerarig
		ActivateCamPosModification(contactPointWithPlayer);
	}

	private void ActivateCamPosModification(Vector3 _contactPoint){
		// activate CameraPosModification on the Camerarig
		var camPosMod = (CameraPosModification)FindObjectOfType(typeof(CameraPosModification));
		if(camPosMod == null || camPosMod.isActivated) {return;}
		if(!camPosMod.isActivated) {
			camPosMod.SetPointsAndNormal(_contactPoint);
			camPosMod.isActivated = true;
		}
	}
}
