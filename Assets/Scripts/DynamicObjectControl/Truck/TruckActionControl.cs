using UnityEngine;
/// <summary>
/// This class controls status and actions of all trucks.
/// </summary>
public class TruckActionControl {

    /// <summary>
    /// InitSetups are called in the Start function of the "ContentManager" class
    /// to initialize trucks even if the phase starts with "pause" state.
    /// </summary>

    // for introduction phase
	public static void IntroductionPhaseInitSetup(FirstTruckActions _firstTruck, SecondTruckActions _secondTruck)
    {
        TruckActions[] truckActions = { _firstTruck, _secondTruck };

        foreach (TruckActions truckAction in truckActions)
        {
            truckAction.SetAtStartPosition();
            DeactivateTruckObject(truckAction);
        }
    }

    // for guiding, navigatoin instruction, instruction and replay phase
    public static void GuidingPhaseInitSetup(FirstTruckActions _firstTruck, SecondTruckActions _secondTruck)
    {
        _firstTruck.SetAtStartPosition();
        _secondTruck.SetAtStartPosition();

        ActivateTruckObject(_firstTruck);
        DeactivateTruckObject(_secondTruck);
    }

    // for accident phase
    public static void AccidentPhaseInitSetup(FirstTruckActions _firstTruck, SecondTruckActions _secondTruck)
    {
        _firstTruck.SetAtEndPosition();
        _secondTruck.SetAtStartPosition();

        TruckActions[] truckActions = { _firstTruck, _secondTruck };

        foreach (TruckActions truckAction in truckActions)
        {
            ActivateTruckObject(truckAction);
        }
    }

    public static void ActivateTruckAction(TruckActions _truckAction) {
        if (_truckAction.isActive) { return; }
        _truckAction.isActive = true;
    }

    public static void DeactivateTruckAction(TruckActions _truckAction)
    {
        if (!_truckAction.isActive) { return; }
        _truckAction.isActive = false;
    }

    public static void ActivateTruckObject(TruckActions _truckActions)
    {
        GameObject truck = _truckActions.gameObject;
        if(truck.activeSelf) { return; }
        _truckActions.gameObject.SetActive(true);
    }

    public static void DeactivateTruckObject(TruckActions _truckActions)
    {
        GameObject truck = _truckActions.gameObject;
        if (!truck.activeSelf) { return; }
        _truckActions.gameObject.SetActive(false);
    }
}
