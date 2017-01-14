using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Enemy inherits from actor.
*/
public class Enemy : Actor 
{
	//Values generated in the start function.
	Rigidbody2D myBody;
	float myWidth;

	//Exposed variables for the unity editor.
	public Transform myTrans;
	public LayerMask whatIsGround;
	public bool isMoving = true;
	public float speed = 5.0f;

	// Use this for initialization.
	void Start() 
	{
		myBody = this.GetComponent<Rigidbody2D>();
		myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
	}

	//Overrides the behaviour for dieing. Mostly to alert the level Manager of it's death.
	public override void OnActorDie()
	{
		//Get the levelManager. Seriously could be a singleton. <-------IMPORVEMENT------->
		GameObject[] endOfLeveObject = GameObject.FindGameObjectsWithTag("LevelEnd");
		LevelFlowManager levelManager = endOfLeveObject [0].GetComponent<LevelFlowManager>();
		levelManager.OnEnemyKilled();

		//Call base death functionality.
		base.OnActorDie();
	}

	//Used to deal damage to the player
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") 
		{
			//Calculates a knock back, incase the player does not die.
			Vector2 dir = coll.transform.position - transform.position; 
			dir.y = 0;

			coll.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 1) * (15f);
			coll.GetComponent<Rigidbody2D>().AddForce (dir * 2000);
			coll.GetComponent<Actor>().DealDamage();
		}

	}

	//Default move implementation for enemys.
	public virtual void Move()
	{
		//Very simple movement. Constantly move the enemy. Change the direction if it's ray can't hit the ground.
		Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down);
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down, whatIsGround);

		if (!isGrounded) 
		{
			Vector3 currRot = myTrans.eulerAngles;
			currRot.y += 180;
			myTrans.eulerAngles = currRot;
		}

		Vector2 myVel = myBody.velocity;

		myBody.velocity = (new Vector2 (1, 0) * (-myTrans.right.x * speed) + (new Vector2 (0, 1) * (myBody.velocity.y)));
	}

	//Update physics is good for, well, physics
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		if(isMoving)
			Move();	
	}
}
