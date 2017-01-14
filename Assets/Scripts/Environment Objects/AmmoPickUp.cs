using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Ammo pick-up that inherits from BasePickup. 
*/
public class AmmoPickUp : BasePickup 
{
	public int ammoIncreaseAmount = 1;

	//Currently just sets the effect for the pick up.
	public override void Effect(Collider2D playerColl)
	{
		playerColl.GetComponent<Weapon> ().AddAmmo(ammoIncreaseAmount);
	}
}
