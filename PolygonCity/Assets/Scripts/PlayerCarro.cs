using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Utility;

public class PlayerCarro : MonoBehaviour
{

	Transform originalParent;
	GameObject[] carros;
	bool achou = false;
	public Text actionText;
	GameObject carro = null;
	public bool dirigindo;

	void Start ()
	{
		carros = GameObject.FindGameObjectsWithTag ("Carro");
		originalParent = transform.parent;
		carro = null;
	}

	void Update ()
	{
		if (carros.Length > 0) {
			foreach (GameObject c in carros) {
				if ((transform.position - c.transform.position).sqrMagnitude < 3 * 5) {
					achou = true;
					carro = c;
				}
			}
		}

		if (dirigindo) {
			if (Input.GetKeyDown (KeyCode.E)) {
				SairCarro ();
				return;
			}
		}

		if (achou && !dirigindo) {
			actionText.text = "Aperte E para entrar no carro";
			if (Input.GetKeyDown (KeyCode.E)) {
				EntrarCarro ();
			}
		} else {
			actionText.text = "";
		}

		achou = false;
	}

	void EntrarCarro ()
	{
		GetComponent<RigidbodyFirstPersonController> ().enabled = false;
		GetComponent<CapsuleCollider> ().isTrigger = true;
		GetComponent<Rigidbody> ().isKinematic = true;
		transform.parent = carro.transform;
		transform.localPosition = new Vector3 (-0.5f, .75f, 0.1f);
		transform.localRotation = Quaternion.identity;
		dirigindo = true;
		GetComponent<SimpleMouseRotator> ().enabled = true;
		LigarCarro ();
	}

	void LigarCarro ()
	{
		carro.GetComponent<CarController> ().enabled = true;
		carro.GetComponent<CarUserControl> ().enabled = true;
		carro.GetComponent<Rigidbody> ().isKinematic = false;
		carro.GetComponent<CarAudio> ().enabled = true;
		foreach (AudioSource auds in carro.GetComponents<AudioSource> ()) {
			auds.enabled = true;
		}
	}

	void SairCarro ()
	{
		DesligarCarro ();
		GetComponent<SimpleMouseRotator> ().enabled = false;
		dirigindo = false;
		GetComponent<RigidbodyFirstPersonController> ().enabled = true;
		GetComponent<CapsuleCollider> ().isTrigger = false;
		GetComponent<Rigidbody> ().isKinematic = false;
		transform.parent = originalParent;
		carro = null;

	}

	void DesligarCarro ()
	{		
		carro.GetComponent<CarUserControl> ().enabled = false;
		carro.GetComponent<Rigidbody> ().isKinematic = true;
		carro.GetComponent<CarController> ().enabled = false;
		foreach (AudioSource auds in carro.GetComponents<AudioSource> ()) {
			auds.enabled = false;
		}
	}
}
