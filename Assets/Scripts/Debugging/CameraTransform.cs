using UnityEngine;
/// <summary>
/// This is an element to simulate viewing angle and position of vive HMD.
/// </summary>
public class CameraTransform : MonoBehaviour {

	public float rotationMul = 0.01f;
	public float translateMul = 0.01f;

	private void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationMul;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * translateMul;
	}


}
