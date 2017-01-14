using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	All players and NPCs will inherit from this class.
*/
public class Actor : MonoBehaviour 
{
	private int healthPoints = 5;
	private bool isAlive = true;

	//Check to see if the actor is has been killed or not
	public bool ActorIsAlive()
	{
		//Sets the actors alive flag. 
		if (healthPoints <= 0) 
		{
			isAlive = false;
		}

		return isAlive;
	}

	//Function for dealing damage to an actor.
	public virtual void DealDamage(int damage = 5)
	{
		healthPoints -= damage;

		if( !ActorIsAlive() )
			OnActorDie();
	}

	//Called when the actor dies. Can be overrided for unique behaviour.
	public virtual void OnActorDie()
	{
		Destroy (this.gameObject);
	}

	//Update physics is good for, well, physics
	public virtual void FixedUpdate() 
	{
		//Kills the actor if the fall off the map.
		if (this.transform.position.y < -20f) 
		{
			OnActorDie ();
		}
	}
}
