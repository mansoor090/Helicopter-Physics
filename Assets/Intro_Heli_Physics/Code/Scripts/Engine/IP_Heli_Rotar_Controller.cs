using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Helicopter
{


	public class IP_Heli_Rotar_Controller : MonoBehaviour
	{

		#region Variables

		[SerializeField] public List<IP_Heli_IRotar> rotars;
		[SerializeField]        float                maxDps;
		[SerializeField]        float                currentDps;

		#endregion


		#region Init Methods

		void Start()
		{
			rotars = GetComponentsInChildren<IP_Heli_IRotar>().ToList();
		}

		#endregion


		#region Custom Methods

		public void UpdateRotorController(IP_Input_Controller input, float currentRPM, float maxRPM)
		{
			float secondsInMinute = 60f;
			float completeCircle  = 360f;
			if (maxDps == 0)
			{
				maxDps = ((completeCircle * maxRPM) / secondsInMinute) * Time.deltaTime;
			}
			currentDps = ((completeCircle * currentRPM) / secondsInMinute) * Time.deltaTime;
			
			foreach (var rotar in rotars)
			{
				rotar.UpdateRotar(currentDps, input, maxDps);
			}
		}

		#endregion

	}


}