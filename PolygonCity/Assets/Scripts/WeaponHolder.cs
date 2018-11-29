using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponHolder : NetworkBehaviour
{
	public Arma arma;

	public GameObject armaHolder;

	void Start ()
	{
		//CmdMudarRender ();
		//Debug.Log (armaHolder.GetComponent<MeshRenderer> ().sharedMaterial.ToString ());
	}

	void Update ()
	{
		//Debug.Log (armaHolder.GetComponent<MeshRenderer> ().sharedMaterial.ToString ());
	}

	[Command]
	void CmdMudarRender ()
	{
		NetworkServer.Spawn (gameObject);
		NetworkServer.Spawn (armaHolder);
		armaHolder.GetComponent<MeshFilter> ().sharedMesh = arma.prefab.GetComponent<MeshFilter> ().sharedMesh;
		armaHolder.GetComponent<MeshRenderer> ().sharedMaterial = arma.prefab.GetComponent<MeshRenderer> ().sharedMaterial;
		RpcMudarRender ();
	}

	[ClientRpc]
	void RpcMudarRender ()
	{
		armaHolder.GetComponent<MeshFilter> ().sharedMesh = arma.prefab.GetComponent<MeshFilter> ().sharedMesh;
		armaHolder.GetComponent<MeshRenderer> ().sharedMaterial = arma.prefab.GetComponent<MeshRenderer> ().sharedMaterial;
		Debug.Log (armaHolder.GetComponent<MeshRenderer> ().sharedMaterial.ToString ());
	}
}
