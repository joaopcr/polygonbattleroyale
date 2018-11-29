using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{

	public static ItemStorage instance;

	void Awake ()
	{
		instance = this;
	}

	public List<WeaponManager> items;
}
