using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	The player's actor class. Currently only overrides Actor. 
*/
public class Player : Actor 
{
	//Once the player dies the scene is re-loaded
	public override void OnActorDie() 
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
