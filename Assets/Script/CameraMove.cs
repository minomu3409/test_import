using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public GameObject bullet;	// copied rigidBody

	private bool rotateLeft = false;
	private bool rotateRight = false;

	private float zoomDist = 30;

	private bool zooming = false;
//	private bool zoomForward = true;
//	private Vector3 zoomPosition;
//	private Vector3 zoomRefPosition;
//	private Vector3 zoomAngle;
//	private Vector3 zoomRefAngle;
	private int zoomCount = 0;
	private int zoomFrame = 60;

	private bool firstCamMove = true;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Pawn rigid body
		if (Input.GetMouseButtonDown (0)) {
			GameObject bullets = GameObject.Instantiate (bullet)as GameObject;
			bullets.transform.position = this.transform.position;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bullets.GetComponent<Rigidbody>().velocity=120*(ray.direction);
		}

		// rotation
		if (zooming || (rotateLeft && rotateRight)) {	// except
		} else if (rotateLeft) {
			transform.Rotate (new Vector3 (0, -2, 0));
		} else if (rotateRight) {
			transform.Rotate (new Vector3 (0, 2, 0));
		}

		// camera moving
		if (Vars.camMoveFlag) {
			if (firstCamMove) {
				Vars.camPosi = transform.position;	// record first position
				Vars.camAngle = transform.eulerAngles;	// record first angles
				Vars.refAngCorrection ();
				firstCamMove = false;
			}
			//// sine curve moving
			transform.position = Vars.camPosi + (Vars.refPosi - Vars.camPosi) * 1 / 2 * (1 - Mathf.Cos (Mathf.PI * (float)zoomCount / (float)zoomFrame));
			transform.eulerAngles = Vars.camAngle + (Vars.refAngle - Vars.camAngle) * 1 / 2 * (1 - Mathf.Cos (Mathf.PI * (float)zoomCount / (float)zoomFrame));

			zoomCount++;

			if (zoomCount >= zoomFrame) {
				Vars.camMoveFlag = false;
				zoomCount = 0;
				Vars.camZoomed = true;	// temporary
				firstCamMove = true;
			}
		}

		// zoom
		if (zooming) {
			// forward & backward
			//// linear
			//			transform.position = zoomPosition + (zoomRefPosition - zoomPosition) * (float)zoomCount / (float)zoomFrame;
			//			transform.eulerAngles = zoomAngle + (zoomRefAngle - zoomAngle) * (float)zoomCount / (float)zoomFrame;
			//// sine curve
//			transform.position = zoomPosition + (zoomRefPosition - zoomPosition) * 1 / 2 * (1 - Mathf.Cos (Mathf.PI * (float)zoomCount / (float)zoomFrame));
//			transform.eulerAngles = zoomAngle + (zoomRefAngle - zoomAngle) * 1 / 2 * (1 - Mathf.Cos (Mathf.PI * (float)zoomCount / (float)zoomFrame));

			transform.position = Vars.camPosi + (Vars.refPosi - Vars.camPosi) * 1 / 2 * (1 - Mathf.Cos (Mathf.PI * (float)zoomCount / (float)zoomFrame));
			transform.eulerAngles = Vars.camAngle + (Vars.refAngle - Vars.camAngle) * 1 / 2 * (1 - Mathf.Cos (Mathf.PI * (float)zoomCount / (float)zoomFrame));
			zoomCount++;

			if (zoomCount >= zoomFrame) {
				zooming = false;
				zoomCount = 0;
				Vars.camZoomed = !Vars.camZoomed;
			}
		}

		// click test
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				GameObject result = hit.collider.gameObject;
				if (result.CompareTag ("TestCube")) {
					Debug.Log ("cube click!!!!!!");
				}
			}
		}
	}

	public void Transport ()
	{
		if (!zooming && !Vars.camZoomed) {
			Vector3 refPosi = new Vector3 (-0.7569315f, 24.1f, 46.1f);
			Vector3 refAng = new Vector3 (21.267f, 161.151f, 0);

//			// forward only
//			zoomRefPosition = refPosi;	// refference position
//			zoomRefAngle = refAng;	// refference angles
//			zooming = true;
//			zoomPosition = transform.position;	// record first position
//			zoomAngle = transform.eulerAngles;	// record first angles
			Vars.refPosi = refPosi;	// refference position
			Vars.refAngle = refAng;	// refference angles
			zooming = true;
			Vars.camPosi = transform.position;	// record first position
			Vars.camAngle = transform.eulerAngles;	// record first angles
			// addtional correction
			//			zoomRefCorrection ();
			Vars.refAngCorrection ();
		}
	}

	public void Zoom ()
	{
		if (!zooming && !Vars.camZoomed) {
			// forward
			Vars.refPosi = transform.position + transform.forward * zoomDist;	// refference position
			Vars.refAngle = transform.eulerAngles;	// refference angles
			zooming = true;
			Vars.camPosi = transform.position;	// record first position
			Vars.camAngle = transform.eulerAngles;	// record first angles
			// addtional correction
//			zoomRefCorrection ();
			Vars.refAngCorrection ();
		} else if (!zooming && Vars.camZoomed) {
			// backward
			Vars.refPosi = Vars.camPosi;	// refference position
			Vars.refAngle = Vars.camAngle;	// refference angles
			zooming = true;
			Vars.camPosi = transform.position;	// record final zoomed position
			Vars.camAngle = transform.eulerAngles;	// record final zoomed angles
			// addtional correction
//			zoomRefCorrection ();
			Vars.refAngCorrection ();
		}
	}
//	// correction of zoomRefAngle
//	private void zoomRefCorrection ()
//	{
//		Vector3 corr = zoomRefAngle - zoomAngle;
//		for (int i = 0; i < 3; i++) {
//			if (corr [i] < -180)
//				zoomRefAngle [i] += 360;
//			else if (corr [i] > 180)
//				zoomRefAngle [i] -= 360;
//		}
//	}

	// Left & right button flag
	public void ContinuousRotateLeftDown ()
	{
		rotateLeft = true;
	}

	public void ContinuousRotateLeftUp ()
	{
		rotateLeft = false;
	}

	public void ContinuousRotateRightDown ()
	{
		rotateRight = true;
	}

	public void ContinuousRotateRightUp ()
	{
		rotateRight = false;
	}


	// Left & right rotation
	public void RotateLeft ()
	{
		transform.Rotate (new Vector3 (0, -45, 0));
	}

	public void RotateRight ()
	{
		transform.Rotate (new Vector3 (0, 45, 0));
	}
}
