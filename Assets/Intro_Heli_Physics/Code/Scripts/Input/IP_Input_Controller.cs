using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Helicopter
{


	[RequireComponent(typeof(IP_Keyboard_Input)), RequireComponent(typeof(IP_Xbox_Input))]
	public class IP_Input_Controller : MonoBehaviour
	{

		public enum InputType
		{

			Keyboard,
			Mobile,
			Xbox

		}

		[SerializeField] InputType         inputType = InputType.Keyboard;
		[SerializeField] IP_Keyboard_Input IPKeyboardInput;
		[SerializeField] IP_Xbox_Input     IPXBoxInput;
		[SerializeField] UnityEvent     events;

		float        thottleInput;
		public float ThottleInput => thottleInput;

		float        stickyThrottle;
		public float StickyThrottle => stickyThrottle;

		Vector2        cyclicInput;
		public Vector2 CyclicInput => cyclicInput;

		float        pedalInput;
		public float PedalInput => pedalInput;

		float        collectiveInput;
		public float CollectiveInput => collectiveInput;
		
		float    stickyCollectiveInput;
		public float StickyCollectiveInput => stickyCollectiveInput;

		protected bool cameraButtonInput;
		public    bool CameraButtonInput => cameraButtonInput;

		void Start()
		{
			SetInputType(inputType);
		}

		void Update()
		{
			switch (inputType)
			{
				case InputType.Keyboard:
					thottleInput          = IPKeyboardInput.RawThrottleInput;
					stickyThrottle        = IPKeyboardInput.StickyThrottle;
					cyclicInput           = IPKeyboardInput.CyclicInput;
					pedalInput            = IPKeyboardInput.PedalInput;
					collectiveInput       = IPKeyboardInput.CollectiveInput;
					stickyCollectiveInput = IPKeyboardInput.StickyCollectiveInput;
					cameraButtonInput     = IPKeyboardInput.CameraButton;
					break;

				case InputType.Mobile:
					break;

				case InputType.Xbox:
					thottleInput          = IPXBoxInput.RawThrottleInput;
					stickyThrottle        = IPXBoxInput.StickyThrottle;
					cyclicInput           = IPXBoxInput.CyclicInput;
					pedalInput            = IPXBoxInput.PedalInput;
					collectiveInput       = IPXBoxInput.CollectiveInput;
					stickyCollectiveInput = IPXBoxInput.StickyCollectiveInput;
					cameraButtonInput     = IPXBoxInput.CameraButton;
					break;
			}

			if (cameraButtonInput)
			{
				events.Invoke();
			}
		}

		void SetInputType(InputType type)
		{
			
			IPKeyboardInput = GetComponent<IP_Keyboard_Input>();
			IPXBoxInput     = GetComponent<IP_Xbox_Input>();
			
			
			IPKeyboardInput.enabled  = false;
			IPXBoxInput.enabled      = false;
			switch (inputType)
			{
				case InputType.Keyboard:
					IPKeyboardInput.enabled = true;
					break;

				case InputType.Mobile:
					break;

				case InputType.Xbox:
					IPXBoxInput.enabled = true;
					break;
			}
		}

	}


}