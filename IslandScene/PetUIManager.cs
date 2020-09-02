using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetUIManager : MonoBehaviour {

	public GameObject CaveUI;
	public Canvas PetUICanvas;

	void Start()
	{
		PetUICanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}


}
