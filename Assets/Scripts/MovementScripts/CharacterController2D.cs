using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 	Class for moving the character and firing weapons
 	Biggest class so far. Make sure it doesn't become bloated.
 	Input mapping. Make controls re-bindable.<--------Improvement-------->
*/
public class CharacterController2D : MonoBehaviour 
{

	//Max speed for the character and jumpforce used for physics calculations.
	public float maxSpeed = 10f;
	public float jumpForce = 60f;

	public Transform detectGround;
	public Transform projectileFiringTransform;

	public LayerMask whatIsGround;
	public GameObject projectile;

	bool isFacingRight = true;
	bool grounded = false;

	Rigidbody2D myBody;
	Animator anim;
	float groundRadius = 0.6f;

	// Use this for initialization
	void Start() 
	{
		myBody = this.GetComponent<Rigidbody2D> ();
		anim = this.GetComponent<Animator> ();
	}

	//Update physics is good for, well, physics
	void FixedUpdate()
	{
		//Check if the player is on the ground
		grounded = Physics2D.OverlapCircle (detectGround.position, groundRadius, whatIsGround);

		//Get raw input data
		float playerDirection = Input.GetAxisRaw ("Horizontal");

		//Set velocity, in a non-float percise manor
		myBody.velocity = (new Vector2 (1, 0) * (playerDirection * maxSpeed)) + (new Vector2 (0, 1) * (myBody.velocity.y));

		//Adjust animation variables
		anim.SetFloat("Speed",Mathf.Abs(playerDirection));
		anim.SetBool ("Grounded", grounded);

		//face the player in the correct direction
		if (playerDirection > 0 && !isFacingRight) 
		{
			Flip ();
		} else if (playerDirection < 0 && isFacingRight) 
		{
			Flip ();
		}
	}

	// Update is called once per frame. Good for key input
	void Update () 
	{
		//Jump force. Try input mapping. Then add re-bindable keys
		if(Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			myBody.velocity = new Vector2 (0, 1) * (jumpForce);
		}

		//Finds the attached weapon and fires it. Also change to input mapping
		if(Input.GetKeyDown(KeyCode.E))
		{
			Weapon myWeapon = this.GetComponent<Weapon> ();

			myWeapon.FireWeapon (projectileFiringTransform, isFacingRight);
				
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			this.GetComponent<Actor> ().OnActorDie();
		}
	}

	//Swaps the direction of the player. Consider changing to a full rotation
	void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
