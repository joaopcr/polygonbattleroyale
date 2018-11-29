using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventario : NetworkBehaviour
{
	private int ARammo = 0;

	[SerializeField]
	private List<PlayerWeapon> weapons;

	[SerializeField]
	private Camera cam;

	private WeaponManager weaponManager;

	public List<PlayerWeapon> GetWeapons ()
	{		
		return weapons;
	}

	public void AddWeapon (PlayerWeapon _weapon)
	{
		weapons.Add (_weapon);
	}

	public void RemoveWeapon (PlayerWeapon _weapon)
	{
		weapons.Remove (_weapon);
	}

	public int GetARammo ()
	{
		return ARammo;
	}

	public void AddARAmmo (int _qtd)
	{
		ARammo += _qtd;
	}

	public void RemoveARAmmo (int _qtd)
	{
		ARammo -= _qtd;
	}
}
