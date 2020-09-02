using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Prototype.NetworkLobby
{
	public class DisConnect : MonoBehaviour {

		private LobbyManager lobbymanager;

		void Start()
		{
			lobbymanager = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();
		}

		public void disconnect()
		{
			lobbymanager.GoBackButton();
		}
	}
}
