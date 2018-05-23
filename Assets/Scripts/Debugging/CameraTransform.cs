using UnityEngine;
/// <summary>
/// This is an element to simulate viewing angle and position of vive HMD.
/// </summary>
public class CameraTransform : MonoBehaviour {

	public float rotationMul = 1f;
	public float translateMul = 1f;

	private void Update () {
		CameraTranslate();
		CameraRotation();
	}

	private void CameraTranslate(){
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * translateMul;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * translateMul;

		gameObject.transform.Translate(new Vector3(x, 0f, z));
	}

	private void CameraRotation(){
		float horizontal = Input.GetAxis("Mouse X") * rotationMul;

		gameObject.transform.Rotate(new Vector3(0f, horizontal, 0f));
	}
}
