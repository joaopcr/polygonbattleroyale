using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
	public GameObject hitMarkerIMG;
	public Image crosshair;
	public Text ammoText;

	public void HitMarker ()
	{
		hitMarkerIMG.SetActive (true);
		crosshair.color = Color.red;
		StartCoroutine (hitMarkerEnum ());
	}

	IEnumerator hitMarkerEnum ()
	{
		yield return new WaitForSeconds (0.25f);
		hitMarkerIMG.SetActive (false);
		crosshair.color = Color.white;
	}

	public void UpdateAmmo (string s)
	{
		ammoText.text = s;
	}
}
