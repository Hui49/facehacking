 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ImageRender : MonoBehaviour {


	[SerializeField]
	private string m_animationPropertyName;
	//[SerializeField]
	//private GameObject m_object;

	[SerializeField]
	private Character m_initialChar;

	[SerializeField]
	private List<Character> m_navigationHistory;
	private string sceneName;
	 


	//private int index;
	public GameObject n1;
	public GameObject n2;
	//Character target; 

	//private Character newChar;
	public void GoBack()
	{	


		//m_navigationHistory[m_navigationHistory.Count-1];
		m_navigationHistory.RemoveAt(m_navigationHistory.Count-1);
		Animate(m_navigationHistory.Count-1);
		Debug.Log (m_navigationHistory.Count.ToString ());
	
	
		if (m_navigationHistory.Count ==2) {
			n1.SetActive (true);
		} 

		if (m_navigationHistory.Count ==1) {
		//	n2 = n1;
			n2.SetActive (false);
		} 
		//index = -1;
	}

	public void GoNext()
	{ 
		Debug.Log (m_navigationHistory.Count.ToString ());
		//newChar=new Character(1);
		Character target=new Character(m_navigationHistory.Count);

		Animate(m_navigationHistory.Count);
	//	index =+1;
	//	Animate(m_navigationHistory[m_navigationHistory.Count 1]);
		m_navigationHistory.Add(target);
	
		GameObject n = GameObject.Find ("ScoreMenu/Next");
		if (m_navigationHistory.Count == 3) {

			n1=n;
			//n2 = n;
			n.SetActive (false);
		} else {
			n.SetActive (true);	}
		if (m_navigationHistory.Count ==2) {
			
			n2.SetActive (true);
		} 
	
		  
	}

	private void Animate(int index)
	{
		Character target = new Character (index);

		GameObject t = GameObject.Find ("ScoreMenu/Text");
		string str = target.getname ();
		Text text = t.GetComponent<Text> ();
		text.text = str;
		sceneName = str;
		GameObject m = GameObject.Find ("Model");
		SpriteRenderer spr = m.GetComponent<SpriteRenderer> ();
	
		spr.sprite = Resources.Load <Sprite> (str);
		Debug.Log (index.ToString ());
	
	
			//	target.
			//	target.SetActive(true);

			/*Canvas canvasComponent = target.GetComponent<Canvas>();
		if (canvasComponent != null)
		{
			canvasComponent.overrideSorting = true;
			canvasComponent.sortingOrder = m_navigationHistory.Count;
		}

		Animator animatorComponent = target.GetComponent<Animator>();
		if (animatorComponent != null)
		{
			animatorComponent.SetBool(m_animationPropertyName, direction);
		}*/
		}


	 void Awake()
	{
		int index = 0;
		Character initial_target = new Character (index);
		m_navigationHistory = new List<Character>{initial_target};
		Animate (index);
       GameObject gameobject1=GameObject.Find ("ScoreMenu/Back");

		n2 = gameobject1;
		gameobject1.SetActive (false);
	}
}
