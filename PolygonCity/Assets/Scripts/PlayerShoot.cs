using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
	private const string PLAYER_TAG = "Player";

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	private PlayerWeapon currentWeapon;
	private WeaponManager weaponManager;

	public PlayerCanvas playerCanvas;

	void Start ()
	{		
		if (cam == null) {
			Debug.LogError ("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}

		weaponManager = GetComponent<WeaponManager> ();
	}

	void Update ()
	{
		//if (currentWeapon != null)
			//playerCanvas.UpdateAmmo (currentWeapon.bullets.ToString ());
		currentWeapon = weaponManager.GetCurrentWeapon ();

		if (currentWeapon == null)
			return;

		if (currentWeapon.bullets < currentWeapon.maxBullets) {
			if (Input.GetKeyDown (KeyCode.R)) {
				weaponManager.Reload ();
				return;
			}
		}

		if (currentWeapon.fireRate <= 0f) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		} else {
			if (Input.GetButtonDown ("Fire1")) {
				InvokeRepeating ("Shoot", 0f, 1f / currentWeapon.fireRate);
			} else if (Input.GetButtonUp ("Fire1")) {
				CancelInvoke ("Shoot");
			}
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			//Drop ();
		}
	}

	[Command]
	void CmdOnShoot ()
	{
		RpcDoShootEffect ();
	}

	[ClientRpc]
	void RpcDoShootEffect ()
	{
		//weaponManager.GetCurrentGraphics().muzzleFlash.gameObject.SetActive(true);
		weaponManager.GetCurrentGraphics ().muzzleFlash.Play ();
	}

	[Command]
	void CmdOnHit (Vector3 _pos, Vector3 _normal)
	{
		RpcDoHitEffect (_pos, _normal);
	}

	[ClientRpc]
	void RpcDoHitEffect (Vector3 _pos, Vector3 _normal)
	{
		GameObject _hitEffect = (GameObject)Instantiate (weaponManager.GetCurrentGraphics ().hitEffectPrefab, _pos, Quaternion.LookRotation (_normal));	
		Destroy (_hitEffect, 0.2f);
	}

	[Client]
	void Shoot ()
	{
		if (!isLocalPlayer)
			return;

		if (currentWeapon == null) {
			Debug.Log ("nulo");
			return;
		}


		if (weaponManager.isReloading) {
			return;
		}

		if (currentWeapon.bullets <= 0) {			
			weaponManager.Reload ();
			return;
		}

		currentWeapon.bullets--;

		Debug.Log ("Ammo: " + currentWeapon.bullets + "\t Inv Ammo: ");

		CmdOnShoot ();

		RaycastHit _hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask)) {
			if (_hit.collider.tag == PLAYER_TAG) {
				CmdPlayerShot (_hit.collider.name, currentWeapon.damage);
				playerCanvas.HitMarker ();
			}

			CmdOnHit (_hit.point, _hit.normal);
		}
	}

	[Command]
	void CmdPlayerShot (string _playerID, int _damage)
	{
		Player _player = GameManager.GetPlayer (_playerID);
		_player.RpcTakeDamage (_damage);
	}

	/*[Client]
	void Drop ()
	{
		if (!isLocalPlayer)
			return;

		CmdOnDrop (transform.position);
	}

	[Command]
	void CmdOnDrop (Vector3 pos)
	{
		RpcDrop (pos);
	}

	[ClientRpc]
	void RpcDrop (Vector3 pos)
	{
		GameObject d = weaponManager.GetCurrentWeapon ().graphics;
		d.GetComponent<Animator> ().enabled = false;
		GameObject drop = (GameObject)Instantiate (d, pos, Quaternion.identity);
	}*/
}
