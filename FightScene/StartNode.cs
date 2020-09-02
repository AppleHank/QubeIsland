using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : MonoBehaviour {

	public NODE startnode;
	public GameObject PathPrefab;
	// Use this for initialization
	void Start () {

		startnode.isStartNode = true;
		GameObject Path = Instantiate(PathPrefab,transform.position,Quaternion.identity);
		NODE node = GetComponent<NODE>();
		node.turret = Path;
	}
	
}
