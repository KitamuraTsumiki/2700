using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// This class manages the choreography of the Guiding (second) scene.
/// </summary>

public class GuidingContentManager : ContentManager {

	enum State { Pause, FirstTruckComing, FirstTruckStops, SecondTruckFound, SecondTruckComing, SecondTruckStops1, SecondTruckHits, SecondTruckStops2, InstructNavigation }

    // Contents of guiding (second) phase
    [SerializeField]
    private TruckComing truckComing;
    [SerializeField]
    private TruckStops truckStops;

	// Contents of accident (third) phase
    [SerializeField]
	private SecondTruckFound secondTruckFound;
    [SerializeField]
    private SecondTruckComing secondTruckComing;
    [SerializeField]
    private SecondTruckStops1 secondTruckStops1;
    [SerializeField]
    private SecondTruckHits secondTruckHits;
    [SerializeField]
    private SecondTruckStops2 secondTruckStops2;

    public GameObject playerHead;
    public GameObject firstTruck;
    public GameObject secondTruck;

    [SerializeField]
    private Text typeOfEnding;

	private State state;
	private State lastState;
    


	protected override void Start () {
		// get the phase manager
		base.Start();
		state = State.Pause;
		lastState = State.FirstTruckComing;

		// Turn all UI canvases off
		truckComing.InitUI();

		// Initialize "type of ending" UI panel (for prototyping)
		InitTypeOfEndDisplay();

		// deactivate all story block managers
		DeactivateAllStoryBlocks();
	}

	private void DeactivateAllStoryBlocks(){
		truckComing.enabled = false;
		truckStops.enabled = false;
		secondTruckFound.enabled = false;
		secondTruckComing.enabled = false;
		secondTruckStops1.enabled = false;
		secondTruckHits.enabled = false;
		secondTruckStops2.enabled = false;
	}

	public override void Pause(){
		if(state != State.Pause) {
            GetActiveContent().Pause();
			lastState = state;
			state = State.Pause;

		} else {
			state = lastState;
            GetActiveContent().Pause();
        }
	}

    private ContentSubBlock GetActiveContent() {
        ContentSubBlock content;

        switch (state)
        {
            case State.FirstTruckComing:
                content = truckComing;
                break;
            case State.FirstTruckStops:
                content = truckStops;
                break;
            case State.SecondTruckFound:
                content = secondTruckFound;
                break;
            case State.SecondTruckComing:
                content = secondTruckComing;
                break;
            case State.SecondTruckStops1:
                content = secondTruckStops1;
                break;
            case State.SecondTruckHits:
                content = secondTruckHits;
                break;
            case State.SecondTruckStops2:
                content = secondTruckStops2;
                break;
            default:
                content = null;
                break;
        }

        return content;
    }

	private void InitTypeOfEndDisplay(){
		typeOfEnding.text = "";
	}

	private void Update () {
        Debug.Log(state);
        ContentSubBlock content = GetActiveContent();
        if (content != null) {
            Debug.Log(GetActiveContent().isActive);
        }
        

        // call each step of the story
        TruckComing();
		InstructNavigation();
		TruckStops();
		SecondTruckFound();
		SecondTruckComing();
		SecondTruckStops1();
		SecondTruckHits();
		SecondTruckStops2();
        
		// for testing scene transition
		base.SceneSwitch();
	}

	private void TruckComing(){
		if(state != State.FirstTruckComing) { return;}
		truckComing.enabled = true;

        // set references of dynamic objects
        truckComing.truck = firstTruck;
        truckComing.playerHead = playerHead.transform;

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

        MoveOnNextPhase();
    }

	private void TruckStops(){
		if(state != State.FirstTruckStops) { return;}
		truckStops.enabled = true;

        // set references of dynamic objects
        truckStops.firstTruck = firstTruck.GetComponent<FirstTruckActions>();
        truckStops.playerHead = playerHead.transform;
        truckStops.secondTruck = secondTruck;

        if (truckStops.hasFinished) {
			// move on the next phase "Accident"
			truckStops.enabled = false;
			state = State.SecondTruckFound;
		}
	}

	protected void SecondTruckFound(){
		if(state != State.SecondTruckFound) { return;}
		secondTruckFound.enabled = true;

        // set references of dynamic objects
        secondTruckFound.playerHead = playerHead.transform;
        secondTruckFound.truck = secondTruck.transform;

        // split the route
        if (!secondTruckFound.hasFinished) { return; }
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

        // set references of dynamic objects
        secondTruckComing.playerHead = playerHead.transform;
        secondTruckComing.truck = secondTruck;

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

        if (!secondTruckStops1.hasFinished) { return; }
		// move on "instruction" phase
		secondTruckStops1.enabled = false;
		typeOfEnding.text = "講習フェイズへ";

        MoveOnNextPhase();
    }


	protected void SecondTruckHits(){
		if(state != State.SecondTruckHits) { return;}
		secondTruckHits.enabled = true;

        // set references of dynamic objects
        secondTruckHits.playerHead = playerHead;

        if (!secondTruckHits.hasFinished) { return; }
		// move on "replay" phase
		secondTruckHits.enabled = false;
		typeOfEnding.text = "リプレイフェイズへ";

        MoveOnNextPhase();
    }

	protected void SecondTruckStops2(){
		if(state != State.SecondTruckStops2) { return;}
		secondTruckStops2.enabled = true;

        // set references of dynamic objects
        secondTruckStops2.truckActions = secondTruck.GetComponent<SecondTruckActions>();

        if (!secondTruckStops2.hasFinished) { return; }
		// move on "instruction" phase
		secondTruckStops2.enabled = false;
		typeOfEnding.text = "講習フェイズへ";

        MoveOnNextPhase();
    }

    private void MoveOnNextPhase() {
        var phaseManager = GetComponentInParent<PhaseManager>();
        if (phaseManager == null) { return; }

        switch (state)
        {
            case State.SecondTruckStops1:
                phaseManager.ActivateinstructionPhase();
                break;
            case State.SecondTruckHits:
                phaseManager.ActivateReplayPhase();
                break;
            case State.SecondTruckStops2:
                phaseManager.ActivateinstructionPhase();
                break;
            case State.InstructNavigation:
                phaseManager.ActivateNavInstructionPhase();
                break;
            default:
                break;
        }

        gameObject.SetActive(false);
    }
}
