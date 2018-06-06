using UnityEngine;
/// <summary>
/// This class includes functions to control behavior of trucks
/// </summary>
public class TruckActions : MonoBehaviour {

    // animation parameters
    [SerializeField]
    protected Spline path;
    [SerializeField, Range(0f, 1f)]
    protected float currDist;
    [SerializeField, Range(0f, 1f)]
    protected float currRot;
    protected float distance;
    [SerializeField]
    protected Transform tr;
    [SerializeField]
    protected Transform startingPoint;
    [SerializeField]
    protected float truckSpeed = 0.1f;

    public bool isActive
    {
        get;
        set;
    }

    public void SetAtStartPosition()
    {
        currDist = 0f;

        SetTransform();
    }

    public void SetAtEndPosition()
    {
        currDist = 1f;

        SetTransform();
    }

    protected void SetTransform() {
        tr.position = path.GetPositionOnSpline(currDist);
        tr.rotation = path.GetOrientationOnSpline(currDist);
    }

}
