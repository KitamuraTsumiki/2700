﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// This class manages the choreography of the Guiding (second) scene.
/// </summary>

public class GuidingContentManager : ContentManager {

	enum State { Pause, FirstTruckComing, FirstTruckStops, InstructNavigation }

    public bool startFromPaused;

    // Contents of guiding (second) phase
    [SerializeField]
    private TruckComing truckComing;
    [SerializeField]
    private TruckStops truckStops;

	[SerializeField]
    private GameObject playerHead;
    [SerializeField]
    private GameObject firstTruck;
    [SerializeField]
    private GameObject secondTruck;
    [SerializeField]
    private Text typeOfEnding;

	private State state;
	private State lastState;

    private void OnEnable()
    {
        SetInitialState();
    }

    private void SetInitialState()
    {
        state = startFromPaused ? State.Pause : State.FirstTruckComing;
        lastState = State.FirstTruckComing;
    }

    protected override void Start () {
		// get the phase manager
		base.Start();
        SetInitialState();

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

        if (!truckStops.hasFinished) { return; }

		// move on the next phase "Accident"
		truckStops.enabled = false;
        MoveOnNextPhase();
        
	}

    private void MoveOnNextPhase() {
        var phaseManager = GetComponentInParent<PhaseManager>();
        if (phaseManager == null) { return; }

        switch (state)
        {
            case State.FirstTruckStops:
                phaseManager.ActivateAccidentPhase();
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
