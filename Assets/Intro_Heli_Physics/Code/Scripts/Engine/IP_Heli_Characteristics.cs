using System;
using System.Collections;
using System.Collections.Generic;
using Helicopter;
using UnityEngine;

public class IP_Heli_Characteristics : MonoBehaviour
{

	#region Variables

	[Header("Lift Properties")] public float maxLiftForce = 5f;

	public IP_Heli_MainRotar mainRotar;
	public IP_Heli_Engine    engine;

	[Header("Tail Properties")]      public float tailForce      = 2f;
	[Header("Cyclic Properties")]    public float cylicForce     = 2f;
	[Header("AutoLevel Properties")] public float autoLevelForce = 3f;

	protected Vector3 flatFwd;
	protected float   forwardDot;
	protected Vector3 flatRight;
	protected float   rightDot;
	protected float    upDot;

	#endregion


	#region Custom Methods

	public void HandleCharacteristics(Rigidbody rb, IP_Input_Controller input)
	{
		HandleLift(rb, input);
		HandlePedal(rb, input);
		HandleCyclic(rb, input);
		CalculateAngles();
		AutoLevel(rb);
	}


	protected virtual void HandleLift(Rigidbody rb, IP_Input_Controller input)
	{
		// Vector3 liftForce     = transform.up         * (Physics.gravity.magnitude + maxLiftForce) * rb.mass;
		float normalizedRPM     = engine.CurrentRpm / engine.maxRPM;
		float normalizedLiftRPM = normalizedRPM + normalizedRPM;
		normalizedLiftRPM = Mathf.Clamp01(normalizedLiftRPM);
		
		Vector3 defaultLiftForce = Vector3.up * ((Physics.gravity.magnitude) * rb.mass      * normalizedLiftRPM);
		Vector3 addedLiftForce   = transform.up * (input.CollectiveInput       * maxLiftForce * normalizedRPM);
		
		rb.AddForce((addedLiftForce + defaultLiftForce), ForceMode.Force);
	}

	protected virtual void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
	{
		float cyclicZforce = input.CyclicInput.x * cylicForce;
		rb.AddRelativeTorque(Vector3.forward      * cyclicZforce, ForceMode.Acceleration);
		float cyclicXforce = -input.CyclicInput.y  * cylicForce;
		rb.AddRelativeTorque(Vector3.right        * cyclicXforce, ForceMode.Acceleration);
	}


	protected virtual void HandlePedal(Rigidbody rb, IP_Input_Controller input)
	{
		rb.AddTorque(Vector3.up * (input.PedalInput * tailForce), ForceMode.Acceleration);
	}

	protected virtual void CalculateAngles()
	{
		//Calculate FlatForward
		flatFwd   = transform.forward;
		flatFwd.y = 0;
		flatFwd   = flatFwd.normalized;
		Debug.DrawRay(transform.position, flatFwd, Color.blue);

		//Calculate FlatRight
		flatRight   = transform.right;
		flatRight.y = 0;
		flatRight   = flatRight.normalized;
		Debug.DrawRay(transform.position, flatRight, Color.red);
		
		//Calculate Angles
		forwardDot = Vector3.Dot(transform.up, flatFwd);
		rightDot   = Vector3.Dot(transform.up, flatRight);
		upDot   = Vector3.Dot(transform.up, Vector3.up);
		// Debug.Log(string.Format("Fwd: {0} - Right: {1}", forwardDot.ToString("0.0"), rightDot.ToString("0.0")));

	}
	protected virtual void AutoLevel(Rigidbody rb)
	{
		if (upDot > 0f)
		{
			float rightForce = -forwardDot     * autoLevelForce;
			rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
			float forwardForce = rightDot        * autoLevelForce;
			rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
		}
	
	}

	#endregion

}