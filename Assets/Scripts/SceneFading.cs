using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class enables fade in / out of the scene for the transition of the phases
/// </summary>
public class SceneFading : MonoBehaviour {

    [SerializeField]
    private CanvasGroup canvas;
    [SerializeField]
    private float step = 0.01f;
    private bool isActive = false;

    public float blackMatteAlpha { get; private set; }

    private void Start () {
        Init();
	}
	
    private void Init()
    {
        canvas.alpha = 1f;
    }

    /// <summary>
    /// FadeOut and FadeIn are called in the content manager class
    /// at the beginnning and the end of each phases
    /// </summary>
    private void FadeOut()
    {
        if (!isActive) { return; }
        canvas.alpha = Mathf.Min(canvas.alpha + step, 1f);
        if (canvas.alpha < 1f) { return; }
        isActive = false;
    }

    private void FadeIn()
    {
        if (!isActive) { return; }
        canvas.alpha = Mathf.Max(canvas.alpha + step, 0f);
        if (canvas.alpha > 0f) { return; }
        isActive = false;
    }

	void Update () {
		
	}
}
