using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Custom.Rendering;
using UnityEngine;

public class IP_Camera_Manager : MonoBehaviour
{

	#region Variables
	public int                 startIndex;
	 List<IP_Base_Camera> Cameras;
	int                  currentIndex;
	
	#endregion 



	#region Init Methods

	void Start()
	{
		Cameras = GetComponentsInChildren<IP_Base_Camera>().ToList();
		SwitchCamera(startIndex);
	}

	#endregion 




	#region Custom Methods

	public void SwitchCamera()
	{
		currentIndex++;
		HandleSwitch();
	}

	public void SwitchCamera(int index)
	{
		currentIndex = index;
		HandleSwitch();
	}

	void HandleSwitch()
	{
		if (currentIndex == Cameras.Count)
		{
			currentIndex = 0;
		}

		Camera        currCam = null;
		AudioListener currListener = null;
		for (int i = 0; i < Cameras.Count; i++)
		{
			        currCam = Cameras[i].GetComponent<Camera>();
			 currListener = Cameras[i].GetComponent<AudioListener>();
			currCam.enabled      = false;
			currListener.enabled = false;
			
			if (i == currentIndex)
			{
				currCam.enabled      = true;
				currListener.enabled = true;
			}
			
		}
		
		
	}

	#endregion 

}
