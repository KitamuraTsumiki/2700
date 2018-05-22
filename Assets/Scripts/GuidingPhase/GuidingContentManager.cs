using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class manages the choreography of the Guiding (second) scene.
/// </summary>

public class GuidingContentManager : ContentManager {

	enum State { FirstTruckComing, FirstTruckStops, SecondTruckFound, SecondTruckComing, SecondTruckStops1, SecondTruckHits, SecondTruckStops2, InstructNavigation }

	// Contents of guiding (second) phase
	public TruckComing truckComing;
	public TruckStops truckStops;

	// Contents of accident (third) phase
	public SecondTruckFound secondTruckFound;
	public SecondTruckComing secondTruckComing;
	public SecondTruckStops1 secondTruckStops1;
	public SecondTruckHits secondTruckHits;
	public SecondTruckStops2 secondTruckStops2;

	public GameObject playerHead;

	public Text typeOfEnding;

	private State state;


	protected override void Start () {
		// get the phase manager
		base.Start();
		state = State.FirstTruckComing;

		// Turn all UI canvases off
		truckComing.InitUI();

		// Initialize player's rigid body status
		InitPlayerRbd();

		// Initialize "type of ending" UI panel (for prototyping)
		InitTypeOfEndDisplay();

		// deactivate all story block managers
		truckComing.enabled = false;
		truckStops.enabled = false;
		secondTruckFound.enabled = false;
		secondTruckComing.enabled = false;
		secondTruckStops1.enabled = false;
		secondTruckHits.enabled = false;
		secondTruckStops2.enabled = false;
	}

	private void InitPlayerRbd(){
		Rigidbody playerRbd = playerHead.GetComponent<Rigidbody>();
		if(playerRbd != null) {
			playerRbd.isKinematic = true;
			playerRbd.useGravity = false;
		}
	}

	private void InitTypeOfEndDisplay(){
		typeOfEnding.text = "";
	}

	private void Update () {
		// call each step of the story
		TruckComing();
		InstructNavigation();
		TruckStops();
		SecondTruckFound();
		SecondTruckComing();
		SecondTruckStops1();
		SecondTruckHits();
		SecondTruckStops2();

		Debug.Log(state);

		// for testing scene transition
		base.SceneSwitch();
	}

	private void TruckComing(){
		if(state != State.FirstTruckComing) { return;}
		truckComing.enabled = true;

		if(!truckComing.hasFinished) { return; }
		if(truckComing.isInMainRoute) {
			// move on the next "TruckStop" (second) block in this phase
			truckComing.enabled = false;
			state = State.FirstTruckStops;
		} else{
			// move on "Instruction of correct position for navigation of trucks"
			truckComing.enabled = false;
			state = State.InstructNavigation;
		}
	}

	private void InstructNavigation(){
		if(state != State.InstructNavigation) { return;}
		// move on "Instruction of correct position for navigation of trucks"
		typeOfEnding.text = "正しい誘導位置の講習フェイズへ";
	}

	private void TruckStops(){
		if(state != State.FirstTruckStops) { return;}
		truckStops.enabled = true;

		if(truckStops.hasFinished) {
			// move on the next phase "Accident"
			truckStops.enabled = false;
			state = State.SecondTruckFound;
		}
	}

	protected void SecondTruckFound(){
		if(state != State.SecondTruckFound) { return;}
		secondTruckFound.enabled = true;

		// split the route
		if(!secondTruckFound.hasFinished) { return; }
		secondTruckFound.enabled = false;
		if(secondTruckFound.isInMainRoute) {
			state = State.SecondTruckComing;
		} else {
			state = State.SecondTruckStops1;
		}
	}

	protected void SecondTruckComing(){
		if(state != State.SecondTruckComing) { return;}
		secondTruckComing.enabled = true;

		// split the route
		if (!secondTruckComing.hasFinished){ return; }
		secondTruckComing.enabled = false;
		if(secondTruckComing.isInMainRoute) {
			state = State.SecondTruckHits;
		} else {
			state = State.SecondTruckStops2;
		}
	}

	protected void SecondTruckStops1(){
		if(state != State.SecondTruckStops1) { return;}
		secondTruckStops1.enabled = true;

		if(secondTruckStops1.hasFinished) {
			// move on "instruction" phase
			secondTruckStops1.enabled = false;
			typeOfEnding.text = "講習フェイズへ";
		}
	}


	protected void SecondTruckHits(){
		if(state != State.SecondTruckHits) { return;}
		secondTruckHits.enabled = true;

		if(secondTruckHits.hasFinished) {
			// move on "replay" phase
			secondTruckHits.enabled = false;
			typeOfEnding.text = "リプレイフェイズへ";
		}
	}

	protected void SecondTruckStops2(){
		if(state != State.SecondTruckStops2) { return;}
		secondTruckStops2.enabled = true;

		if(secondTruckStops2.hasFinished) {
			// move on "instruction" phase
			secondTruckStops2.enabled = false;
			typeOfEnding.text = "講習フェイズへ";
		}
	}
}
