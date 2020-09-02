using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ToFight : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
		Debug.Log("ToFight");
		if(isServer)
        	NetworkManager.singleton.ServerChangeScene("Fight");
	}
	
}
