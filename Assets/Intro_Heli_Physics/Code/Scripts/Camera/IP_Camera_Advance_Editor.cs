using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(IP_Camera_Advance))]
public class IP_Camera_Advance_Editor : Editor
{

	#region Variables

	IP_Camera_Advance targetCamera;
	
	#endregion


	#region Methods


	void OnSceneGUI()
	{
		if (!targetCamera)
		{
			targetCamera = (IP_Camera_Advance) target;
		}
		
		Handles.color = Color.blue;

		float   minDist      = targetCamera.minDistance;
		float   maxDist      = targetCamera.maxDistance;
		
		Handles.DrawWireDisc(targetCamera.rb.position,Vector3.up,targetCamera.minDistance);
		Handles.DrawWireDisc(targetCamera.rb.position,Vector3.up,targetCamera.maxDistance);
	
		targetCamera.minDistance = Handles.ScaleSlider(minDist, targetCamera.rb.position + (Vector3.back * minDist), Vector3.back, Quaternion.identity, 2f, 1f);
		targetCamera.maxDistance = Handles.ScaleSlider(maxDist, targetCamera.rb.position + (Vector3.back  * maxDist), Vector3.back, Quaternion.identity, 2f, 1f);
	
	
	}

	#endregion
	
}
