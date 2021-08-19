using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helicopter
{


	public class IP_Heli_Engine : MonoBehaviour
	{

		#region Variables

		public float          maxHP      = 140f;
		public float          maxRPM     = 2700f;
		public float          powerDelay = 2f; // power smoother to reach max number
		public AnimationCurve powerCurve = new AnimationCurve();

		#endregion


		#region Properties

		[SerializeField] float currentHP;
		public           float CurrentHp => currentHP;

		[SerializeField]float        currentRPM;
		public float CurrentRpm => currentRPM;

		#endregion

		

		#region Custom Methods

		public void UpdateEngine(float throttleInput)
		{
			// calculate horse power
			float wantedHP = powerCurve.Evaluate(throttleInput) * maxHP;
			currentHP = Mathf.Lerp(currentHP, wantedHP, Time.deltaTime * powerDelay);

			// calculate RPM
			float wantedRPM = throttleInput * maxRPM;
			currentRPM = Mathf.Lerp(currentRPM, wantedRPM, Time.deltaTime * powerDelay);
		
		}

		#endregion

	}


}