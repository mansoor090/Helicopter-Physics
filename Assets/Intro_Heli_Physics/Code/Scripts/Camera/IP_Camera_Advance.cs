using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class IP_Camera_Advance : IP_Base_Camera, IP_IHeliCamera
{

	#region Variables

	[Header("Advance Properties")] public float minDistance          = 4f;
	public                                float maxDistance          = 8f;
	public                                float height               = 2f;
	public                                float minGroundHeight      = 4f;
	public                                float catchUpModifer       = 5f;
	public                                float rotationSpeed        = 5f;
	public                                float minVelocityForOrient = 5f;

	private float   finalAngle;
	private Vector3 wantedDir;
	float           finalHeight;
	#endregion


	#region Init Methods

	void OnEnable()
	{
		cameraEvent.AddListener(UpdateCamera);
	}

	#endregion

	#region Custom Methods

	public void UpdateCamera()
	{
		// get the flat dirToTarget from helicopter to the camera
		Vector3 dirToTarget = transform.position - rb.position;
		dirToTarget.y = 0;
		Vector3 normalizedDir = dirToTarget.normalized;
		wantedDir = normalizedDir;
		Debug.DrawRay(rb.position, wantedDir, Color.green);

		// find the angle between our dir and the flat forward
		float angleToFwd  = Vector3.SignedAngle(normalizedDir, targetFlatForward, Vector3.up);
		float wantedAngle = 0f;
		if (rb.velocity.magnitude > minVelocityForOrient)
		{
			wantedAngle = angleToFwd * Time.deltaTime;
		}
		finalAngle = Mathf.Lerp(finalAngle, wantedAngle, Time.deltaTime * rotationSpeed);
		wantedDir  = Quaternion.AngleAxis(finalAngle, Vector3.up) * wantedDir;

		//repositioning the camera based off of the min and max distance
		wantedPos = rb.position + (wantedDir * dirToTarget.magnitude);
		float curMagnitude = dirToTarget.magnitude;
		if (curMagnitude < minDistance)
		{
			float delta = minDistance - curMagnitude;
			wantedPos += wantedDir * delta * Time.deltaTime * catchUpModifer;
		}

		if (curMagnitude > maxDistance)
		{
			float delta = curMagnitude - maxDistance;
			wantedPos -= wantedDir * delta * Time.deltaTime * catchUpModifer;
		}


		float      wantedheight = height;
		RaycastHit hit;
		Ray        groundRay = new Ray(transform.position, Vector3.down);
		if (Physics.Raycast(groundRay, out hit, 100f))
		{
			if (hit.collider.CompareTag("Ground") && hit.distance <= minGroundHeight)
			{
				wantedheight += minGroundHeight - hit.distance;
			}
			
		}

		finalHeight = Mathf.Lerp(finalHeight, wantedheight, Time.deltaTime);
		

		// Apply final Transformatinos 
		transform.position = wantedPos + (Vector3.up * finalHeight);
		transform.LookAt(lookAtTarget);
	}

	#endregion

}