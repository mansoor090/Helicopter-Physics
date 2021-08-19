using System.Collections;
using System.Collections.Generic;
using Helicopter;
using UnityEngine;

public class IP_Heli_TailRotar : MonoBehaviour, IP_Heli_IRotar
{

	#region Properties

	[Header("Rotars Properties")] public     Transform LRotar;
	public                                   Transform RRotar;
	public                                   float     maxPitch;
	
	[Header("Tail Rotar Properties")] public float     rotSpeedModifer = 1.5f;
	
	#endregion 


	#region Init Methods

	void Start()
	{
		
	}

	#endregion 


	#region Custom Methods
	
	public void UpdateRotar(float dps, IP_Input_Controller input, float maxDps)
	{
		transform.Rotate(Vector3.right * (dps * rotSpeedModifer));
		
		if (LRotar && RRotar)
		{
			LRotar.localRotation = Quaternion.Euler(0, input.PedalInput  * maxPitch, 0);
			RRotar.localRotation = Quaternion.Euler(0, -input.PedalInput * maxPitch, 0);
		}
		
	}
	

	#endregion

}
