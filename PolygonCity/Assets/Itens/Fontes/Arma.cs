using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Nova Arma", menuName = "Arma")]
public class Arma : Item
{
	[Space]
	[Header("Arma Settings")]
	public int dano;
	public enum ArmaTipo{Rifle, Pistola, Sniper}
	public ArmaTipo armaTipo;
	public int magazine;
	public float fireRate;
	public int zoom;
	public GameObject hitEffectPrefab;

}
