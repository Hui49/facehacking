using UnityEngine;
using System.Collections;
using System;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("StartTime="+Time.time);
		IntegrationTest.Pass ();
	}


	void Update(){
		Debug.Log ("Time="+Time.time);
	}
}