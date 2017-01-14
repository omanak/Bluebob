using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	The base pick-up. Anything that is picked up by the player. Current plans are ammo and keys.
*/
public abstract class BasePickup : MonoBehaviour 
{
	//All pick ups must mave some kind of effect. From increasing ammo, to opening a door.
	public abstract void Effect(Collider2D playerColl);

	//A function that can be overidden to alter what the pick-up does before it dies. Consider playing an animation, then calling destroy. <--------Improvement-------->
	public virtual void AfterPickUp()
	{
		Destroy(this.gameObject);
	}

	//Triggers the effect when the player touches the item.
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") 
		{
			Effect(coll);
			AfterPickUp ();
		}
	}
}
