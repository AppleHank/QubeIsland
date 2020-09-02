using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.Match;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 6;

	private string roomName;

	private NetworkManager networkManager;

	void Start ()
	{
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
		{
			networkManager.StartMatchMaker();
		}
	}

	public void Search()
	{
		networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);

	}

	public void OnMatchList (bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
	{
			if (!success || (matchList.Count == 0))
			{
				Debug.Log("No room now,Create New Game");
				CreateGame();
			}
			else
			{
				networkManager.matchMaker.JoinMatch(matchList[0].networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
				StartCoroutine(CheckJoinSuccess());
			}
	}
	IEnumerator CheckJoinSuccess()
	{
		int countdown = 5;
		while (countdown > 0)
		{
			Debug.Log(countdown);
				yield return new WaitForSeconds(1);

				countdown--;
		}
	}

	void CreateGame()
	{
		networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
	}

}
