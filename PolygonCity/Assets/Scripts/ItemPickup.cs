using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	[SerializeField]
	private int amount;

	public int GetAmount ()
	{
		return amount;
	}
}
