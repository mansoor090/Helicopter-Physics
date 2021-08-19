using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helicopter
{


	public class IP_BaseInput : MonoBehaviour
	{

		#region Variables

		protected float vertical;
		protected float horizontal;

		#endregion


		#region InitialMethods

		void Start()
		{
		}

		#endregion


		void Update()
		{
			HandleInput();
		}


		protected virtual void HandleInput()
		{
			vertical   = Input.GetAxis("Vertical");
			horizontal = Input.GetAxis("Horizontal");
		}

	}


}