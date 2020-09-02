using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Prototype.NetworkLobby
{
	public class SearchOnlineGame : MonoBehaviour {

		private LobbyManager lobbyManager;
		public float SearchTime;
		public float TotalSearchTime;
		private static bool SuccessToJoin;
		private NetworkManager networkManager;
		public GameObject SearchButton;
		public GameObject StopSearchButton;
		private string FriendName;
		public FriendManager fightmanager;
		public GameObject FindText;
		public GameObject SearchText;
		public GameObject SerachPanel;
		public GameObject SetWarriorPanel;
		public GameObject ChoseFightPanel;
		public List<string> Tips = new List<string>();
		public Text TipsText;
		private int NowTipIndex = -1;
		public Image LoadingPanelImg;
		public List<Sprite> LoadingPanel = new List<Sprite>();
		public Text LoadingTipsText;
		private int NowIndex;


		void Start()
		{
			networkManager = NetworkManager.singleton;
			lobbyManager = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();
			
			StartCoroutine(ChangeLoadingTipsAndSprite());
	//		int index = Random.Range(0,LoadingPanel.Count-1);
	//		LoadingPanelImg.sprite = LoadingPanel[index];
		}

		IEnumerator ChangeLoadingTipsAndSprite()
		{
			
			while(LoadingPanelImg.gameObject.activeSelf)
			{
				int index = Random.Range(0,LoadingPanel.Count-1);
				int TipsIndex = Random.Range(0,Tips.Count-1);
				while(NowIndex == index)
				{
					index = Random.Range(0,LoadingPanel.Count-1);
				}
				while(NowTipIndex == TipsIndex)
				{
					TipsIndex = Random.Range(0,Tips.Count);
				}
				NowIndex = index;
				NowTipIndex = TipsIndex;
				LoadingPanelImg.sprite = LoadingPanel[index];
				LoadingTipsText.text = Tips[NowTipIndex];

				LoadingPanelImg.gameObject.SetActive(true);
				yield return new WaitForSeconds(10f);
			}
		}

		public void StopSearching()
		{
			
            FriendManager FM = GameObject.FindObjectOfType<FriendManager>();
            PetMoveManager PMM = GameObject.FindObjectOfType<PetMoveManager>();
            AdTest AdManager = GameObject.FindObjectOfType<AdTest>();
            FM.UnSubScribeFriend();
            PMM.UnSubScribePet();
			FM.StopTellFriendOnline();
            AdManager.Unsubscribe();
			GameObject.FindObjectOfType<LobbyManager>().GoBackButton();
		}



		public void ReadyToSearch()
		{
			Debug.Log(PetWarrior.WarriorLife);
			Debug.Log(PetWarrior.HasWarrior);
			if(PetWarrior.WarriorLife == 0)
			{
				SetWarriorPanel.SetActive(true);
				return;
			}
			SerachPanel.SetActive(true);
			StartCoroutine(AssignTips());
	//		ChoseFightPanel.SetActive(false);
			lobbyManager.StartMatchMaker();
			SearchTime = Time.time;
			Debug.Log("Searching");
			SearchGame();
		}

		IEnumerator AssignTips()
		{
			while(true)
			{
				Debug.Log("ChangeTips!");
				int TipIndex = Random.Range(0,11);
				while(NowTipIndex == TipIndex)
				{
					TipIndex = Random.Range(0,11);
				}
				TipsText.text = Tips[TipIndex];
				NowTipIndex = TipIndex;

				yield return new WaitForSeconds(10f);
			}
		}

		void SearchGame()
		{
			lobbyManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
			ChoseFightPanel.SetActive(false);
	//		SearchButton.SetActive(false);
	//		StopSearchButton.SetActive(true);
		}

		public void ForGiveSearch()
		{
			StartCoroutine(CancelPanel());
		}

		IEnumerator CancelPanel()
		{
			yield return new WaitForSeconds(10f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

				Debug.Log("Room Num:"+matchList.Count);
				for(int i=matchList.Count-1;i>=0;i--)
				{
					Debug.Log("Current Player Num:"+matchList[i].currentSize);
					if(matchList[i].name != "TempRoomName" + PlayerData.PlayerName)
					{
						if(matchList[i].name.Contains("TempRoomName") & matchList[i].currentSize == 1)
						{
							Debug.Log("has room,join");

							FriendManager FM = GameObject.FindObjectOfType<FriendManager>();
							PetMoveManager PMM = GameObject.FindObjectOfType<PetMoveManager>();
							AdTest AdManager = GameObject.FindObjectOfType<AdTest>();
							FM.UnSubScribeFriend();
							PMM.UnSubScribePet();
							FM.StopTellFriendOnline();
							AdManager.Unsubscribe();
							Destroy(AdManager.gameObject);

							lobbyManager.matchMaker.JoinMatch(matchList[i].networkId, "", "", "", 0, 0,lobbyManager.OnMatchJoined);
							return;
						}
						else if(matchList[i].currentSize != 1)
							Debug.Log("Player Error:"+matchList[i].currentSize);
					}
					else
					{
						Debug.Log("ErrorRoomCatch!!!!!!!!!!!!!!!!!!!");
///						matchList[i].name = "ErrorRoom";
//						Debug.Log(matchList[i].name);
					}
				}
				
				CreateGame();
				Debug.Log("Create");
			//	StartCoroutine(CheckJoinSuccess());
			}
		}

		public void StopSearch()
		{
			StartCoroutine(WaitForCreateRoom());
		}

		IEnumerator WaitForCreateRoom()
		{
			yield return new WaitForSeconds(2f);
			Debug.Log("Drop Connection");
			MatchInfo matchInfo = networkManager.matchInfo;
			networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
			networkManager.StopHost();
			SerachPanel.SetActive(false);
		}


		IEnumerator CheckJoinSuccess()
		{
			float WaitSeconds = Random.Range(2f,4f);
			yield return new WaitForSeconds(WaitSeconds);
			if(SuccessToJoin)
				Debug.Log("succeess");
			else
			{
				ReadyToSearch();
			}
		}

		public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
 		{
			
			if(!success)
				Debug.Log("fail join game");
			else
			{
				SuccessToJoin = true;
				Debug.Log("success to join game");
				SearchText.SetActive(false);
				FindText.SetActive(true);
	//			lobbyManager.ServerChangeScene(lobbyManager.playScene);
			}
		}

		void CreateGame()
		{
			lobbyManager.matchMaker.CreateMatch(
				"TempRoomName"+PlayerData.PlayerName,
				(uint)lobbyManager.maxPlayers,
				true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);
		}

		public void InviteFriend(Text FriendName)
		{
			lobbyManager = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();
			networkManager = NetworkManager.singleton;
			networkManager.StartMatchMaker();
			Debug.Log(lobbyManager);
			Debug.Log(lobbyManager.matchMaker);
			lobbyManager.matchMaker.CreateMatch(
				FriendName.text,
				(uint)lobbyManager.maxPlayers,
				true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);
			Debug.Log(FriendName.text);
			StartCoroutine(FriendFightRoomCreated());
		}

		IEnumerator FriendFightRoomCreated()
		{
			yield return new WaitForSeconds(2f);
			fightmanager.FriendJoin();
		}

		public void JoinFriendFight(string FriendName)
		{

			lobbyManager = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();
			networkManager = NetworkManager.singleton;
			networkManager.StartMatchMaker();
			this.FriendName = FriendName;
			Debug.Log(this.FriendName);
			Debug.Log(FriendName);
			Debug.Log("SOG "+FriendName);
			lobbyManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, FriendFightList);
		}

		public void FriendFightList (bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
		{
			if (!success || (matchList.Count == 0))
			{
				Debug.Log("NoRoomNow");	
				Debug.Log("Search Agagin");
				lobbyManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, FriendFightList);
			}
			else
			{
				Debug.Log(matchList.Count);
				Debug.Log("FindingFriendRoom");
				for(int i=0;i<matchList.Count;i++)
				{
					Debug.Log(matchList[i].name);
					if(matchList[i].name == PlayerData.PlayerName)	
					{
						Debug.Log("FIght With Friend!!!");
						lobbyManager.matchMaker.JoinMatch(matchList[i].networkId, "", "", "", 0, 0,lobbyManager.OnMatchJoined);
					}
				}
			//	StartCoroutine(CheckJoinSuccess());
			}
		}

	}
	
}