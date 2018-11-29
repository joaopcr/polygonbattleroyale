using UnityEngine;

[System.Serializable]
public class PlayerWeapon
{
	public string name = "Glock";

	public string tipo = "AR";

	public int damage = 10;
	public float range = 100f;

	public float fireRate = 0f;

	public int maxBullets = 20;

	public int bullets;

	public float reloadTime = 1f;

	public GameObject graphics;

	public Arma arma;

	public PlayerWeapon ()
	{
		bullets = maxBullets;
	}

	public PlayerWeapon (string _name, int _damage, float _range, float _fireRate, int _maxBullets, int _bullets, float _reloadTime, GameObject _graphics)
	{
		this.name = _name;
		this.damage = _damage;
		this.range = _range;
		this.fireRate = _fireRate;
		this.maxBullets = _maxBullets;
		this.bullets = _bullets;
		this.graphics = _graphics;

	}
}
