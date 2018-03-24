using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnMouseDown () {
		Debug.Log ("sphere click!!!!!!!!!!!!!!!!!!!");
//		Vars.camDest.x = 1;
//		Vars.camDest.y = 2;
//		Vars.camDest.z = 3;

		// set refference
		Vars.refPosi.Set (-22, 10.7f, 7.1f);
		Vars.refAngle.Set (23.583f, -72.202f, -0.029f);
		// camera moving start
//		Vars.camMoveFlag = true;
	}
}
