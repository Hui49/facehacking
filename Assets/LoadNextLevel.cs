using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadNextLevel : MonoBehaviour
{
	
	private SpeechManager speechManager;
	private ImageRender imageManager;

	private bool jumpNow;
	private bool waveNow;

	private bool levelLoaded = false;




	void FixedUpdate ()
	{
		// get the speech manager instance
		if (speechManager == null) {
			speechManager = SpeechManager.Instance;
		}

		if (speechManager != null && speechManager.IsSapiInitialized ()) {
			if (speechManager.IsPhraseRecognized ()) {
				string sPhraseTag = speechManager.GetPhraseTagRecognized ();

				switch (sPhraseTag) {
				case "FORWARD":
					//GameObject m=GameObject.Find("Next");
					levelLoaded = true;
				Application.LoadLevel (3);
					NextPage ();

					break;

				case "Next":
					//GameObject m=GameObject.Find("Next");
					//levelLoaded = true;
				//	Application.LoadLevel (3);
					//NextPage ();
				//	levelLoaded = true;
				//	Application.LoadLevel (3);
					break;


				case "Back":
					//GameObject m=GameObject.Find("Next");
					//levelLoaded = true;
					//	Application.LoadLevel (3);
					LastPage();
					break;


				}

				speechManager.ClearPhraseRecognized ();
			}

		} else {
			Debug.Log ("FAIL TO RECOGNIZE ");
		}

	}


	private void NextPage()
	{
		GameObject m=GameObject.Find("Next");
		m.GetComponent<Button>().onClick.Invoke ();
	}

	private void  LastPage()
	{
		GameObject m=GameObject.Find("Back");
		m.GetComponent<Button> ().onClick.Invoke ();
		//nextStepTime = 0f;
	}


}