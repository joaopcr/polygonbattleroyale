using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Nova Ammo", menuName = "Ammo")]
public class Ammo : Item
{
	public int quantidade;
	public enum AmmoTipo{Rifle, Pistola, Sniper}
	public AmmoTipo ammoTipo;
}
