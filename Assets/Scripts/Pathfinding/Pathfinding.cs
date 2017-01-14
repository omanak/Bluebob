using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

/*
	Main A* search algorithm manager. It's a singleton. Only one instance will exist.
*/
public class Pathfinding : MonoBehaviour {

	Grid grid;
	static Pathfinding instance;

	//Gets an instance of itself
	void Awake() 
	{
		grid = GetComponent<Grid>();
		instance = this;
	}

	//Public interface used to request a path. Returns an array containing the path
	public static Vector2[] RequestPath(Vector2 from, Vector2 to) 
	{
		return instance.FindPath (from, to);
	}

	//A* search algorithm
	Vector2[] FindPath(Vector2 from, Vector2 to) 
	{
		Vector2[] waypoints = new Vector2[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(from);
		Node targetNode = grid.NodeFromWorldPoint(to);
		startNode.parent = startNode;

		if (!startNode.walkable) 
		{
			startNode = grid.ClosestWalkableNode (startNode);
		}
		if (!targetNode.walkable)
		{
			targetNode = grid.ClosestWalkableNode (targetNode);
		}
		
		if (startNode.walkable && targetNode.walkable) 
		{
			
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0)
			{
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) 
				{
					pathSuccess = true;
					break;
				}
				
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) 
				{
					if (!neighbour.walkable || closedSet.Contains(neighbour))
					{
						continue;
					}

					//This is where the movement cost is calculated. Any adjustments to that value can be made here.
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour)+TurningCost(currentNode,neighbour) + currentNode.nodePenalty;
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						else 
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}

		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		}

		return waypoints;
		
	}

	//Currently not used. But if the code is re-used is can be added here
	int TurningCost(Node from, Node to)
	{
		/*
		Vector2 dirOld = new Vector2(from.gridX - from.parent.gridX, from.gridY - from.parent.gridY);
		Vector2 dirNew = new Vector2(to.gridX - from.gridX, to.gridY - from.gridY);
		if (dirNew == dirOld)
			return 0;
		else if (dirOld.x != 0 && dirOld.y != 0 && dirNew.x != 0 && dirNew.y != 0) {
			return 5;
		}
		else {
			return 10;
		}
		*/

		return 0;
	}
	
	Vector2[] RetracePath(Node startNode, Node endNode) 
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
		
	}
	
	Vector2[] SimplifyPath(List<Node> path) 
	{
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++) 
		{
			//Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
		//	if (directionNew != directionOld)
		//	{
				waypoints.Add(path[i].worldPosition);
			//}
			//directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
	
	int GetDistance(Node nodeA, Node nodeB) 
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
	
	
}
