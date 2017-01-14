using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Flying Enemy, inhertirs from Enemy.
*/
public class FlyingEnemy : Enemy 
{
	public Transform target;
	public float followDistance = 10.0f;

	private float distanceToTarget = 5000f;

	Vector2[] path;
	int targetIndex;

	// Use this for initialization
	void Start () 
	{
		Move ();
	}

	//RefreshPath is launched as a coroutine so it may update it's path as the player moves.
	public override void Move ()
	{
		StartCoroutine (RefreshPath ());
	}

	//RefreshPath also uses coroutines for FollowPath. This allows the enemy to change it's direction.
	IEnumerator RefreshPath() 
	{
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

		while (true) 
		{
			if (targetPositionOld != (Vector2)target.position) 
			{
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath (transform.position, target.position);
				StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}

			yield return new WaitForSeconds (.25f);
		}
	}

	//Simply follows the path provided by the A* planner.
	IEnumerator FollowPath() 
	{
		if (path.Length > 0) 
		{
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) 
			{
				if ((Vector2)transform.position == currentWaypoint) 
				{
					targetIndex++;
					if (targetIndex >= path.Length) 
					{
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}
				if(distanceToTarget < followDistance)
					transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;
			}
		}
	}

	//Visual debugging for the A* path planning.
	public void OnDrawGizmos() 
	{
		if (path != null) 
		{
			for (int i = targetIndex; i < path.Length; i ++) 
			{
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) 
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else 
				{
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
		
	//Overrides FixedUpdate. Constantly works out how close the player is.
	public override void FixedUpdate ()
	{
		distanceToTarget = Vector3.Distance(transform.position, target.position);
	}
}
