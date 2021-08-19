using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helicopter
{


	public class IP_Xbox_Input : IP_Keyboard_Input
	{

		protected override void HandleThrottle()
		{
			throttleInput = Input.GetAxis("XBoxThottleUp") + -Input.GetAxis("XBoxThottleDown");
		}

		protected override void HandleCycle()
		{
			cyclicInput.y = Input.GetAxis("XBoxCyclicVertical");
			cyclicInput.x = Input.GetAxis("XBoxCyclicHorizontal");
		}

		protected override void HandleCollective()
		{
			collectiveInput = Input.GetAxis("XBoxCollective");
		}

		protected override void HandlePedal()
		{
			pedalInput = Input.GetAxis("XBoxPedal");
		}

		protected override void HandleCameraKey()
		{
			cameraButton = Input.GetButtonDown("XBoxCamBtn");
		}

		

	}


}