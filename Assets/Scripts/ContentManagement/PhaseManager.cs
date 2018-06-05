using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Phase manager manages dynamic objects of all phases of whole content.
/// </summary>

public class PhaseManager : MonoBehaviour {

    [SerializeField]
    private UnityEvent guidingPhase;
    [SerializeField]
    private UnityEvent accidentPhase;
    [SerializeField]
    private UnityEvent navInstructionPhase;
    [SerializeField]
    private UnityEvent instructionPhase;
    [SerializeField]
    private UnityEvent replayPhase;

    private void Start()
    {
        int phaseNum = transform.childCount;
        for (int i = 0; i < phaseNum; i++) {
            transform.GetChild(i).gameObject.SetActive(false) ;
        }
    }

    private void Update()
    {
        ActivateInitialPhaseTest();
        Pause();
    }

    private void ActivateInitialPhaseTest()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            guidingPhase.Invoke();
        }
    }

    public void Skip()
    {

    }

    public void Pause()
    {
        // keyboard interaction is for testing purpose
        if (Input.GetKeyDown(KeyCode.P))
        {
            ContentManager activePhase = GetActivePhase();
            if (activePhase == null) { return; }
            activePhase.Pause();
        }
    }

    private ContentManager GetActivePhase()
    {
        int phaseNum = transform.childCount;

        for (int i = 0; i < phaseNum; i++)
        {
            GameObject phaseObject = transform.GetChild(i).gameObject;
            if (!phaseObject.activeSelf) { continue; }
            return phaseObject.GetComponent<ContentManager>();
        }

        return null;
    }

    public void Reset()
    {
        
    }

    /// <summary>
    /// "Activate" functions are called at the end of phase
    /// in each content manager,
    /// and when the player selects particular phase on the UI
    /// </summary>
    
    public void ActivateGuidingPhase()
    {
        guidingPhase.Invoke();
    }

    public void ActivateAccidentPhase()
    {
        accidentPhase.Invoke();
    }

    public void ActivateReplayPhase()
    {
        replayPhase.Invoke();
    }

    public void ActivateNavInstructionPhase()
    {
        navInstructionPhase.Invoke();
    }

    public void ActivateinstructionPhase()
    {
        instructionPhase.Invoke();
    }
}
