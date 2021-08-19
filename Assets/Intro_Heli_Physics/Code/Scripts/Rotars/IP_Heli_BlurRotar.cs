using System;
using System.Collections;
using System.Collections.Generic;
using Helicopter;
using UnityEngine;

public class IP_Heli_BlurRotar : MonoBehaviour, IP_Heli_IRotar
{

	#region Variables
	
	public List<GameObject> blades;
	public GameObject       blurGeo;
	
	public List<Texture2D> blurTextures;
	public Material        blurMat;
	#endregion 
	

	#region Custom Methods

	void Start()
	{
		blurMat = blurGeo.GetComponent<MeshRenderer>().sharedMaterial;
	}

	public void UpdateRotar(float dps, IP_Input_Controller input, float maxDps)
	{
		float normalizedDps = Mathf.InverseLerp(0f, maxDps, dps);
		int   blurTexID     = Mathf.FloorToInt(normalizedDps * blurTextures.Count);
		blurTexID           = Mathf.Clamp(blurTexID, -1, blurTextures.Count - 1);

		if (blurMat && blurTextures[blurTexID])
		{
			blurMat.mainTexture = blurTextures[blurTexID];
		}

		// handle geoblur blades visual
		if (blurTexID > 1)
		{
			HandleBlurGeoVisual(true);
		}
		else
		{
			HandleBlurGeoVisual(false);
		}
		
		// handle actual blades visual
		if (blurTexID > 2)
		{
			HandleBladesVisual(false);
		}
		else
		{
			HandleBladesVisual(true);
		}			
		
		
		// Debug.Log(blurTexID);
	}

	void HandleBladesVisual(bool value)
	{
		if (blades.Count == 0)
		{
			Debug.LogWarningFormat("No Blades Are Assigned");
		}
		
		for (int i = 0; i < blades.Count; i++)
		{
			blades[i].SetActive(value);
		}
		
		
	}
	void HandleBlurGeoVisual(bool value)
	{
		blurGeo.SetActive(value);
	}

	#endregion

	
	

}
