﻿using UnityEngine;
/// <summary>
/// This manages the first block of the Accident (third) phase;
/// check whether the player finds the second truck
/// </summary>
public class SecondTruckFound : ContentSubBlock {

    public Transform truck;
    public Transform playerHead;
    
    [SerializeField]
    private CanvasGroup[] canvases;

    private float truckCheckPeriod = 3f;
	private float truckCheckDeadline;
	private SecondTruckActions truckActions;

	private void Start(){
        base.Start();
       truckCheckDeadline = 0.7f;
    }

    public void InitUI()
    {
        GuideCanvasControl.TurnGroupOff(canvases);
    }

    private void StartTruckActions(){
		SecondTruckActions truckActions = truck.GetComponent<SecondTruckActions>();

		// activate truck animation if the "SecondTruckActions" is not active
		if(truckActions.isActive) {return;}
		truckActions.isActive = true;
	}

	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }
        StartTruckActions();

        ActivateDynamicObjectStatus();
        DeactivateDynamicObjectStatus();

        if (!isActive) { return; }
        ActivateTruckAction();
		CheckTruckRecognition();
	}

    private bool CheckDynamicObjectReference()
    {
        var truckAndPlayerAreAssigned = truck != null && playerHead != null;
        if (!truckAndPlayerAreAssigned) { return false; }

        if (truckActions != null){ return true; }

        truckActions = truck.GetComponent<SecondTruckActions>();
        truckActions.SetAtStartPosition();
        return true;
    }

    /// <summary>
    /// pausing function is called by "content manager" class
    /// in a function with the same name.
    /// it can be triggered by UI panels
    /// </summary>
    public override void Pause()
    {
        base.Pause();
    }

    protected override void ActivateDynamicObjectStatus()
    {
        if (truckActions.isActive == isActive) { return; }
        TruckActionControl.ActivateTruckAction(truckActions);
    }

    protected override void DeactivateDynamicObjectStatus()
    {
        if (truckActions.isActive == isActive) { return; }
        TruckActionControl.DeactivateTruckAction(truckActions);
    }

    private void CheckTruckRecognition(){
		if(PlayerSawTruck()) {
			// move on instruction phase
			Debug.Log("SecondTruckFound has finished (instruction route)");
			truckActions.isGoingToHit = false;
			isInMainRoute = false;
			hasFinished = true;
		}

		CheckTruckRecognitionDeadline();
	}

	private void CheckTruckRecognitionDeadline(){
		if (truckActions.currPosOnPath > truckCheckDeadline){ // if the player doesn't notice the second truck
			// move on "SecondTruckComing"
			Debug.Log("SecondTruckFound has finished (main route)");

			hasFinished = true;
		}
	}

	private bool PlayerSawTruck(){
		float viewDirThreshold = 0.5f;
		Vector3 truckDir = Vector3.Normalize(truck.transform.position
			- new Vector3(playerHead.position.x, truck.transform.position.y, playerHead.position.z));
		Vector3 viewAngle = playerHead.transform.forward.normalized;

		float viewAngleDotTruckDir = Vector3.Dot(viewAngle, truckDir);

		return viewAngleDotTruckDir > viewDirThreshold;
	}

	private void ActivateTruckAction(){
		truckActions.isGoingToHit = true;

		if(!truckActions.isActive) {
			truckActions.isActive = true;
		}
	}
}
