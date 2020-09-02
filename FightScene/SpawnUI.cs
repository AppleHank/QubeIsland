using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUI : MonoBehaviour {

	public Canvas UI;
	public Camera MainCamera;
	public GameObject EditCanvas;
	public GameObject BuildCanvas;
	public GameObject BuildNode;

	// Use this for initialization
	void Start () {
		Canvas UICanvas = Instantiate(UI);
		UICanvas.worldCamera  = MainCamera;
	}
	
}
