using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkDiscovery : NetworkDiscovery
{
	private NetworkManager networkManager;

	public void CriarSala ()
	{
		base.Initialize ();
		base.StartAsServer ();	
	}

	public void StartGame ()
	{
		networkManager.StartHost ();
	}

	void Start ()
	{
		networkManager = NetworkManager.singleton;
	}

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		//Debug.Log (fromAddress + " data: " + data);
		//Este método lista as salas para o cliente;
	}
}

