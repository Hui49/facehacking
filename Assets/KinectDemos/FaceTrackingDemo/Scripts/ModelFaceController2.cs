using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelFaceController2 : MonoBehaviour 
{
	public enum AxisEnum { X, Y, Z };
	
	[Tooltip("Transform of the joint, used to move and rotate the head.")]
	public Transform HeadTransform;

	[Tooltip("Transform of the joint, used to move and rotate the left eye.")]
	public Transform LeftEyeTransform;

	[Tooltip("Transform of the joint, used to move and rotate the right eye.")]
	public Transform RightEyeTransform;

	[Tooltip("value used to controll Lippucker.")]
	public float Value_Lippucker;

	[Tooltip("value used to controll jaw.")]
	public float Value_JawOpen;

	[Tooltip("value used to controll lip stretch.")]
	public float Value_Lipstretcher;

	[Tooltip("value used to controll lip corner depressor.")]
	public float Value_LipCornerDepressor;


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
	bool isOpenMouth=false;
	public	float i=1;
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
		if(LeftEyeTransform != null)
		{
			LeftEyeInitialPosition = LeftEyeTransform.localPosition;
			//HeadInitialPosition.z = 0;
			LeftEyeInitialRotation = LeftEyeTransform.localRotation;
		}
		if(RightEyeTransform != null)
		{
			RightEyeInitialPosition = LeftEyeTransform.localPosition;
			//HeadInitialPosition.z = 0;
			RightEyeInitialRotation = LeftEyeTransform.localRotation;
		}

		KinectManager kinectManager = KinectManager.Instance;
		if(kinectManager && kinectManager.IsInitialized())
		{
			platform = kinectManager.GetSensorPlatform();
		}

		blendShapeCount = skinnedMesh.blendShapeCount; 

	}
	

	void FixedUpdate()
	{  // float translation = Time.deltaTime * 10;
		

			// get the face-tracking manager instance
			if (manager == null) {
				manager = FacetrackingManager.Instance;
			}

			if (manager && manager.IsTrackingFace ()) {
				// set head position & rotation
				if(HeadTransform != null)
			{
				// head position
				Vector3 newPosition = HeadInitialPosition + manager.GetHeadPosition(mirroredHeadMovement);

				if(smoothFactor != 0f)
					HeadTransform.localPosition = Vector3.Lerp(HeadTransform.localPosition, newPosition, smoothFactor * Time.deltaTime);
				else
					HeadTransform.localPosition = newPosition;

				// head rotation
				Quaternion newRotation = HeadInitialRotation * manager.GetHeadRotation(mirroredHeadMovement);

				if(smoothFactor != 0f)
					HeadTransform.localRotation = Quaternion.Slerp(HeadTransform.localRotation, newRotation, smoothFactor * Time.deltaTime);
				else
					HeadTransform.localRotation = newRotation;
			}
			if(LeftEyeTransform != null)
			{
				// head position
				Vector3 newPosition = LeftEyeInitialPosition + manager.GetHeadPosition(mirroredHeadMovement);

				if(smoothFactor != 0f)
					LeftEyeTransform.localPosition = Vector3.Lerp(HeadTransform.localPosition, newPosition, smoothFactor * Time.deltaTime);
				else
					LeftEyeTransform.localPosition = newPosition;

				// head rotation
				Quaternion newRotation = LeftEyeInitialRotation * manager.GetHeadRotation(mirroredHeadMovement);

				if(smoothFactor != 0f)
					LeftEyeTransform.localRotation = Quaternion.Slerp(LeftEyeTransform.localRotation, newRotation, smoothFactor * Time.deltaTime);
				else
					LeftEyeTransform.localRotation = newRotation;
			}
			if(RightEyeTransform != null)
			{
				// head position
				Vector3 newPosition = RightEyeInitialPosition + manager.GetHeadPosition(mirroredHeadMovement);

				if(smoothFactor != 0f)
					RightEyeTransform.localPosition = Vector3.Lerp(HeadTransform.localPosition, newPosition, smoothFactor * Time.deltaTime);
				else
					RightEyeTransform.localPosition = newPosition;

				// head rotation
				Quaternion newRotation = RightEyeInitialRotation * manager.GetHeadRotation(mirroredHeadMovement);

				if(smoothFactor != 0f)
					RightEyeTransform.localRotation = Quaternion.Slerp(RightEyeTransform.localRotation, newRotation, smoothFactor * Time.deltaTime);
				else
					RightEyeTransform.localRotation = newRotation;
			}// apply animation units

				// AU0 - Upper Lip Raiser
				// 0=neutral, covering teeth; 1=showing teeth fully; -1=maximal possible pushed down lip
				float fAU0 = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LipPucker);
			if (fAU0 > Value_Lippucker && isSadMouth == false && isSmillingMouth == false && isOpenMouth==false) {
					setBlendShape (100, 4);
					isKissingMouth = true;
				} else {
					setBlendShape (0, 4);
					isKissingMouth = false;
				}
		
			//	Debug.Log (fAU0 .ToString ());
				//	setBlendShape1 (fAU0, 5);
				// AU1 - Jaw Lowerer
				// 0=closed; 1=fully open; -1= closed, like 0
				float fAU1 = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.JawOpen);
			if (fAU1 >Value_JawOpen && isSadMouth == false && isSmillingMouth == false && isKissingMouth==false) {
				setBlendShape (100, 8);
				isOpenMouth = true;
			} else {
				setBlendShape (0, 8);
				isOpenMouth  = false;
			}
	//	Debug.Log (fAU1 .ToString ());
				// AU2 – Lip Stretcher
				// 0=neutral; 1=fully stretched (joker’s smile); -1=fully rounded (kissing mouth)
				float fAU2_left = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LipStretcherLeft);
				//fAU2_left = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU2_left * 2 - 1) : fAU2_left;
	
				float fAU2_right = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LipStretcherRight);
			//	fAU2_right = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU2_right * 2 - 1) : fAU2_right;
		

		
			if (fAU2_left > Value_Lipstretcher && isSadMouth == false && isKissingMouth == false && isOpenMouth==false) {
					setBlendShape (100, 1);
					isSmillingMouth = true;

				} else {
					//setBlendShape (0, 4);
					setBlendShape (0, 1);
					isSmillingMouth = false;
				}


		//	Debug.Log (fAU2_left .ToString ());
				// AU3 – Brow Lowerer
				// 0=neutral; -1=raised almost all the way; +1=fully lowered (to the limit of the eyes)
				float fAU3_left = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LefteyebrowLowerer);
				//fAU3_left = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU3_left * 2 - 1) : fAU3_left;


				float fAU3_right = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.RighteyebrowLowerer);
				//fAU3_right = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU3_right * 2 - 1) : fAU3_right;
		
	
				if (fAU3_left < -0.1) {
		
					setBlendShape (100f, 3);
			
				} else if (fAU3_left > 0.1) {
					setBlendShape (100f, 7);
			
				} else {
					setBlendShape (0, 7);
					setBlendShape (0, 3);
				}
			
				
		

				// AU4 – Lip Corner Depressor
				// 0=neutral; -1=very happy smile; +1=very sad frown
				float fAU4_left = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LipCornerDepressorLeft);
				//	fAU4_left = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU4_left * 2-1) : fAU4_left;

				float fAU4_right = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LipCornerDepressorRight);
			//	Debug.Log (fAU4_left.ToString ());
			 
			if (fAU1 <0.6 && fAU4_left >Value_LipCornerDepressor&& isSmillingMouth == false && isKissingMouth == false) {

					setBlendShape (100f, 2);
					isSadMouth = true;

				} else {
					setBlendShape (0f, 2);
					isSadMouth = false;
				}

			Debug.Log (fAU4_left .ToString ());
				// AU6, AU7 – Eyelid closed
				// 0=neutral; -1=raised; +1=fully lowered
				float fAU6_left = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.LefteyeClosed);
				fAU6_left = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU6_left * 2 - 1) : fAU6_left;
		
			 
				float fAU6_right = manager.GetAnimUnit (KinectInterop.FaceShapeAnimations.RighteyeClosed);
				fAU6_right = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU6_right * 2 - 1) : fAU6_right;
		
				/*if (blendShapeCount > 2) {

				if (blendOne<<100f &&fAU2_left>0f) {
					blendOne=1000*fAU2_left;
					skinnedMeshRenderer.SetBlendShapeWeight (1, blendOne);
					//blendOne += blendSpeed;
				} else {
					//blendOneFinished = true;
				}

				if (  blendTwo < 100f) {
					skinnedMeshRenderer.SetBlendShapeWeight (2, blendTwo);
					blendTwo += blendSpeed;
				}

			}*/
			}
		
	}
	
	void setBlendShape(float weight,int index){
	

		skinnedMeshRenderer.SetBlendShapeWeight (index,weight);
	}



}