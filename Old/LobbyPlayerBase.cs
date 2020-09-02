using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerBase : NetworkBehaviour {

	// Use this for initialization
	void Update () {
		this.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
	}
	
}
