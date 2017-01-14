using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
	public int ammo = 3;
	public int maxAmmo = 6;

	public GameObject projectile;

	public void AddAmmo(int ammoIncrease)
	{
		ammo += ammoIncrease;

		if (ammo > maxAmmo) 
		{
			ammo = maxAmmo;
		}
	}

	public virtual void FireWeapon(Transform firePoint, bool isFacingRight)
	{
		ammo -= 1;

		if (ammo >= 0) 
		{
			GameObject clone;
			clone = Instantiate (projectile, firePoint.position, this.transform.rotation);
			Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D> ();

			if (isFacingRight) 
			{
				cloneBody.velocity = transform.TransformVector (50 * Vector3.right);
			} else 
			{
				Vector3 theScale = clone.transform.localScale;
				theScale.x *= -1;
				clone.transform.localScale = theScale;
				cloneBody.velocity = transform.TransformVector (-50 * Vector3.left);
			}
		} else 
		{
			//Alert the player that they have run out of ammo here.<--------Improvement-------->
		}
	}
}
