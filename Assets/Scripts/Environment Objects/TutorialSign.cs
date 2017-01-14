using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Quick script to get tutorial messages working
*/
public class TutorialSign : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") 
		{
			this.GetComponentInChildren<Canvas> ().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") 
		{
			this.GetComponentInChildren<Canvas> ().enabled = false;
		}
	}
}
