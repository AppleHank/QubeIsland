using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CamaraControl : NetworkBehaviour {

	private GameObject MainCamera;
//	public float minposi;
//	public float maxposi;
	public GameObject HostArea;
	public GameObject ClientArea;
	private GameObject EnemyArea;
	private GameObject SelfArea;
	private Vector3 EnemyAreatemp;
	private Vector3 SelfAreatemp;
	public bool NowWatchEnemyArea;

	void Start()
	{
		MainCamera = this.gameObject;
		if(isServer)
		{
			Vector3 temp = HostArea.transform.position;
			temp.z = -10;
			this.transform.position = temp;

			EnemyArea = ClientArea;
			SelfArea = HostArea;
			
		}
		if(!isServer)
		{
			Vector3 temp = ClientArea.transform.position;
			temp.z = -10;
			this.transform.position = temp;
			
			EnemyArea = HostArea;
			SelfArea = ClientArea;
		}
		EnemyAreatemp = EnemyArea.transform.position;
		EnemyAreatemp.z = -10;

		SelfAreatemp = SelfArea.transform.position;
		SelfAreatemp.z = -10;
		
	}

	// Update is called once per frame
	void Update () {
//		if(Input.GetKey("w"))
//		{
//			Vector3 temp = MainCamera.transform.position;
//			temp.y += 2.5f;
//			MainCamera.transform.position = new Vector3(temp.x,Mathf.Clamp(temp.y,minposi,maxposi),temp.z);
//		}
//		else if(Input.GetKey("s"))
//		{
//			Vector3 temp = MainCamera.transform.position;
//			temp.y -= 2.5f;
//			MainCamera.transform.position = temp;
//		}

		
		if(Input.GetKeyDown("s"))
		{
			MainCamera.transform.position = EnemyAreatemp;
		}
		
		else if(Input.GetKeyUp("s"))
		{
			MainCamera.transform.position = SelfAreatemp;
		}
	}

	public void ChangeView()
	{
		if(!NowWatchEnemyArea)
		{
			MainCamera.transform.position = EnemyAreatemp;
			NowWatchEnemyArea = true;
		}
		else
		{
			MainCamera.transform.position = SelfAreatemp;
			NowWatchEnemyArea = false;
		}
	}
	
}