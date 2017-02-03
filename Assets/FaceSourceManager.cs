using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Microsoft.Kinect.Face;
/*parts of code urese online resource to complete*/

public class FaceSourceManager : MonoBehaviour
{
	private KinectSensor sensor = null;

	private BodyFrameSource bodySource = null;
	private BodyFrameReader bodyReader = null;

	private FaceFrameSource FaceFrameSource = null;
	private FaceFrameReader FaceFrameReader = null;

	private FaceAlignment currentFaceAlignment = null;
	private FaceModel currentFaceModel = null;

	private Body currentTrackedBody = null;
	private ulong currentTrackingId = 0;
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;

	private ulong CurrentTrackingId
	{
		get
		{
			return currentTrackingId;
		}

		set
		{
			currentTrackingId = value;
		}
	}



	private static double VectorLength(CameraSpacePoint point)
	{
		var result = Mathf.Pow(point.X, 2) + Mathf.Pow(point.Y, 2) + Mathf.Pow(point.Z, 2);

		result = Mathf.Sqrt(result);

		return result;
	}

	private static Body FindClosestBody(BodyFrame bodyFrame)
	{
		Body result = null;
		double closestBodyDistance = double.MaxValue;

		Body[] bodies = new Body[bodyFrame.BodyCount];
		bodyFrame.GetAndRefreshBodyData(bodies);

		foreach (var body in bodies)
		{
			if (body.IsTracked)
			{
				var currentLocation = body.Joints[JointType.SpineBase].Position;

				var currentDistance = VectorLength(currentLocation);

				if (result == null || currentDistance < closestBodyDistance)
				{
					result = body;
					closestBodyDistance = currentDistance;
				}
			}
		}

		return result;
	}

	private static Body FindBodyWithTrackingId(BodyFrame bodyFrame, ulong trackingId)
	{
		Body result = null;

		Body[] bodies = new Body[bodyFrame.BodyCount];
		bodyFrame.GetAndRefreshBodyData(bodies);

		foreach (var body in bodies)
		{
			if (body.IsTracked)
			{
				if (body.TrackingId == trackingId)
				{
					result = body;
					break;
				}
			}
		}

		return result;
	}void Awake ()
	{
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}

	void Start(){
		sensor = KinectSensor.GetDefault();
		bodySource = sensor.BodyFrameSource;
		bodyReader = bodySource.OpenReader();
		bodyReader.FrameArrived += BodyReader_FrameArrived;
		FaceFrameFeatures faceFrameFeatures =
			FaceFrameFeatures.BoundingBoxInColorSpace
			| FaceFrameFeatures.PointsInColorSpace
			| FaceFrameFeatures.BoundingBoxInInfraredSpace
			| FaceFrameFeatures.PointsInInfraredSpace
			| FaceFrameFeatures.RotationOrientation
			| FaceFrameFeatures.FaceEngagement
			| FaceFrameFeatures.Glasses
			| FaceFrameFeatures.Happy
			| FaceFrameFeatures.LeftEyeClosed
			| FaceFrameFeatures.RightEyeClosed
			| FaceFrameFeatures.LookingAway
			| FaceFrameFeatures.MouthMoved
			| FaceFrameFeatures.MouthOpen;
		FaceFrameSource = FaceFrameSource.Create (sensor, currentTrackingId,faceFrameFeatures );
			
		FaceFrameSource.TrackingIdLost += HdFaceSource_TrackingIdLost;

		FaceFrameReader = FaceFrameSource.OpenReader();
		FaceFrameReader.FrameArrived += HdFaceReader_FrameArrived;

		//CurrentFaceModel = FaceModel.Create();
		currentFaceAlignment = FaceAlignment.Create();

		sensor.Open();
	}

	private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
	{
		var frameReference = e.FrameReference;
		using (var frame = frameReference.AcquireFrame())
		{
			if (frame == null)
			{
				// We might miss the chance to acquire the frame, it will be null if it's missed
				return;
			}

			if (currentTrackedBody != null)
			{
				currentTrackedBody = FindBodyWithTrackingId(frame, CurrentTrackingId);

				if (currentTrackedBody != null)
				{
					return;
				}
			}

			Body selectedBody = FindClosestBody(frame);

			if (selectedBody == null)
			{
				return;
			}

			currentTrackedBody = selectedBody;
			CurrentTrackingId = selectedBody.TrackingId;

		FaceFrameSource.TrackingId = CurrentTrackingId;
		}
	}

	private void HdFaceSource_TrackingIdLost(object sender, TrackingIdLostEventArgs e)
	{
		var lostTrackingID = e.TrackingId;

		if (CurrentTrackingId == lostTrackingID)
		{
			CurrentTrackingId = 0;
			currentTrackedBody = null;

			FaceFrameSource.TrackingId = 0;
		}
	}

	private void HdFaceReader_FrameArrived(object sender,FaceFrameArrivedEventArgs e)
	{// Retrieve the face reference
		FaceFrameReference faceRef = e.FrameReference;

		if (faceRef == null) return;

		// Acquire the face frame
		using (FaceFrame faceFrame = faceRef.AcquireFrame())
		{
			if (faceFrame == null) return;

			// Retrieve the face frame result
			FaceFrameResult frameResult = faceFrame.FaceFrameResult;
		//	Debug.Log ("hui");

			// Display the values
			if (frameResult.FaceProperties [FaceProperty.LeftEyeClosed].ToString () == "Yes") {
				skinnedMeshRenderer.SetBlendShapeWeight (1, 100);
			} else {
				skinnedMeshRenderer.SetBlendShapeWeight (1,  0);}
			if (frameResult.FaceProperties [FaceProperty.RightEyeClosed].ToString () == "Yes") {
				skinnedMeshRenderer.SetBlendShapeWeight (2, 100);
			} else {
				skinnedMeshRenderer.SetBlendShapeWeight (2,  0);}
			if (frameResult.FaceProperties [FaceProperty.Happy].ToString () == "Yes") {
				skinnedMeshRenderer.SetBlendShapeWeight (0, 100);
			} else {
				skinnedMeshRenderer.SetBlendShapeWeight (0,  0);}
			if (frameResult.FaceProperties [FaceProperty.MouthOpen].ToString () == "Yes") {
				skinnedMeshRenderer.SetBlendShapeWeight (1, 100);
			} else {
				skinnedMeshRenderer.SetBlendShapeWeight (3,  0);}
		Debug.Log ("hui");
		//	EngagedResult.Text = frameResult.FaceProperties[FaceProperty.Engaged].ToString();
			//GlassesResult.Text = frameResult.FaceProperties[FaceProperty.WearingGlasses].ToString();
			/*LeftEyeResult.Text = frameResult.FaceProperties[FaceProperty.LeftEyeClosed].ToString();
			RightEyeResult.Text = frameResult.FaceProperties[FaceProperty.RightEyeClosed].ToString();
			MouthOpenResult.Text = frameResult.FaceProperties[FaceProperty.MouthOpen].ToString();*/
			//	Debug.Log(MouthMovedResult.Text = frameResult.FaceProperties[FaceProperty.MouthMoved].ToString());
		//	LookingAwayResult.Text = frameResult.FaceProperties[FaceProperty.LookingAway].ToString();*/
		}
	}
	}
