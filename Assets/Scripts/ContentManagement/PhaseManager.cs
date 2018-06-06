using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Phase manager manages dynamic objects of all phases of whole content.
/// </summary>

public class PhaseManager : MonoBehaviour {

    [SerializeField]
    private UnityEvent introductionPhase;
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

        // initialize the phase after integration of the introduction phase
        //ActivateIntroductionPhase();
    }

    private void Update()
    {
        ActivateInitialPhaseTest();

        // for the test of phase control features
        Pause();
        Skip();
        Reset();
    }

    private void ActivateInitialPhaseTest()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            guidingPhase.Invoke();
        }
    }

    /// <summary>
    /// skip will be called from a UI panel in the final product
    /// </summary>
    public void Skip()
    {
        // this feature is available in only "introduction" and "guiding" phase
        // keyboard interaction is for testing purpose
        if (!Input.GetKeyDown(KeyCode.Alpha1)) { return; }

        // get activated phase
        var activePhase = GetActivePhase();
        if(activePhase == null) { return; }

        bool isIntroductionPhase = activePhase.GetType() == typeof(IntroductionContentManager);
        bool isGuidingPhase = activePhase.GetType() == typeof(GuidingContentManager);
        
        bool canSkip = isIntroductionPhase || isGuidingPhase;
        
        if (!canSkip) { return; }

        // move on next phase
        if (isIntroductionPhase) {
            activePhase.gameObject.GetComponent<IntroductionContentManager>().MoveOnNextPhase();
            return;
        }
        activePhase.gameObject.GetComponent<GuidingContentManager>().MoveOnNextPhase();
    }

    /// <summary>
    /// pause will be called from a UI panel in the final product
    /// </summary>
    public void Pause()
    {
        // keyboard interaction is for testing purpose
        if (!Input.GetKeyDown(KeyCode.P)) { return; }

        ContentManager activePhase = GetActivePhase();
        if (activePhase == null) { return; }
        activePhase.Pause();
        
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

    /// <summary>
    /// reset will be called from a UI panel in the final product
    /// </summary>
    public void Reset()
    {
        // keyboard interaction is for testing purpose
        if (!Input.GetKeyDown(KeyCode.Alpha2)) { return; }

        // stop activated phase
        GetActivePhase().gameObject.SetActive(false);

        // reload current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// "Activate" functions are called at the end of phase
    /// in each content manager,
    /// and when the player selects particular phase on the UI
    /// </summary>

    public void ActivateIntroductionPhase()
    {
        introductionPhase.Invoke();
    }

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
