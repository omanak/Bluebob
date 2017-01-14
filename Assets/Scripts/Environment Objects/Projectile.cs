using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Basic projectile class
*/
public class Projectile : MonoBehaviour 
{
	// adjust the impact force
	public float force = 500; 

	//Handle hitting anobject
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Enemy")
		{
			Vector2 dir = coll.transform.position - transform.position; 
			dir.y = 0;

			coll.GetComponent<Rigidbody2D> ().AddForce (dir * 500);

			//Change to get damage
			coll.GetComponent<Actor>().DealDamage();

			//Get rid of game object
			Destroy (this.gameObject);
		}
	}
}
