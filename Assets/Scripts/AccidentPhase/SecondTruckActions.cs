using UnityEngine;
/// <summary>
/// This class defines all actions of the second truck,
/// which hits the player
/// </summary>
public class SecondTruckActions : MonoBehaviour {

	public bool isActive = false;
	public bool hitting = false;
	public AudioSource truckSoundBeforeHit;
	public AudioSource truckSoundAfterHit;
	public GuidingContentManager contentManager;
	private Animator truckAnimation;
	private string truckComingAnimState = "Truck_Lane2_Hit";


	private void Start () {
		truckAnimation = GetComponent<Animator>();
		truckSoundBeforeHit = GetComponent<AudioSource>();
	}
	
	private void Update () {
		if(contentManager.secondTruckComing.enabled) {
			ActivateActionsBeforeHit();
		}
	}

	private void ActivateActionsBeforeHit(){
		// activate car horn, brake sound
		if(truckSoundBeforeHit.clip != null) {
			truckSoundBeforeHit.Play();
		}

		// animate the truck
		truckAnimation.Play(truckComingAnimState);

		if(!hitting) {
			// cross fade the animation to "stopping" one slightly
		}
	}

	public void ActivateActionsAfterHit(){
		// activate sound
		if(truckSoundAfterHit.clip != null) {
			truckSoundAfterHit.Play();
		}
	}

	private void OnCollisionEnter(Collision collision){
		// when the collider contacts player's head, "hitting" is activated
		if(collision.gameObject.tag == "MainCamera") {
			hitting = true;
		}
	}
}
