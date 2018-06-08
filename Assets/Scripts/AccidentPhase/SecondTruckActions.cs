using UnityEngine;
/// <summary>
/// This class defines all actions of the second truck,
/// which hits the player
/// </summary>
public class SecondTruckActions : TruckActions
{

	public bool isGoingToHit;
	public bool isContacting = false;
	public AudioSource truckSoundBeforeHit;
	public AudioSource truckSoundAfterHit;
	public GuidingContentManager contentManager;

	private bool soundBeforeHitPlayed = false;
	private bool soundAfterHitPlayed = false;

	[SerializeField]
	private CameraPosModification camPosMod;

    public float currPosOnPath {
		get{ return currDist; }
	}

    private void OnEnable()
    {
        isGoingToHit = true;
    }

    private void Start () {
		SetAtStartPosition();
        RecordOriginalSpeed();
        gameObject.SetActive(false);
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
		SetTransform();
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
