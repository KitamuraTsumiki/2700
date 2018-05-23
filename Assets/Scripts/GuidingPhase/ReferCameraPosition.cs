using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferCameraPosition : MonoBehaviour {

	public Transform camPosReference;

	private void Start () {
		
	}
	
	private void Update () {
		transform.position = camPosReference.position;
	}
}
