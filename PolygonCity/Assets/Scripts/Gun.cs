
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

	public Camera fpsCam;

	public float nextTimeToFire = 0f;
	public float fireRate;
	private bool inputFire;

	public GameObject hitMarker;
	private Transform canvasTransform;

	void Start ()
	{
		canvasTransform = GameObject.Find ("Canvas").transform;
	}
		
	public void Shoot ()
	{
		muzzleFlash.Play ();

		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.transform.position, fpsCam.transform.forward, out hit)) {			

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 0.13f);
		}
	}

	public void Hit ()
	{
		GameObject hitMarkerGO = Instantiate (hitMarker, canvasTransform);
		Destroy (hitMarkerGO, .3f);
	}
		
}
