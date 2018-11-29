using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	void Start ()
	{
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemoteLayer ();
		}

		GetComponent<Player> ().Setup ();
	}

	public override void OnStartClient ()
	{
		base.OnStartClient ();

		string _netID = GetComponent<NetworkIdentity> ().netId.ToString ();
		Player _player = GetComponent<Player> ();

		GameManager.RegisterPlayer (_netID, _player);
	}

	void AssignRemoteLayer ()
	{
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents ()
	{
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable [i].enabled = false;
		}
	}

	void OnDisable ()
	{	
		if (isLocalPlayer)
			GameManager.instance.SetSceneCameraActive (true);
		
		GameManager.UnRegisterPlayer (transform.name);
	}

}
