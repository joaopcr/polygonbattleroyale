using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spec : MonoBehaviour
{

	private float speedX;
	private float speedZ;
	private float diveSeped;

	void Update ()
	{
		speedX = Input.GetAxis ("Horizontal");
		speedZ = Input.GetAxis ("Vertical");

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			diveSeped++;
			diveSeped *= 4;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			diveSeped--;
			diveSeped *= 4;
		} else {
			diveSeped = 0;
		}

		float maxX = Mathf.Clamp (speedX, -20, 20);
		float maxZ = Mathf.Clamp (speedZ, -20, 20);
		float maxDive = Mathf.Clamp (diveSeped, 5, 50);

		transform.Translate (speedX, speedZ, diveSeped);
		Vector3 temp = transform.position;
		temp.x = Mathf.Clamp (temp.x, -100, 100);
		temp.y = Mathf.Clamp (temp.y, 3, 180);
		temp.z = Mathf.Clamp (temp.z, -100, 100);
		transform.position = temp;
	}
}
