using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IP_Camera_Basic : IP_Base_Camera, IP_IHeliCamera
{

	#region Variables

	[Header("Basic Properties")]
	public float height = 2f;
	public float distance = 2f;

	public float smoothSpeed = 0.35f;

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
		

		//wanted pos
		wantedPos = rb.position + (targetFlatForward * distance) + (Vector3.up * height);

		// lets position the camera
		transform.position = Vector3.SmoothDamp(transform.position, wantedPos, ref refVelocity, smoothSpeed);
		transform.LookAt(lookAtTarget);
	}

	#endregion

}