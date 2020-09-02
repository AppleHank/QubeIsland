using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTurret : MonoBehaviour {

	public Tower turret;
	public GameObject Mesh;
	
	public bool isST;
	public bool isBT;
	public bool isFT;
	public bool isIT;
	public bool isHolyturret;
	// Use this for initialization
	void Start () {
		Vector3 tempPosition = transform.position;
		tempPosition.y += 3.5f;
		transform.position = tempPosition;

		if(isST)
		{
			Vector3 STtemp = transform.position;
			STtemp.y += 1.2f;
			transform.position = STtemp;
		}

		if(isBT)
		{
			Vector3 BTtemp = transform.position;
			BTtemp.y += 1.5f;
			transform.position = BTtemp;
		}

		if(isFT)
		{
			Vector3 FTtemp = transform.position;
			FTtemp.y += 0.7f;
			transform.position = FTtemp;
		}

		if(isIT)
		{
			Vector3 Temp = transform.position;
			Temp.y += 1;
			transform.position = Temp;
		}

		if(!isHolyturret)
		{
			Vector3 temp = transform.localScale;
			temp.x = turret.range;
			temp.y = turret.range;
			Mesh.transform.localScale = temp;
		}
	}
	
}
