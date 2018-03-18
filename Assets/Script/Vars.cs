using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Vars : Object {
public static class Vars{

	public static Vector3 refPosi = new Vector3 ();
	public static Vector3 refAngle = new Vector3 ();
	public static Vector3 camPosi = new Vector3 ();
	public static Vector3 camAngle = new Vector3 ();
	public static bool camMoveFlag = false;
	public static bool camZoomed = false;

//	// camera movable detection
//	public static bool camMovable ()
//	{
//		return (!camMoveFlag && !camZoomed);
//	}

	// correction of refAngle
	public static void refAngCorrection ()
	{
		Vector3 corr = refAngle - camAngle;
		for (int i = 0; i < 3; i++) {
			if (corr [i] < -180)
				refAngle [i] += 360;
			else if (corr [i] > 180)
				refAngle [i] -= 360;
		}
	}

}
