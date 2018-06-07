using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// This class manages the "instruction" phase
/// </summary>
public class InstructionContentManager : ContentManager
{
    [SerializeField]
    AudioSource[] narrations;

    [SerializeField]
    GameObject worker;

    // those UnityEvents will be replaced with a dictionary of events and names probably
    [SerializeField]
    private UnityEvent startIntroNarration;
    [SerializeField]
    private UnityEvent startFirstTruckAction;
    [SerializeField]
    private UnityEvent startFirstWorkerAnim;
    [SerializeField]
    private UnityEvent startSecondNarration;
    [SerializeField]
    private UnityEvent startSecondTruckAction;
    [SerializeField]
    private UnityEvent startEndingNarration;
    
    private bool isPaused = false;
    private TruckActions[] actions;

    protected override void Start () {
        base.Start();
        InitTrucks();
        SetInitialState();
        SetInitCamRigPos();
    }

    protected override void SetInitialState()
    {
        isPaused = startFromPaused;
    }

    private void InitTrucks()
    {
        FirstTruckActions first = firstTruck.GetComponent<FirstTruckActions>();
        SecondTruckActions second = secondTruck.GetComponent<SecondTruckActions>();
        TruckActions[] temp = {first, second};
        actions = temp;

        foreach (TruckActions action in actions)
        {
            TruckActionControl.DeactivateTruckAction(action);
            action.ResetSpeed();
        }

        TruckActionControl.GuidingPhaseInitSetup(first, second);
    }

    /// <summary>
    /// pausing functions are called in the PhaseManager class
    /// </summary>
    public override void EnterPause()
    {
        if (isPaused) { return; }
        isPaused = true;

        foreach (TruckActions action in actions)
        {
            TruckActionControl.DeactivateTruckAction(action);
        }
    }

    public override void ExitPause()
    {
        if (!isPaused) { return; }
        isPaused = false;
        
        foreach (TruckActions action in actions)
        {
            TruckActionControl.ActivateTruckAction(action);
        }
    }

    private void Update () {
        Debug.Log("you're in the instruction phase");

        debug();
	}

    private void debug()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            startSecondTruckAction.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            startFirstTruckAction.Invoke();
        }
    }
}
