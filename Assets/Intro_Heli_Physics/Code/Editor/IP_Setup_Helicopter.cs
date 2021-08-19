using System.Collections;
using System.Collections.Generic;
using Helicopter;
using UnityEditor;
using UnityEngine;

public class IP_Setup_Helicopter : MonoBehaviour
{

	[MenuItem("MAK Game World/Vehicles/Setup New Helicopter")]
	public static void BuildNewHelicopter()
	{

		GameObject currHeli = new GameObject("New_Helicopter", typeof(IP_Input_Controller), typeof(IP_Heli_Controller) );

		GameObject curCOG = new GameObject("COG");
		curCOG.transform.parent = currHeli.transform;

		IP_Heli_Controller curController = currHeli.GetComponent<IP_Heli_Controller>();
		curController.cog = curCOG.transform;
		
		IP_Input_Controller curInput = currHeli.GetComponent<IP_Input_Controller>();
		curController.input      = curInput;

		SetupRigidbody(curController);

		GameObject audioGRP = new GameObject("Audio_GRP");
		GameObject graphicsGRP = new GameObject("Graphics_Grp");
		GameObject colGRP = new GameObject("Collision_GRP");
		GameObject engineGRP = new GameObject("Engine_GRP");
		GameObject rotarGRP = new GameObject("Rotar_GRP");

		SetupEngineGrp(engineGRP,curController);
		SetupRotarGrp(rotarGRP,curController);
		SetupCharacteristics(currHeli,curController);
		
		audioGRP.transform.parent    = currHeli.transform;
		graphicsGRP.transform.parent = currHeli.transform;
		colGRP.transform.parent      = currHeli.transform;
		engineGRP.transform.parent   = currHeli.transform;
		rotarGRP.transform.parent    = currHeli.transform;

		Selection.activeGameObject = currHeli;

	}


	static void SetupRigidbody(IP_Heli_Controller curController)
	{
		curController.weightInLbs = 1200;

		Rigidbody rigidbody = curController.GetComponent<Rigidbody>();
		rigidbody.drag                   = 0.5f;
		rigidbody.angularDrag            = 1f;
		rigidbody.useGravity             = true;
		rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		
	}

	static void SetupRotarGrp(GameObject rotargo, IP_Heli_Controller curController)
	{
		IP_Heli_Rotar_Controller curRotar = rotargo.AddComponent<IP_Heli_Rotar_Controller>();
		curController.rotar = curRotar;
		
		GameObject mainGRP = new GameObject("Main_Rotar");
		mainGRP.AddComponent<IP_Heli_MainRotar>();
		GameObject tailGRP = new GameObject("Tail_Rotar");
		tailGRP.AddComponent<IP_Heli_TailRotar>();
		
		mainGRP.transform.parent = rotargo.transform;
		tailGRP.transform.parent = rotargo.transform;

	}
	
	static void SetupEngineGrp(GameObject engineGO, IP_Heli_Controller curController)
	{

		
		GameObject     engineGRP  = new GameObject("Main_Engine");
		
		IP_Heli_Engine heliEngine = engineGRP.AddComponent<IP_Heli_Engine>();
		curController.engines.Add(heliEngine);
		
		engineGRP.transform.parent = engineGO.transform;

	}
	
	static void SetupCharacteristics(GameObject heliGO, IP_Heli_Controller curController)
	{
		IP_Heli_Characteristics curCharacteristics = heliGO.AddComponent<IP_Heli_Characteristics>();
		curController.characteristics = curCharacteristics;
	}

}
