using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetwork : NetworkBehaviour
{

	public void MudarScene (string name)
	{
		NetworkManager.singleton.ServerChangeScene (name);
	}
}
