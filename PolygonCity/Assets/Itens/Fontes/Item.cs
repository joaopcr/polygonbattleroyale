using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Item")]
public class Item : ScriptableObject {
	[Header("Item Settings")]
	public new string nome;
	public GameObject prefab;
	public GameObject drop;
	public Sprite thumbnail;
	public enum Tipo {Arma, Ammo, Cura, Escudo};
	public Tipo tipo;

}
