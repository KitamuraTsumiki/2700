using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// This class manages choreography of the "navigation instruction" phase
/// </summary>
public class NavInstructionContentManager : ContentManager
{

    [SerializeField]
    private AudioSource[] narrations;

    [SerializeField]
    private UnityEvent startIntroNarration;
    [SerializeField]
    private UnityEvent startFirstTruckAction;
    [SerializeField]
    private UnityEvent startSecondNarration;
    [SerializeField]
    private UnityEvent startEndingNarration;

    private bool isPaused = false;
    private FirstTruckActions first;
    private TruckActions[] actions;

    protected override void Start () {
        base.Start();
        InitTrucks();
	}

    private void InitTrucks()
    {
        first = firstTruck.GetComponent<FirstTruckActions>();
        SecondTruckActions second = secondTruck.GetComponent<SecondTruckActions>();
        TruckActions[] actions = { first, second };

        foreach (TruckActions action in actions)
        {
            TruckActionControl.DeactivateTruckAction(action);
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

        TruckActionControl.DeactivateTruckAction(first);
    }

    public override void ExitPause()
    {
        if (!isPaused) { return; }
        isPaused = false;

        TruckActionControl.ActivateTruckAction(first);
    }

    private void Update () {
        Debug.Log("you're in the navigation instruction phase");
    }
}
