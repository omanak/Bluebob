using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	Makes sure that enemys in a level have been destroyed.
*/
public class LevelFlowManager : MonoBehaviour 
{	
	public string nextLevel;
	public int enemiesInScene = 0;

	// Use this for initialization
	void Start() 
	{
		enemiesInScene = GameObject.FindGameObjectsWithTag ("Enemy").Length;
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player" && enemiesInScene == 0) 
		{
			SceneManager.LoadScene(nextLevel);
		}
	}

	public void OnEnemyKilled() 
	{
		enemiesInScene -= 1;
	}
}
