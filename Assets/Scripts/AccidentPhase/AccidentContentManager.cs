using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages choreography of the "accident" phase.
/// </summary>
/// 
public class AccidentContentManager : ContentManager
{

    enum State { Pause, SecondTruckFound, SecondTruckComing, SecondTruckStops1, SecondTruckHits, SecondTruckStops2 }

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

    [SerializeField]
    private GameObject playerHead;
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

    protected override void SetInitialState() {
        state = startFromPaused ? State.Pause : State.SecondTruckFound;
        lastState = State.SecondTruckFound;
    }

    protected override void Start()
    {
        // get the phase manager
        base.Start();
        SetInitialState();

        // Turn all UI canvases off
        secondTruckFound.InitUI();

        // Initialize "type of ending" UI panel (for prototyping)
        InitTypeOfEndDisplay();

        // deactivate all story block managers
        DeactivateAllStoryBlocks();
    }

    private void DeactivateAllStoryBlocks()
    {
        secondTruckFound.enabled = false;
        secondTruckComing.enabled = false;
        secondTruckStops1.enabled = false;
        secondTruckHits.enabled = false;
        secondTruckStops2.enabled = false;
    }

    public override void EnterPause()
    {
        if (state == State.Pause) { return; }
        GetActiveContent().Pause();
        lastState = state;
        state = State.Pause;
    }

    public override void ExitPause()
    {
        if (state != State.Pause) { return; }
        state = lastState;
        GetActiveContent().Pause();
    }

    private ContentSubBlock GetActiveContent()
    {
        ContentSubBlock content;

        switch (state)
        {
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

    private void InitTypeOfEndDisplay()
    {
        typeOfEnding.text = "";
    }

    private void Update()
    {
        Debug.Log(state);
        ContentSubBlock content = GetActiveContent();
        if (content != null)
        {
            Debug.Log(GetActiveContent().isActive);
        }


        // call each step of the story
        SecondTruckFound();
        SecondTruckComing();
        SecondTruckStops1();
        SecondTruckHits();
        SecondTruckStops2();
    }

    protected void SecondTruckFound()
    {
        if (state != State.SecondTruckFound) { return; }
        secondTruckFound.enabled = true;

        // set references of dynamic objects
        secondTruckFound.playerHead = playerHead.transform;
        secondTruckFound.truck = secondTruck.transform;

        // split the route
        if (!secondTruckFound.hasFinished) { return; }
        secondTruckFound.enabled = false;

        state = secondTruckFound.isInMainRoute ? State.SecondTruckComing : State.SecondTruckStops1;
    }

    protected void SecondTruckComing()
    {
        if (state != State.SecondTruckComing) { return; }
        secondTruckComing.enabled = true;

        // set references of dynamic objects
        secondTruckComing.playerHead = playerHead.transform;
        secondTruckComing.truck = secondTruck;

        // split the route
        if (!secondTruckComing.hasFinished) { return; }
        secondTruckComing.enabled = false;

        state = secondTruckComing.isInMainRoute ? state = State.SecondTruckHits : State.SecondTruckStops2;
    }

    protected void SecondTruckStops1()
    {
        if (state != State.SecondTruckStops1) { return; }
        secondTruckStops1.enabled = true;

        if (!secondTruckStops1.hasFinished) { return; }
        // move on "instruction" phase
        secondTruckStops1.enabled = false;
        typeOfEnding.text = "講習フェイズへ";

        MoveOnNextPhase();
    }


    protected void SecondTruckHits()
    {
        if (state != State.SecondTruckHits) { return; }
        secondTruckHits.enabled = true;

        // set references of dynamic objects
        secondTruckHits.playerHead = playerHead;

        if (!secondTruckHits.hasFinished) { return; }
        // move on "replay" phase
        secondTruckHits.enabled = false;
        typeOfEnding.text = "リプレイフェイズへ";

        MoveOnNextPhase();
    }

    protected void SecondTruckStops2()
    {
        if (state != State.SecondTruckStops2) { return; }
        secondTruckStops2.enabled = true;

        // set references of dynamic objects
        secondTruckStops2.truckActions = secondTruck.GetComponent<SecondTruckActions>();

        if (!secondTruckStops2.hasFinished) { return; }
        // move on "instruction" phase
        secondTruckStops2.enabled = false;
        typeOfEnding.text = "講習フェイズへ";

        MoveOnNextPhase();
    }
    
    private void MoveOnNextPhase()
    {
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
            default:
                break;
        }

        gameObject.SetActive(false);
    }
}
