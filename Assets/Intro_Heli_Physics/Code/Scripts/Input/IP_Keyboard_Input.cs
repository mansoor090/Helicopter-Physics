using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helicopter
{


	public class IP_Keyboard_Input : IP_BaseInput
	{

		// Start is called before the first frame update

		public KeyCode cameraKey = KeyCode.C;
		

		#region Properties

		protected float throttleInput;
		public  float RawThrottleInput => throttleInput;

		protected float stickyThrottle;
		public    float StickyThrottle => stickyThrottle;

		protected Vector2 cyclicInput;
		public    Vector2 CyclicInput => cyclicInput;

		protected float pedalInput;
		public    float PedalInput => pedalInput;

		protected float collectiveInput;
		public    float CollectiveInput => collectiveInput;

		protected float stickyCollectiveInput;
		public    float StickyCollectiveInput => stickyCollectiveInput;

		protected bool cameraButton;
		public    bool CameraButton => cameraButton;
	


		#endregion


		protected override void HandleInput()
		{
			base.HandleInput();
			HandleThrottle();
			HandlePedal();
			HandleCycle();
			HandleCollective();
			HandleCameraKey();
			// clamping above inputs;
			ClampInputs();
			
			// handling sticky inputs
			HandleStickyThrottle();
			HandleStickyCollective();
		}


		protected virtual void HandleThrottle()
		{
			throttleInput = Input.GetAxis("Throttle");
		}

		protected virtual void HandlePedal()
		{
			pedalInput = Input.GetAxis("Pedal");
		}

		protected virtual void HandleCycle()
		{
			cyclicInput.y = vertical;
			cyclicInput.x = horizontal;
		}

		protected virtual void HandleCollective()
		{
			collectiveInput = Input.GetAxis("Collective");
		}

		protected virtual void ClampInputs()
		{
			throttleInput   = Mathf.Clamp(throttleInput,   -1f, 1f);
			pedalInput      = Mathf.Clamp(pedalInput,      -1f, 1f);
			collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
			cyclicInput     = Vector2.ClampMagnitude(cyclicInput, 1f);
		}

		protected virtual void HandleStickyThrottle()
		{
			stickyThrottle += RawThrottleInput * Time.deltaTime;
			stickyThrottle =  Mathf.Clamp01(stickyThrottle);
		
		}
		
		protected virtual void HandleStickyCollective()
		{
			stickyCollectiveInput += collectiveInput * Time.deltaTime;
			stickyCollectiveInput =  Mathf.Clamp01(stickyCollectiveInput);
			// stickyCollectiveInput =  Mathf.Clamp(stickyCollectiveInput, -1f, 1f);
			
		}
		
		protected virtual void HandleCameraKey()
		{
			cameraButton = Input.GetKeyDown(cameraKey);
		}

	}


}