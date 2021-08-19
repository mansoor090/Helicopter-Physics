using System.Collections;
using System.Collections.Generic;
using Helicopter;
using UnityEngine;

public class IP_Arcade_Heli_Characteristics : IP_Heli_Characteristics
{

	[Header("AutoLevel Properties")]
	public float blankAngle = 35f;
	public float blankSpeed = 1.5f;

	private float xRot = 0;
	private float yRot = 0;
	private float zRot = 0;

	protected Quaternion wantedAngle = Quaternion.identity;
	
	
	protected override void HandleLift(Rigidbody rb, IP_Input_Controller input)
	{
		base.HandleLift(rb, input);
	}
	
	protected override void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
	{
	//	base.HandleCyclic(rb, input);

	Vector3 fwdDir   = -input.CyclicInput.y * flatFwd;
	Vector3 rightDir = -input.CyclicInput.x  * flatRight;
	Vector3 finalDir = (rightDir + fwdDir).normalized;
	
	rb.AddForce(finalDir * cylicForce, ForceMode.Acceleration);


	xRot = -input.CyclicInput.y * blankAngle;
	zRot = input.CyclicInput.x * blankAngle;


	}


	protected override void HandlePedal(Rigidbody rb, IP_Input_Controller input)
	{
		// base.HandlePedal(rb, input);

		yRot += input.PedalInput * tailForce;

	}

	protected override void AutoLevel(Rigidbody rb)
	{
		wantedAngle        = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRot, yRot, zRot), Time.deltaTime * blankSpeed);
		rb.MoveRotation(wantedAngle);
	}

}
