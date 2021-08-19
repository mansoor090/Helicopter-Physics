using System.Collections.Generic;
using UnityEngine;


namespace Helicopter
{


	public class IP_Heli_Controller : IP_Base_RBController
	{

		#region Variables

		[Header("Input"), Space] public IP_Input_Controller input;


		[Header("Engine Properties")] public List<IP_Heli_Engine> engines = new List<IP_Heli_Engine>();

		[Header("Rotars Properties")]          public IP_Heli_Rotar_Controller rotar;
		[Header("Characteristics Properties")] public IP_Heli_Characteristics  characteristics;

		#endregion


		#region Properties

		#endregion


		#region Init Methods

		public override void Start()
		{
			base.Start();
			
			if (!input)
			{
				input = GetComponent<IP_Input_Controller>();
			}

			if (!characteristics)
			{
				characteristics = GetComponent<IP_Heli_Characteristics>();
			}
		}

		#endregion


		#region Update Methods

		void Update()
		{
		}

		#endregion


		#region Custom Methods

		protected override void HandlePhysics()
		{
			if (input)
			{
				HandleEngines();
				HandleRotars();
				HandleCharacteristics();
			}
		}

		protected virtual void HandleEngines()
		{
			for (int i = 0; i < engines.Count; i++)
			{
				engines[i].UpdateEngine(input.StickyThrottle);
				float finalHP  = engines[i].CurrentHp;
				float finalRPM = engines[i].CurrentRpm;
			}
		}

		protected virtual void HandleRotars()
		{
			if (engines.Count > 0 && rotar)
			{
				rotar.UpdateRotorController(input, engines[0].CurrentRpm, engines[0].maxRPM);
			}
		}

		protected virtual void HandleCharacteristics()
		{
			characteristics.HandleCharacteristics(rb, input);
		}
		
		#endregion

	}


}