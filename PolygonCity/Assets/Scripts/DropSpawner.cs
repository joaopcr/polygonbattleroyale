using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DropSpawner : NetworkBehaviour
{

	public GameObject dropPrefabAK;
	public GameObject dropPrefabPistola;
	public GameObject dropPrefabM4;

	public override void OnStartServer ()
	{

		GameObject drop = Object.Instantiate (dropPrefabAK, transform.position, Quaternion.identity);
		NetworkServer.Spawn (drop);

		GameObject drop2 = Instantiate (dropPrefabAK, transform.position, Quaternion.identity);
		NetworkServer.Spawn (drop2);

		GameObject drop3 = Instantiate (dropPrefabM4, transform.position, Quaternion.identity);
		NetworkServer.Spawn (drop3);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public static void Spawn (GameObject go, Vector3 pos, Quaternion rot)
	{
		GameObject g = Instantiate (go, pos, rot);
		NetworkServer.Spawn (g);
	}
}
