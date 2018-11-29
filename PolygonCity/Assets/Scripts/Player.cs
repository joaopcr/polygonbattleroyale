using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class Player : NetworkBehaviour
{
	[SyncVar]
	private bool _isDead = false;

	public bool isDead {
		get { return _isDead; }
		protected set { _isDead = value; }
	}

	[SerializeField]
	private int maxHealth = 100;

	[SyncVar]
	private int currentHealth;

	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] wasEnabled;

	[SerializeField]
	private GameObject[] GODisableOnDeath;


	public void Setup ()
	{
		wasEnabled = new bool[disableOnDeath.Length];

		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled [i] = disableOnDeath [i].enabled;
		}



		SetDefaults ();
	}

	public Slider healthSlider;

	void Update ()
	{
		healthSlider.value = this.currentHealth;
	}

	[ClientRpc]
	public void RpcTakeDamage (int _amount)
	{
		if (isDead)
			return;
		
		currentHealth -= _amount;
		Debug.Log (currentHealth);

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public Behaviour[] enableOnDeath;

	private void Die ()
	{
		isDead = true;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = false;
		}

		for (int x = 0; x < GODisableOnDeath.Length; x++) {
			GODisableOnDeath [x].SetActive (false);
		}

		for (int y = 0; y < enableOnDeath.Length; y++) {
			enableOnDeath [y].enabled = true;
		}

		GetComponent<Rigidbody> ().useGravity = false;

		Collider _col = GetComponent<Collider> ();
		if (_col != null)
			_col.enabled = true;

		Spectate ();
	}

	public void Spectate ()
	{
		transform.position = new Vector3 (0, 180, 0);
		transform.rotation = Quaternion.Euler (90, 0, 0);
	}

	public void SetDefaults ()
	{
		isDead = false;

		currentHealth = maxHealth;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = wasEnabled [i];
		}

		Collider _col = GetComponent<Collider> ();
		if (_col != null)
			_col.enabled = true;
	}

	public int getCurrentHealth ()
	{
		return this.currentHealth;
	}
}
