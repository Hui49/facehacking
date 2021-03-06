using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HandCtr : MonoBehaviour 
{

	[Tooltip("Smooth factor used for object rotations.")]
	public float smoothFactor = 3.0f;

	[Tooltip("GUI-Text used to display information messages.")]
	public GUIText infoGuiText;

	private InteractionManager manager;
	private bool isLeftHandPress;

	private GameObject selectedObject;

	void Update() 
	{
		manager = InteractionManager.Instance;

		if(manager != null && manager.IsInteractionInited())
		{
			Vector3 screenNormalPos = Vector3.zero;
			Vector3 screenPixelPos = Vector3.zero;

		//	if(selectedObject == null)
			//{
				// no object is currently selected or dragged.
				// if there is a hand grip, try to select the underlying object and start dragging it.

					
				if(manager.GetLastRightHandEvent() == InteractionManager.HandEventType.Grip)
					{
						//isLeftHandPress = true;
						//screenNormalPos = manager.GetLeftHandScreenPos();
				      GetComponent<Button> ().onClick.Invoke ();
					}

		}}
}
