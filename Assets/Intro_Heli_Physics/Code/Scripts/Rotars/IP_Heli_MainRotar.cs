using System.Collections;
using System.Collections.Generic;
using Helicopter;
using UnityEngine;

public class IP_Heli_MainRotar : MonoBehaviour , IP_Heli_IRotar
{


	#region Properties

	[Header("Rotars Properties")] public Transform LRotar;
	public                               Transform RRotar;
	public                               float     maxPitch;

	[Header("Rotars Blend Properties")] public List<SkinnedMeshRenderer> bladesSkinMesh;
	#endregion


	// private float currentRPM;
	// public  float CurrentRPM => currentRPM;


	#region Custom Methods

	public void UpdateRotar(float dps, IP_Input_Controller input, float maxDps)
	{
	
		transform.Rotate(Vector3.up                 * dps);

		// currentRPM = (dps * 60) / ( Time.deltaTime * 360);

		if (LRotar && RRotar)
		{
			LRotar.localRotation = Quaternion.Euler(input.StickyCollectiveInput  * maxPitch, 0, 0);
			RRotar.localRotation = Quaternion.Euler(-input.StickyCollectiveInput * maxPitch, 0, 0);
		}
		
		// handle blendshape of blades
		float normalizeDps = Mathf.InverseLerp(0f, 95f, dps);
		float blendValue   = Mathf.Lerp(0, 50f, normalizeDps);

		for (int i = 0; i < bladesSkinMesh.Count; i++)
		{
			bladesSkinMesh[i].SetBlendShapeWeight(0, blendValue);
		}

		
	}
	

	#endregion


	

}
