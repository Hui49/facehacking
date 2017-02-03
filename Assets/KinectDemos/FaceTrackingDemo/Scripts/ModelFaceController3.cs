using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelFaceController3 : MonoBehaviour 
{
	public enum AxisEnum { X, Y, Z };
	
	[Tooltip("Transform of the joint, used to move and rotate the head.")]
	public Transform HeadTransform;

	[Tooltip("Transform of the joint, used to move and rotate the left eye.")]
	public Transform LeftEyeTransform;

	[Tooltip("Transform of the joint, used to move and rotate the right eye.")]
	public Transform RightEyeTransform;

	private FacetrackingManager manager;
	private KinectInterop.DepthSensorPlatform platform;
	
	private Vector3 HeadInitialPosition;
	private Quaternion HeadInitialRotation;
	private Vector3 LeftEyeInitialPosition;
	private Quaternion LeftEyeInitialRotation;
	private Vector3 RightEyeInitialPosition;
	private Quaternion RightEyeInitialRotation;
	public bool mirroredHeadMovement = true;
	int blendShapeCount;
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;
	float blendOne = 0f;
	float blendTwo = 0f;
	float blendSpeed = 3f;
	bool blendOneFinished = false;
	bool isKissingMouth=false;
	bool isSmillingMouth=false;
	bool isSadMouth=false;
	float i=1;
	float smoothFactor=5f;
	void Awake ()
	{
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}
	
	void Start()
	{
		if(HeadTransform != null)
		{
			HeadInitialPosition = HeadTransform.localPosition;
			//HeadInitialPosition.z = 0;
			HeadInitialRotation = HeadTransform.localRotation;
		}

		KinectManager kinectManager = KinectManager.Instance;
		if(kinectManager && kinectManager.IsInitialized())
		{
			platform = kinectManager.GetSensorPlatform();
		}

		blendShapeCount = skinnedMesh.blendShapeCount; 

	}
	
	void Update() 

	{  // float translation = Time.deltaTime * 10;
		

			// get the face-tracking manager instance
			if (manager == null) {
				manager = FacetrackingManager.Instance;
			}

			if (manager && manager.IsTrackingFace ()) {
				// set head position & rotation
		//	FaceFrameReference faceRef = e.FrameReference;
			}

			}
		


	
	void setBlendShape(float weight,int index){
	
		if ( weight>0f) {
			blendOne=100*weight;
			//skinnedMeshRenderer.SetBlendShapeWeight (1, blendOne);
			//blendOne += blendSpeed;
		} else {
			blendOne =0f;
		}
		skinnedMeshRenderer.SetBlendShapeWeight (index, blendOne);
	}

	void setBlendShape1(float weight,int index){

		if ( weight>0f) {
			blendOne=100*weight;
			//skinnedMeshRenderer.SetBlendShapeWeight (1, blendOne);
			//blendOne += blendSpeed;
		} else {
			//blendOne =100f;
		}
		skinnedMeshRenderer.SetBlendShapeWeight (index, blendOne);
	}
	
}
