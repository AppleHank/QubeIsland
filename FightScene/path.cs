using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Networking;

public class path : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
		// Safe to update graphs here
		var node = AstarPath.active.GetNearest(transform.position).node;
		node.Walkable = true;
		}));

		if(Time.time > 1 & gameObject.GetComponent<AudioSource>() != null)
		{
			this.GetComponent<AudioSource>().Play();
			Debug.Log(Time.time);
		}
		
	}

	void PathWalkable ()
	{
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
		// Safe to update graphs here
		var node = AstarPath.active.GetNearest(transform.position).node;
		node.Walkable = true;
		}));
	}
	

}
