using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
	public Inventario inventario;

	void Start ()
	{
		AtualizarInventario ();
	}

	void Update ()
	{
		AtualizarInventario ();
	}

	public void AtualizarInventario ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<Image> ().sprite = null;
		}

		if (inventario.GetWeapons () != null) {
			for (int i = 0; i < inventario.GetWeapons ().Count; i++) {
				if (transform.GetChild (i).GetComponent<Image> ().sprite == null) {
				
					transform.GetChild (i).GetComponent<Image> ().sprite = inventario.GetWeapons () [i].graphics.GetComponent<WeaponGraphics> ().thumb;

				}
			}
		}
	}
}
