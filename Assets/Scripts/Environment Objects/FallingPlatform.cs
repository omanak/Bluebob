using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Very simple script to allow for falling platforms.
*/
public class FallingPlatform : MonoBehaviour 
{
	public float fallDelay = 0.2f;
	bool isTriggered = false;

	//Check to see if the player enters the object.
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") 
		{
			if (!isTriggered) 
			{
				isTriggered = true;
				StartCoroutine (WaitForFall());
			}
		}
	}

	//Simply waits the fall delay and makes the rigidbody kinematic.
	IEnumerator WaitForFall() 
	{
		yield return new WaitForSeconds(fallDelay);
		this.GetComponent<Rigidbody2D> ().isKinematic = false;
	}
}
