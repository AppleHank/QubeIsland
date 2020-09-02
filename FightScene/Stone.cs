using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Stone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
		// Safe to update graphs here
		var node = AstarPath.active.GetNearest(transform.position).node;
		node.Walkable = true;
		}));
	}

}
