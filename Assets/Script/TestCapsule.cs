using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCapsule : MonoBehaviour, IPointerDownHandler
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log ("Capsule click!!!!!!!!!!!!!!!!!!!!");
		if (!Vars.camMoveFlag && !Vars.camZoomed) {
			// set refference
			Vars.refPosi.Set (15.8f, 10.7f, 13.7f);
			Vars.refAngle.Set (15.762f, 52.852f, -0.008f);
			// camera moving start
//			Vars.camMoveFlag = true;
		}
	}

}
