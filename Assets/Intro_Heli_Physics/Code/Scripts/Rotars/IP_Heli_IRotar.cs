using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helicopter
{


	public interface IP_Heli_IRotar
	{

		#region Methods

		void UpdateRotar(float dps, IP_Input_Controller input, float maxDps);

		#endregion

	}


}