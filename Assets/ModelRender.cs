using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModelRender : MonoBehaviour {
	private bool levelLoaded = false;

	// Use this for initialization
	public void SceneRender(){
		levelLoaded = true;
		GameObject t = GameObject.Find ("ScoreMenu/Text");
		string text = t.GetComponent<Text> ().text;
	
		Application.LoadLevel (text);
		//Application.LoadLevel ("Model1");

	}
}
