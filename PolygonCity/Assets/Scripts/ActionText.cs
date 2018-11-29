using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionText : MonoBehaviour {

	private static string text = "";

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = text;
	}

	public static void Trigger(string t){
		text = t;
	}
}
