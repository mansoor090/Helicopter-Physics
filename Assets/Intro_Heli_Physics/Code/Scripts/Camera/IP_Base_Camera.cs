using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IP_Base_Camera : MonoBehaviour
{

	#region Variables

	[Header("Base Properties")] public Rigidbody rb;
	public                             Transform lookAtTarget;

	[HideInInspector]public Vector3 wantedPos;
	[HideInInspector]public Vector3 refVelocity;
	[HideInInspector]public Vector3 targetFlatForward;

	[HideInInspector] public UnityEvent cameraEvent;

	#endregion


	void FixedUpdate()
	{
		targetFlatForward   = rb.transform.forward;
		targetFlatForward.y = 0;
		targetFlatForward   = targetFlatForward.normalized;
		
		cameraEvent.Invoke();
	}

	void OnDisable()
	{
		cameraEvent.RemoveAllListeners();
	}

}