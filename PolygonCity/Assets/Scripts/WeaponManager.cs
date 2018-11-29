using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{
	[SerializeField]
	private Transform weaponHolder;

	[SerializeField]
	private PlayerWeapon primaryWeapon;

	private PlayerWeapon currentWeapon;
	private WeaponGraphics currentGraphics;

	private Inventario inventario;

	public bool isReloading = false;

	[SerializeField]
	private Camera cam;

	private PlayerCanvas playerCanvas;

	void Start ()
	{
		playerCanvas = GetComponentInChildren<PlayerCanvas> ();
		inventario = GetComponent<Inventario> ();
		EquipWeapon (primaryWeapon);
	}

	public PlayerWeapon GetCurrentWeapon ()
	{
		return currentWeapon;
	}

	public WeaponGraphics GetCurrentGraphics ()
	{
		return currentGraphics;
	}

	void Update ()
	{
		if (currentWeapon != null) {
			if (currentWeapon.tipo == "AR") {
				playerCanvas.UpdateAmmo (currentWeapon.bullets.ToString () + "/" + inventario.GetARammo ().ToString ());
			}
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			SwapWeapon ();
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			Drop ();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			RaycastHit _hit;
			if (Physics.Raycast (cam.transform.position, cam.transform.forward, out _hit, 100)) {		
				if (_hit.transform.tag == "Arma") {					
					Pickup (_hit.transform.gameObject);
				} else if (_hit.transform.tag == "ARammo") {
					PickupARammo (_hit.transform.gameObject);
				}				
			}
		}
	}

	int invIndex = 0;

	[Client]
	void SwapWeapon ()
	{

		if (!isLocalPlayer) {
			return;
		}

		CmdOnSwap ();

	}

	[Command]
	void CmdOnSwap ()
	{
		RpcOnSwap ();
	}

	[ClientRpc]
	void RpcOnSwap ()
	{
		if (inventario.GetWeapons ().Count > 0) {
			if (weaponHolder.childCount > 0) {
				Destroy (weaponHolder.GetChild (0).gameObject);
			}
			if (inventario.GetWeapons ().Count - 1 <= invIndex) {
				invIndex = 0;
			} else {
				invIndex++;
			}
			EquipWeapon (inventario.GetWeapons () [invIndex]);
		} else {
			return;
		}
	}

	[Client]
	void EquipWeapon (PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;

		GameObject _weaponIns = (GameObject)Instantiate (_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
		_weaponIns.transform.SetParent (weaponHolder);

		if (_weaponIns.GetComponent<Animator> () != null)
			_weaponIns.GetComponent<Animator> ().enabled = true;
		
		currentGraphics = _weaponIns.GetComponent<WeaponGraphics> ();

		if (currentGraphics == null)
			Debug.LogError ("No WeaponGraphics component on the weapon object");
	}

	public void Reload ()
	{
		if (isReloading)
			return;

		StartCoroutine (Reload_Coroutine ());
	}

	private IEnumerator Reload_Coroutine ()
	{
		Debug.Log ("Reloading...");

		isReloading = true;

		CmdOnReload ();

		yield return new WaitForSeconds (currentWeapon.reloadTime);

		currentWeapon.bullets = currentWeapon.maxBullets;

		isReloading = false;
	}

	[Command]
	void CmdOnReload ()
	{
		RpcOnReload ();
	}

	[ClientRpc]
	void RpcOnReload ()
	{
		Animator anim = currentGraphics.GetComponent<Animator> ();
		if (anim != null) {
			anim.SetTrigger ("Reload");
		}
	}

	[Client]
	void Drop ()
	{
		if (!isLocalPlayer)
			return;

		if (weaponHolder.childCount == 0)
			return;

		inventario.RemoveWeapon (currentWeapon);
		CmdOnDrop (transform.position);
	}

	[Command]
	void CmdOnDrop (Vector3 pos)
	{
		GameObject d = currentWeapon.graphics;
		d.GetComponent<Animator> ().enabled = false;
		GameObject drop = (GameObject)Instantiate (d, pos, Quaternion.identity);	
		drop.GetComponent<ArmaPickup> ().SetPlayerWeapon (currentWeapon);
		NetworkServer.Spawn (drop);
		RpcDrop (pos);
	}

	[ClientRpc]
	void RpcDrop (Vector3 pos)
	{
		Debug.Log ("DroppedGIT");
		Destroy (weaponHolder.GetChild (0).gameObject);
		currentWeapon = null;
	}

	[Client]
	void Pickup (GameObject go)
	{
		if (!isLocalPlayer)
			return;

		CmdOnPickup (go);
	}

	[Command]
	void CmdOnPickup (GameObject go)
	{
		RpcPickup (go);
	}

	[ClientRpc]
	void RpcPickup (GameObject go)
	{
		PlayerWeapon pw = go.GetComponent<ArmaPickup> ().GetPlayerWeapon ();

		GameObject graph = null;

		if (pw.name == "M4A1") {
			graph = (GameObject)Resources.Load ("M4_Carbine");
		} else if (pw.name == "AK47") {
			graph = (GameObject)Resources.Load ("AK47");
		}

		inventario.AddWeapon (new PlayerWeapon (pw.name, pw.damage, pw.range, pw.fireRate, pw.maxBullets, pw.bullets, pw.reloadTime, graph));
		Destroy (go);

		NetworkServer.Destroy (go);
	}

	[Command]
	void CmdOnEquipWeapon (PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;
		RpcEquipWeapon (_weapon);
	}

	[ClientRpc]
	void RpcEquipWeapon (PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;
	}

	[Client]
	void PickupARammo (GameObject go)
	{
		if (!isLocalPlayer)
			return;

		CmdOnPickupARammo (go);
	}

	[Command]
	void CmdOnPickupARammo (GameObject go)
	{
		RpcPickupARammo (go);
	}

	[ClientRpc]
	void RpcPickupARammo (GameObject go)
	{
		inventario.AddARAmmo (go.GetComponent<ItemPickup> ().GetAmount ());
		Destroy (go);
		NetworkServer.Destroy (go);
	}

	#region Spawn e Destroy arma na mao do jogador

	/*[Client]
	void SpawnWeapon ()
	{
		if (!isLocalPlayer)
			return;

		CmdOnSpawnWeapon ();
	}

	[Command]
	void CmdOnSpawnWeapon ()
	{
		GameObject a = currentWeapon.graphics;
		GameObject arma = (GameObject)Instantiate (a, Vector3.zero, Quaternion.identity);
		arma.transform.parent = weaponHolder.transform;
		NetworkServer.Spawn (arma);
	}

	[Client]
	void DestroyWeapon ()
	{
		if (!isLocalPlayer)
			return;

		CmdOnDestroyWeapon (weaponHolder.GetChild (0).gameObject);
	}

	[Command]
	void CmdOnDestroyWeapon (GameObject go)
	{
		RpcDestroyWeapon (go);
	}

	[ClientRpc]
	void RpcDestroyWeapon (GameObject go)
	{
		Destroy (go);
		NetworkServer.Destroy (go);
	}*/

	#endregion
}