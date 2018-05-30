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

	// animation parameters
	[SerializeField]
	private Spline path;
	[SerializeField,Range(0f,1f)]
	private float currDist;
	[SerializeField, Range(0f, 1f)]
	private float currRot;
	private float distance;
	[SerializeField]
	private Transform tr;
	[SerializeField]
	private Transform startingPoint;

	private float truckSpeed = 0.2f;

	private bool soundBeforeHitPlayed = false;
	private bool soundAfterHitPlayed = false;
	private Animator truckAnimation;
	private string truckAnimHit = "Truck_Lane2_Hit";
	private string truckAnimStop = "Truck_Lane2_Stop";

	[SerializeField]
	private CameraPosModification camPosMod;

	public float currPosOnPath {
		get{ return currDist; }
	}

	private void Start () {
		truckAnimation = GetComponent<Animator>();
		gameObject.SetActive(false);
		transform.position = startingPoint.position;
		transform.rotation = startingPoint.rotation;
	}
	
	private void Update () {
		if(!isActive) { return; }
		ActivateActionsBeforeHit();
		AnimateTruck();
	}

	private void AnimateTruck(){
		float brakePosThresh = 0.8f;
		if(!isGoingToHit && currDist > brakePosThresh) {
			float brake = 0.95f;
			truckSpeed *= brake;
		}

		float step = Time.deltaTime * truckSpeed;

		currDist = Mathf.Min(currDist + step, 1f);
		tr.position = path.GetPositionOnSpline(currDist);
		tr.rotation = path.GetOrientationOnSpline(currDist);
	}

	private void ActivateActionsBeforeHit(){
		float carHornThresh = 0.8f;
		if(currDist < carHornThresh || !isGoingToHit) { return; }

		// activate car horn, brake sound
		var soundMustBePlayed = truckSoundBeforeHit.clip != null && !truckSoundBeforeHit.isPlaying && !soundBeforeHitPlayed;
		if(soundMustBePlayed) {
			Debug.Log("sound before hit has played");
			truckSoundBeforeHit.Play();
			soundBeforeHitPlayed = true;
		}
	}

	public void ActivateActionsAfterHit(){
		// activate sound
		var soundMustBePlayed = truckSoundAfterHit.clip != null && !truckSoundAfterHit.isPlaying && !soundAfterHitPlayed;
		if(!soundMustBePlayed) { return; }

		float fadingPeriod = 0.2f;
		Debug.Log("sound after hit has played");
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
		if(camPosMod == null || camPosMod.isActivated) {return;}
		if(!camPosMod.isActivated) {
			camPosMod.SetPointsAndNormal(_contactPoint);
			camPosMod.isActivated = true;
		}
	}
}
