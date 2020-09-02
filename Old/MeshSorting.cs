using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSorting : MonoBehaviour {

	public MeshRenderer Myrenderer;

	// Use this for initialization
	void Start () {
		
		Myrenderer.sortingLayerName = "Mesh";
		Myrenderer.sortingOrder  = 10;
	}
	

}
