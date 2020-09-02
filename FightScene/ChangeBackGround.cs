using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangeBackGround : NetworkBehaviour {

	public Sprite EnemyBackGround;
	public GameObject Mirror;
	public GameObject HostArea;
	public GameObject HostGrid;
	public GameObject HostCancel;

	public GameObject ClientArea;
	public GameObject ClientGrid;
	public GameObject ClientCancel;
	public buildmanager Buildmanager;
	private bool Success = false;

	// Use this for initialization
	void Start () {
		InvokeRepeating("AssignBackGround",0,0.5f);
		if(isServer)
		{
			Buildmanager.Grid = HostGrid;
			Buildmanager.CancelBuild = HostCancel;
		}
		if(!isServer)
		{
			Buildmanager.Grid = ClientGrid;
			Buildmanager.CancelBuild = ClientCancel;
		}
	}

	void Update()
	{
		if(Success)
			Cancel();
	}

	void AssignBackGround()
	{
		Debug.Log("Searching");
		if(isServer)
			if(ClientArea.transform.position.x>Mirror.transform.position.x)
			{
				ClientArea.GetComponent<SpriteRenderer>().sprite = EnemyBackGround;
				Success = true;
				Debug.Log("Server Succeees");
			}

		if(!isServer)
			if(HostArea.transform.position.x<Mirror.transform.position.x)
			{
				HostArea.GetComponent<SpriteRenderer>().sprite = EnemyBackGround;
				Success = true;
				Debug.Log("Client SUcecess");
			}
	}

	void Cancel()
	{
		CancelInvoke("AssignBackGround");
		enabled = false; //tell system not call update anymore
	}
	
}
