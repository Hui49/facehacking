using UnityEngine;
using System.Collections;

public class loadBack : MonoBehaviour {

	private bool levelLoaded = false;

	// Use this for initialization
	public void SceneRender(){
		levelLoaded = true;


		Application.LoadLevel ("MenuScene");
		//Application.LoadLevel ("Model1");

	}
}