using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Obstalce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
			var node = AstarPath.active.GetNearest(transform.position).node;
			node.Walkable = false;
			}));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
