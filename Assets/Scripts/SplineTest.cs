using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class SplineTest : MonoBehaviour {

	[SerializeField]
	private Spline spline;
	[SerializeField,Range(0f,1f)]
	private float currDist;
	[SerializeField, Range(0f, 1f)]
	private float currRot;

	private float distance;

	[SerializeField]
	private Transform tr;

	// Use this for initialization
	void Start () {
		distance = spline.Length;
        
	}
	
	// Update is called once per frame
	void Update () {
		tr.position = spline.GetPositionOnSpline(currDist);
		tr.rotation = spline.GetOrientationOnSpline(currRot);
	}
}
