using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Presentation: MonoBehaviour 
{
	[Tooltip("Whether the presentation slides can be changed with gestures (SwipeLeft & SwipeRight).")]
	public bool slideChangeWithGestures = true;
	[Tooltip("Whether the presentation slides can be changed with keys (PgDown & PgUp).")]
	public bool slideChangeWithKeys = true;
	[Tooltip("Speed of spinning, when presentation slides change.")]
	public int spinSpeed = 5;




	private bool isSpinning = false;


	private CubeGestureListener gestureListener;



	void Start() 
	{
		// hide mouse cursor
		Cursor.visible = false;

		// calculate max slides and textures
		//maxSides = cubeSides.Count;

	//	isSpinning = false;



	

		// get the gestures listener
		gestureListener = CubeGestureListener.Instance;
	}

	void Update() 
	{
		// dont run Update() if there is no gesture listener
		if(!gestureListener){


			return; }
		else{
	
			if(slideChangeWithKeys)
		{    	
			if (Input.GetKeyDown (KeyCode.Delete)) {
		
				NextPage ();
			}
				else if(Input.GetKeyDown(KeyCode.PageUp))
					LastPage ();
			}

			else if(slideChangeWithGestures && gestureListener)
			{
				
				Debug.Log ("caaaaaa");
				if (gestureListener.IsSwipeLeft ()) {

					NextPage ();
				} else if (gestureListener.IsSwipeRight ()) {
					LastPage ();
				} else {
					Debug.Log ("haaaaa");}
			}
		}
	}


	// rotates cube left
	private void NextPage()
	{
		GameObject m=GameObject.Find("Next");
		m.GetComponent<Button>().onClick.Invoke ();
	}

	// rotates cube right
	private void  LastPage()
	{
		GameObject m=GameObject.Find("Back");
		m.GetComponent<Button> ().onClick.Invoke ();
		//nextStepTime = 0f;
	}




}
