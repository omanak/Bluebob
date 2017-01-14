using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	Pad to launch the player into the air.
	Point to consider, could it be a pick up? <--------Possible Improvement-------->
*/
public class JumpPad : MonoBehaviour 
{
	public float jumpForce = 10f;

	//Adds a jump force to the player when they walk over it.
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") {
			Rigidbody2D collBody = coll.GetComponent<Rigidbody2D> ();
			collBody.velocity = new Vector2 (0, 1) * (jumpForce);
		}
	}
}
