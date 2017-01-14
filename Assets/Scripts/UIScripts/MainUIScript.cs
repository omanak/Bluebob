using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Very simple UI script. Again will be updated late.
	Probable area for improvement.
*/
public class MainUIScript : MonoBehaviour 
{
	public Text ammoText;

	// Update is called once per frame
	void Update () 
	{
		//Get the levelManager. Seriously could be a singleton
		GameObject endOfLeveObject = GameObject.Find("Character");
		Weapon weapon = endOfLeveObject .GetComponent<Weapon> ();
		ammoText.text = weapon.ammo.ToString();
	}
}
