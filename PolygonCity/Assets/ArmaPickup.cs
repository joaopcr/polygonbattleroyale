using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPickup : MonoBehaviour
{
	[SerializeField]
	private PlayerWeapon playerWeapon;


	public PlayerWeapon GetPlayerWeapon ()
	{
		return playerWeapon;
	}

	public void SetPlayerWeapon (PlayerWeapon _weapon)
	{
		playerWeapon = _weapon;
	}
}
