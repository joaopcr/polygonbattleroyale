using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
	private float maxStamina = 100f;
	private float currentStamina;
	public float staminaLoss = 5f;
	float startMultiplier;

	public Slider staminaSlider;

	void Start ()
	{
		currentStamina = maxStamina;
		startMultiplier = GetComponent<RigidbodyFirstPersonController> ().movementSettings.RunMultiplier;
	}

	void Update ()
	{
		if (!GetComponent<PlayerCarro> ().dirigindo) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				if (currentStamina >= 0)
					currentStamina -= staminaLoss * Time.deltaTime;
			} else {
				if (currentStamina <= 100)
					currentStamina += staminaLoss / 6 * Time.deltaTime;
			}
			if (currentStamina <= 0) {
				GetComponent<RigidbodyFirstPersonController> ().movementSettings.RunMultiplier = 1;
			} else {
				GetComponent<RigidbodyFirstPersonController> ().movementSettings.RunMultiplier = startMultiplier;
			}
		}

		staminaSlider.value = currentStamina;
	}

	public float GetStamina ()
	{
		return currentStamina;
	}
}
