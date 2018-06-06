using UnityEngine;
/// <summary>
/// This class controls alpha of canvas group
/// </summary>
public class GuideCanvasControl {

    public static void TurnOn(CanvasGroup _canvas) {
        _canvas.alpha = 1f;
    }

    public static void TurnOff(CanvasGroup _canvas)
    {
        _canvas.alpha = 0f;
    }

    public static void TurnGroupOn(CanvasGroup[] _canvases) {
        foreach (CanvasGroup canvas in _canvases)
        {
            GuideCanvasControl.TurnOn(canvas);
        }
    }

    public static void TurnGroupOff(CanvasGroup[] _canvases)
    {
        foreach (CanvasGroup canvas in _canvases)
        {
            GuideCanvasControl.TurnOff(canvas);
        }
    }

    public static void FadeIn(CanvasGroup _canvas) {
        _canvas.alpha = Mathf.Min(_canvas.alpha + Time.deltaTime, 1f);
    }

    public static void FadeOut(CanvasGroup _canvas)
    {
        _canvas.alpha = Mathf.Max(_canvas.alpha + Time.deltaTime, 0f);
    }
}
