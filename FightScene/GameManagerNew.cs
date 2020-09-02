using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Pathfinding;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


[System.Serializable]
public class TextAndImg
{
	public Text BackBee;
	public Text Text1;
	public Text Text2;
	public Text Text3;
	public Image Img1;
	public Image Img2;
	public Image Img3;
	private string Meterial1;
	private string Meterial2;
	private string Meterial3;
}

public class GameManagerNew : NetworkBehaviour {


	public Sprite WoodSprite;
	public Sprite MetalSprite;
	public Sprite RockSprite;
	public Sprite EarthSprite;
	public Sprite CottonSprite;
	private int SetNameTime= 0;
	[Header("Doll")]
	public Image LeftHead;
	public Image LeftEye;
	public Image LeftMouse;
	public Image LeftCloth;
	public Image LeftPants;
	public Image LeftUpperArm;
	public Image SelfHead;
	public Image SelfEye;
	public Image SelfMouse;
	public Image SelfCloth;
	public Image SelfPants;
	public Image SelfUpperArm;
	public Image RightHead;
	public Image RightEye;
	public Image RightMouse;
	public Image RightCloth;
	public Image RightPants;
	public Image RightUpperArm;
	public Image OppositeHead;
	public Image OppositeEye;
	public Image OppositeMouse;
	public Image OppositeCloth;
	public Image OppositePants;
	public Image OppositeUpperArm;
	public Sprite ServerHead;
	public Sprite ServerEye;
	public Sprite ServerMouse;
	public Sprite ServerCloth;
	public Sprite ServerPants;
	public Sprite ServerUpperArm;
	public Sprite ClientHead;
	public Sprite ClientEye;
	public Sprite ClientMouse;
	public Sprite ClientCloth;
	public Sprite ClientPants;
	public Sprite ClientUpperArm;

	public Image LeftIcon;
	public Image RightIcon;
	public Image LeftIcon2;
	public Image RightIcon2;
	public Sprite ServerIcon;
	public Sprite ClientIcon;
	public string ServerName;
	public string ClientName;
	public Sprite ServerPet;
	public Sprite ClientPet;
	public Text LeftName;
	public Text RightName;
	public Image LeftBar;
	public Image RightBar;
	public Text LeftLiveText;
	public Text RightLiveText;
	private Player HostPlayer;
	private Player ClientPlayer;
	public GameObject GameLoseUI;
	public GameObject GameWinUI;
	public GameObject GameDrawUI;
	private bool GameEnd;
	public GameObject ShadManager;
	public MeterialObtain WinAward;
	public MeterialObtain LoseAward;
	public MeterialObtain DrawAward;
	public GameObject WinPanel;
	public Animator WindAwardAnimate;
	public GameObject LosePanel;
	public Animator LoseAwardAimate;
	public GameObject DrawPanel;
	public Animator DrawAwardAnimate;
	public AudioSource BGM; 
	public AudioSource ReWard;
	private bool CanAddMaterial; 
	private int AddMaterialNum;
	private bool ServerDontOpenIcon;
	private bool ClientDontOpenIcon;
	private bool ServerTwoIcon;
	private bool ClientTwoIcon;
	private bool ServerCakePos1;
	private bool ClientCakePos1;
	public Sprite Cake;
	public TextAndImg WinUI;
	public TextAndImg DrawUI;
	public TextAndImg LoseUI;
	private NetworkManager NM;
	private NetPlayer netplayer;
	public Text OpenAnimationDownName;
	public Text OpenAnimationUpperName;
	public Image OpenAnimationDownPet;
	public Image OpenAnimationUpperPet;
	private int SetDollTime;
	public GameObject WaitPanel;
	public GameObject Opening;
	private int TotalFightTime;
	private int WinFightTime;
	public int ServerID;
	public int ClientID;
	public Image HeadImg;
	public Image EyesImg;
	public Image ClothImg;
	public Image PantsImg;
	public Image UpperArmImg;
	public Image MouseImg;
	public Image WarriorImg;
	public Text WarriorBuffText;
	public Text SpecialBuffText;
	public Text WarriorLevelText;
	public Image WarriorStarImg;
	public Sprite WarriorGrade2Star;
	public Text NameText;
	public Text ScoreText;
	public Text WinRateText;
	public GameObject InviteBlock;
	private bool NotSetOver = true;
	public bool GameStart;

	void Start()
	{
		WaitPanel.SetActive(true);
		Player.AddedMoney = 200;
		GameEnd = false;

		Debug.Log(PetWarrior.WarriorName);
		Debug.Log(PetWarrior.WarriorLife);
		HostPlayer = GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<Player>();
		ClientPlayer = GameObject.FindGameObjectsWithTag("ClientHealth")[0].GetComponent<Player>();

		GetFightInfo();
		InvokeRepeating("Check",1f,1f);
	}

	public void SetGameStart()
	{
		GameStart = true;
	}

	void Check()
	{
		Debug.Log("GameStart Status:"+GameStart);
		if(SetDollTime < 2 && GameStart)
		{
			if(WaitPanel.activeSelf)
			{
				Debug.Log("Not Set Over::"+NotSetOver);
				SetDoll();
			}
			else
				CancelInvoke("Check");
		}
		else if(SetDollTime >= 2)
		{
			CancelInvoke("Check");
		}
	}

	
	async void GetFightInfo()
	{
				
		TotalFightTime = await FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("FightTotalTime").GetValueAsync().ContinueWith( task => {
			DataSnapshot snapshot = task.Result;
			return int.Parse(snapshot.GetRawJsonValue());
		});
		WinFightTime = await FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("FightWinTime").GetValueAsync().ContinueWith( task => {
			DataSnapshot snapshot = task.Result;
			return int.Parse(snapshot.GetRawJsonValue());
		});

		SetTotalFightTime();
	}

	void SetTotalFightTime()
	{
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString());
		reference.Child("FightTotalTime").SetValueAsync(TotalFightTime+1);
	}

	public void Surrender()
	{
//		NM = NetworkManager.singleton;
		NetPlayer[] NPs = GameObject.FindObjectsOfType<NetPlayer>();		
		foreach(NetPlayer NP in NPs)
		{
			if(NP.isLocalPlayer)
				netplayer = NP;
		}

		if(isServer)
			netplayer.CmdServerSurrender();
		else if(isClient)
			netplayer.CmdClientSurrender();

//		if(isServer)
//			NetworkManager.StopHost();
//		else if(isClient)
//			NetworkManager.StopClient();
	}

	public void SetDoll()
	{
		SetDollTime += 1;
		Debug.Log("NowSetDolllllllllllllllllllllllllllll"+SetDollTime);
		if(SetDollTime <2)
			return;
		Debug.Log("SetDOll");
		if(isServer)
		{
			LeftHead.sprite = ServerHead;
			LeftEye.sprite = ServerEye;
			LeftMouse.sprite = ServerMouse;
			LeftCloth.sprite = ServerCloth;
			LeftPants.sprite = ServerPants;
			LeftUpperArm.sprite = ServerUpperArm;
			SelfHead.sprite = ServerHead;
			SelfEye.sprite = ServerEye;
			SelfMouse.sprite = ServerMouse;
			SelfCloth.sprite = ServerCloth;
			SelfPants.sprite = ServerPants;
			SelfUpperArm.sprite = ServerUpperArm;
			
			RightHead.sprite = ClientHead;
			RightEye.sprite = ClientEye;
			RightMouse.sprite = ClientMouse;
			RightCloth.sprite = ClientCloth;
			RightPants.sprite = ClientPants;
			RightUpperArm.sprite = ClientUpperArm;
			OppositeHead.sprite = ClientHead;
			OppositeEye.sprite = ClientEye;
			OppositeMouse.sprite = ClientMouse;
			OppositeCloth.sprite = ClientCloth;
			OppositePants.sprite = ClientPants;
			OppositeUpperArm.sprite = ClientUpperArm;

			
			HeadImg.sprite = ClientHead;
			EyesImg.sprite = ClientEye;
			MouseImg.sprite = ClientMouse;
			ClothImg.sprite = ClientCloth;
			PantsImg.sprite = ClientPants;
			UpperArmImg.sprite = ClientUpperArm;
			SetOppositeInfo(ClientID,ClientName);
		}
		else
		{
			Debug.Log("--------------------------------------------------Client GameManager");

			LeftHead.sprite = ClientHead;
			LeftEye.sprite = ClientEye;
			LeftMouse.sprite = ClientMouse;
			LeftCloth.sprite = ClientCloth;
			LeftPants.sprite = ClientPants;
			LeftUpperArm.sprite = ClientUpperArm;
			SelfHead.sprite = ClientHead;
			SelfEye.sprite = ClientEye;
			SelfMouse.sprite = ClientMouse;
			SelfCloth.sprite = ClientCloth;
			SelfPants.sprite = ClientPants;
			SelfUpperArm.sprite = ClientUpperArm;
			
			RightHead.sprite = ServerHead;
			RightEye.sprite = ServerEye;
			RightMouse.sprite = ServerMouse;
			RightCloth.sprite = ServerCloth;
			RightPants.sprite = ServerPants;
			RightUpperArm.sprite = ServerUpperArm;
			OppositeHead.sprite = ServerHead;
			OppositeEye.sprite = ServerEye;
			OppositeMouse.sprite = ServerMouse;
			OppositeCloth.sprite = ServerCloth;
			OppositePants.sprite = ServerPants;
			OppositeUpperArm.sprite = ServerUpperArm;

			HeadImg.sprite = ServerHead;
			EyesImg.sprite = ServerEye;
			MouseImg.sprite = ServerMouse;
			ClothImg.sprite = ServerCloth;
			PantsImg.sprite = ServerPants;
			UpperArmImg.sprite = ServerUpperArm;
			Debug.Log("Set Opposite Doll");
			SetOppositeInfo(ServerID,ServerName);
		}
		WaitPanel.SetActive(false);
		NotSetOver = false;
		Opening.SetActive(true);
	}

	class Info
	{
		public string WarriorSpriteName;
		public int WarriorLevel;
		public int WarriorGrade;
		public float FightWinTime;
		public float FightTotalTime;
		public int Score;
	}

	public async void SetOppositeInfo(int ID,string Name)
	{
		Debug.Log("1111111111111111111111111111111111111111111111111111111111");
		Debug.Log(ID);
		Debug.Log(Name);
		Debug.Log(isServer);
		Debug.Log("1111111111111111111111111111111111111111111111111111111111");
		Info OppositeInfo = await FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(ID.ToString()).GetValueAsync().ContinueWith( task => {
			DataSnapshot snapshot = task.Result;
			Info LocalInfo = new Info();

			LocalInfo.WarriorSpriteName = (string)snapshot.Child("WarriorImgName").Value;
			LocalInfo.WarriorLevel = int.Parse(snapshot.Child("WarriorLevel").GetRawJsonValue());
			LocalInfo.WarriorGrade = int.Parse(snapshot.Child("WarriorGrade").GetRawJsonValue());
			LocalInfo.FightWinTime = float.Parse(snapshot.Child("FightWinTime").GetRawJsonValue());
			LocalInfo.FightTotalTime = float.Parse(snapshot.Child("FightTotalTime").GetRawJsonValue());
			LocalInfo.Score = int.Parse(snapshot.Child("Score").GetRawJsonValue());

			return LocalInfo;
		});
		
		Debug.Log("2222222222222222222222222222");

		float WinRate = OppositeInfo.FightWinTime / OppositeInfo.FightTotalTime;
		if(WinRate.ToString() == "非數值")
			WinRate = 0;
		if(WinRate.ToString().Length > 5)
			WinRateText.text = ((WinRate*100).ToString()).Substring(0,5)+"%";
		else
			WinRateText.text = ((WinRate*100).ToString())+"%";

		ScoreText.text = OppositeInfo.Score.ToString();

		Debug.Log("333333333333333333333333333333333333333333333333333333333333333333");
		NameText.text = Name;
		WarriorImg.sprite = Resources.Load<Sprite>("WarriorHead/"+OppositeInfo.WarriorSpriteName);
		WarriorLevelText.text = "Lv：" + OppositeInfo.WarriorLevel.ToString();
		if(OppositeInfo.WarriorGrade == 2)
			WarriorStarImg.sprite = WarriorGrade2Star;
		SetWarriorBuff(OppositeInfo.WarriorSpriteName);

		bool hasSendInvite = await FirebaseDatabase.DefaultInstance.GetReference("users").Child(ID.ToString()).Child("InviteFriendPlayer").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot Snapshot = task.Result;
				foreach(DataSnapshot Child in Snapshot.Children)
				{
					if(int.Parse(Child.Child("PlayerId").GetRawJsonValue()) == (int)PlayerData.UserId)
					{
						Debug.Log("FInd");
						return true;
					}
				}
				Debug.Log("NotFind");
			}
			return false;
		});
		
		bool isFriend = await FirebaseDatabase.DefaultInstance.GetReference("users").Child(ID.ToString()).Child("Friend").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot Snapshot = task.Result;
				foreach(DataSnapshot Child in Snapshot.Children)
				{
					Debug.Log("---------------"+(string)Child.Child("FriendName").Value);
					Debug.Log(PlayerData.PlayerName);
					if((string)Child.Child("FriendName").Value == PlayerData.PlayerName)
					{
						Debug.Log("FInd Friend");
						return true;
					}
				}
				Debug.Log("NotFind");
			}
			return false;
		});

		Debug.Log("NameText::::::::::::::::::::::::::::::::::::"+NameText.text);

		if(NameText.text == "Player(Clone)")
		{
			StartCoroutine(ResetInfoName(true,NameText.text));

		//	InviteBlock.SetActive(true);
		//	NameText.text = "連線不佳，無法取得!";
		//	NameText.color = Color.red;
		}

		if(hasSendInvite)
			InviteBlock.SetActive(true);
		if(isFriend)
			InviteBlock.SetActive(true);
	}

	IEnumerator ResetInfoName(bool NotSetOver,string WrongName)
	{
		while(NotSetOver)
		{
			if(isServer)
				NameText.text = ClientName;
			else
				NameText.text = ServerName;

			if(NameText.text != WrongName)
			{
				NotSetOver = false;
				Debug.Log("Set Name Over");
			}
			else
				Debug.Log("Not Set Over");
			yield return new WaitForSeconds(1f);
		}
	}

	public void InvitedToBeFriend()
	{
		int ID = 0;
		if(isServer)
			ID = ClientID;
		else
			ID = ServerID;

		DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("users");
		FirebaseDatabase.DefaultInstance.GetReference("users").Child(ID.ToString()).Child("InviteFriendPlayer").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot Snapshot = task.Result;
				mDatabaseRef.Child(ID.ToString()).Child("InviteFriendPlayer").Child(Snapshot.ChildrenCount.ToString()).Child("PlayerName")
				.SetValueAsync(PlayerData.PlayerName);
				Debug.Log("!!!@@!!@@!");
				mDatabaseRef.Child(ID.ToString()).Child("InviteFriendPlayer").Child(Snapshot.ChildrenCount.ToString()).Child("PlayerId")
				.SetValueAsync(PlayerData.UserId);
				mDatabaseRef.Child(ID.ToString()).Child("InviteFriendPlayer").Child(Snapshot.ChildrenCount.ToString()).Child("Seen")
				.SetValueAsync(0);
				Debug.Log("WDQDSADSA");
			}
		});
		
	}

	public void SetWarriorBuff(string WarriorImgName)
	{
		if(WarriorImgName.Contains("口水羊"))
		{
			WarriorBuffText.text = "我方島嶼的總生命值增加。";
			SpecialBuffText.text = "";
		}
		else if(WarriorImgName.Contains("厭世貓"))
		{
			WarriorBuffText.text = "我方怪物低機率迴避攻擊。";
			SpecialBuffText.text = "";
		}
		else if(WarriorImgName.Contains("暴走豬"))
		{
			WarriorBuffText.text = "我方怪物生命值增加。";
			SpecialBuffText.text = "";
		}
		else if(WarriorImgName.Contains("小熊貓"))
		{
			WarriorBuffText.text = "我方派出的小熊貓死後，附近九宮格內的塔變成雕像數秒。";
			SpecialBuffText.text = "";
		}
		else if(WarriorImgName.Contains("窩母雞"))
		{
			WarriorBuffText.text = "每隔一段時間多派出一隻窩母雞。";
			SpecialBuffText.text = "";
		}
		else if(WarriorImgName.Contains("糖果馬"))
		{
			WarriorBuffText.text = "我方怪物移動速度增加。";
			SpecialBuffText.text = "被擊中時機率性往前方傳送。";
		}
		else if(WarriorImgName.Contains("懶猴王"))
		{
			WarriorBuffText.text = "我方怪物機率性對敵方島嶼造成兩倍傷害。";
			SpecialBuffText.text = "死後分裂成兩隻小懶猴。";
		}
		else if(WarriorImgName.Contains("木頭鷹"))
		{
			WarriorBuffText.text = "敵方怪物抵達我方島嶼時，低機率迴避傷害。";
			SpecialBuffText.text = "不會被緩速、冰凍，且殘血時飛行速度增加。";
		}
		else if(WarriorImgName.Contains("黃綠龍"))
		{
			WarriorBuffText.text = "代表血量大於一半時，免疫波及灼傷；小於一半時，扣血時反擊，對前方三格內的怪造成即死傷害。";
			SpecialBuffText.text = "每隔一段時間，機率性隱形數秒。";
		}
		else if(WarriorImgName.Contains("大方龜"))
		{
			WarriorBuffText.text = "我方怪物部分傷害減免。";
			SpecialBuffText.text = "死後該格產生補血包，經過的怪物可回血。（數次後消失）";
		}
	}

	public void SetName(int Direction,int WarriorIconNum,bool CanCake)
	{
		Debug.Log("direction:"+Direction + "  Cancake:"+CanCake);
		if(WarriorIconNum == 0)
		{
			if(!CanCake)
			{
				if(Direction == 0)
					ServerDontOpenIcon = true;
				else if(Direction == 1)
					ClientDontOpenIcon = true;
			}
			else
			{
				if(Direction == 0)
					ServerCakePos1 = true;
				else if(Direction == 1)
					ClientCakePos1 = true;
			}
		}
		else if(CanCake)
		{
			if(Direction == 0)
				ServerTwoIcon = true;
			else if(Direction == 1)
				ClientTwoIcon = true;
		}

		SetNameTime += 1;
		Debug.Log("SETINFO!!!!!!!!!!"+SetNameTime);
		if(SetNameTime < 2)
			return;

		LeftIcon.gameObject.SetActive(true);
		RightIcon.gameObject.SetActive(true);
		if(isServer)//Server
		{
			if(ServerDontOpenIcon)
				LeftIcon.gameObject.SetActive(false);
			if(ClientDontOpenIcon)
				RightIcon.gameObject.SetActive(false);
			LeftIcon.sprite = ServerIcon;
			RightIcon.sprite = ClientIcon;
			if(ServerTwoIcon)
			{
				LeftIcon2.sprite = Cake;
				LeftIcon2.gameObject.SetActive(true);
			}
			if(ClientTwoIcon)
			{
				RightIcon2.sprite = Cake;
				RightIcon2.gameObject.SetActive(true);
			}
			LeftName.text = ServerName;
			RightName.text = ClientName;
			NameText.text = ClientName;
			if(HostPlayer == null)	
			{
				HostPlayer = GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<Player>();
				Debug.Log("Catch!!!! HostPlayer NULL");
			}
			HostPlayer.LivesBar = LeftBar;

			if(ClientPlayer == null)	
			{
				ClientPlayer = GameObject.FindGameObjectsWithTag("ClientHealth")[0].GetComponent<Player>();
				Debug.Log("Catch!!!! ClientPlayer NULL");
			}
			ClientPlayer.LivesBar = RightBar;
			HostPlayer.liveText = LeftLiveText;
			ClientPlayer.liveText = RightLiveText;
			if(ServerCakePos1)
			{
				LeftIcon.sprite = Cake;
				LeftIcon.gameObject.SetActive(true);
			}
			if(ClientCakePos1)
			{
				RightIcon.sprite = Cake;
				RightIcon.gameObject.SetActive(true);
			}
			OpenAnimationDownName.text = ServerName;
			OpenAnimationUpperName.text = ClientName;
			OpenAnimationDownPet.sprite = ServerPet;
			OpenAnimationUpperPet.sprite = ClientPet;
		}
		else
		{
			if(ServerDontOpenIcon)
				RightIcon.gameObject.SetActive(false);
			if(ClientDontOpenIcon)
				LeftIcon.gameObject.SetActive(false);
			LeftIcon.sprite = ClientIcon;
			RightIcon.sprite = ServerIcon;
			if(ServerTwoIcon)
			{
				RightIcon2.sprite = Cake;
				RightIcon2.gameObject.SetActive(true);
			}
			if(ClientTwoIcon)
			{
				LeftIcon2.sprite = Cake;
				LeftIcon2.gameObject.SetActive(true);
			}
			if(HostPlayer == null)	
			{
				HostPlayer = GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<Player>();
				Debug.Log("Catch!!!! HostPlayer NULL");
			}
			if(ClientPlayer == null)	
			{
				ClientPlayer = GameObject.FindGameObjectsWithTag("ClientHealth")[0].GetComponent<Player>();
				Debug.Log("Catch!!!! ClientPlayer NULL");
			}
			LeftName.text = ClientName;
			RightName.text = ServerName;
			NameText.text = ServerName;
			ClientPlayer.LivesBar = LeftBar;
			HostPlayer.LivesBar = RightBar;
			ClientPlayer.liveText = LeftLiveText;
			HostPlayer.liveText = RightLiveText;
			if(ServerCakePos1)
			{
				RightIcon.sprite = Cake;
				RightIcon.gameObject.SetActive(true);
			}
			if(ClientCakePos1)
			{
				LeftIcon.sprite = Cake;
				LeftIcon.gameObject.SetActive(true);
			}
			OpenAnimationDownName.text = ClientName;
			OpenAnimationUpperName.text = ServerName;
			OpenAnimationDownPet.sprite = ClientPet;
			OpenAnimationUpperPet.sprite = ServerPet;
		}
	}

	public void DefineWinLose(Player PlayerLose)
	{
		if(isServer & !hasAuthority)
		{
			Debug.Log("Clone PLayer");
			return;
		}
		
		if(PlayerLose.gameObject.GetComponent<NetworkIdentity>().netId == HostPlayer.gameObject.GetComponent<NetworkIdentity>().netId)
		{
			Debug.Log("Host Lost");
			if(isServer)
			{
				Debug.Log("ISserver HOSt LOSE");
				Lose();
			}
			if(!isServer)
			{
				Debug.Log("ISServer Client Win");
				Win();
			}
		}
		else
		{
			Debug.Log("Client Lose");
			if(isServer)
				Win();
			if(!isServer)
				Lose();
		}

		DatabaseReference reference2 = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString());
		reference2.Child("UseCake").SetValueAsync(0);
		if(Meterial.MineProgress<5)
			reference2.Child("CanMine").SetValueAsync(Meterial.MineProgress+1);
	}

	public void DefineTimeWinLose()
	{

		if(HostPlayer.Lives <ClientPlayer.Lives)
		{
			if(isServer)
				Lose();
			if(!isServer)
				Win();
		}
		if(HostPlayer.Lives > ClientPlayer.Lives)
		{
			if(isServer)
				Win();
			if(!isServer)
				Lose();
		}
		if(HostPlayer.Lives == ClientPlayer.Lives)
		{
			Debug.Log("Draw");
			Draw();
		}
		
		DatabaseReference reference2 = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString());
		reference2.Child("UseCake").SetValueAsync(0);
		if(Meterial.MineProgress<5)
			reference2.Child("CanMine").SetValueAsync(Meterial.MineProgress+1);
	}

	void Draw()
	{
		if(GameEnd)
		{
			Debug.Log("You has Lose");
			return;
		}
		GameEnd = true;
		WaitPanel.SetActive(false);

		GetMeterial("Draw");

		Stop();

		GameDrawUI.SetActive(true);
		StartCoroutine(WaitAudioPlay(DrawPanel.GetComponent<AudioSource>()));
		DrawPanel.GetComponent<Animator>().SetBool("ShowUp",true);
		BGM.Stop();
		StartCoroutine(WaitTwoSeconds(DrawPanel.GetComponent<Animator>(),DrawAwardAnimate));
		ShadManager.SetActive(true);
		this.GetComponent<CountDown>().StopCount();
		Meterial.CanCake = false;

	}

	public void Lose()
	{
		
		if(GameEnd)
		{
			Debug.Log("You has Lose");
			return;
		}
		GameEnd = true;
		WaitPanel.SetActive(false);
		
		GetMeterial("Lose");

		Stop();
		GameLoseUI.SetActive(true);
		StartCoroutine(WaitAudioPlay(LosePanel.GetComponent<AudioSource>()));
		LosePanel.GetComponent<Animator>().SetBool("ShowUp",true);
		BGM.Stop();
		StartCoroutine(WaitTwoSeconds(LosePanel.GetComponent<Animator>(),LoseAwardAimate));
		ShadManager.SetActive(true);
		this.GetComponent<CountDown>().StopCount();

		if(PetWarrior.WarriorName != null)
		{
			DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("Pet").Child(PetWarrior.WarriorName).Child("PetLife");
			reference.SetValueAsync((PetWarrior.WarriorLife)-1);
		}

		Meterial.CanCake = false;

	}

	void RecordWinRate()
	{
		
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString());
		reference.Child("FightWinTime").SetValueAsync(WinFightTime+1);
	}

	IEnumerator WaitTwoSeconds(Animator TextAnimate,Animator AwardAnimate)
	{
		yield return new WaitForSeconds(7f);
		TextAnimate.SetBool("Disapeaar",true);
		AwardAnimate.SetBool("AwardShow",true);
		yield return new WaitForSeconds(1f);
		ReWard.Play();
	}

	IEnumerator WaitAudioPlay(AudioSource Audio)
	{
		yield return new WaitForSeconds(1f);
		Audio.Play();
	}

	public bool GameOver()
	{
		return GameEnd;
	}

	
	public void Win()
	{
		if(GameEnd)
		{
			Debug.Log("You has Win!");
			return;
		}
		GameEnd = true;
		WaitPanel.SetActive(false);

		GetMeterial("Win");
		
		Stop();
		GameWinUI.SetActive(true);
		StartCoroutine(WaitAudioPlay(WinPanel.GetComponent<AudioSource>()));
		WinPanel.GetComponent<Animator>().SetBool("ShowUp",true);
		BGM.Stop();
		StartCoroutine(WaitTwoSeconds(WinPanel.GetComponent<Animator>(),WindAwardAnimate));
		ShadManager.SetActive(true);
		this.GetComponent<CountDown>().StopCount();

		RecordWinRate();
	
	}

	void GetMeterial(string Status)
	{
		MeterialObtain Award = new MeterialObtain();
		TextAndImg UIs = new TextAndImg();
		Debug.Log(Status);
		if(Status == "Win")
		{
			Award = WinAward;
			UIs = WinUI;
		}
		else if(Status == "Draw")
		{
			Award = DrawAward;
			UIs = DrawUI;
		}
		else if(Status == "Lose")
		{
			Award = LoseAward;
			UIs = LoseUI;
		}
		Debug.Log(Award);
		Debug.Log(UIs);

		float Rate = Random.Range(0,100);
		
		UIs.BackBee.text = Award.backbee.ToString();
		Meterial.AddMaterial("Back",Award.backbee);
		if(Rate <= 25)
		{
			UIs.Text1.text = Award.wood.ToString();
			UIs.Text2.text = Award.rock.ToString();
			UIs.Text3.text = Award.earth.ToString();
			UIs.Img1.sprite = WoodSprite;
			UIs.Img1.rectTransform.sizeDelta = new Vector2(UIs.Img1.rectTransform.sizeDelta.x/2,UIs.Img1.rectTransform.sizeDelta.y/2);
			UIs.Img2.sprite = RockSprite;
			UIs.Img3.sprite = EarthSprite;
			Meterial.AddMaterial("Wood",Award.wood);
			Meterial.AddMaterial("Rock",Award.rock);
			Meterial.AddMaterial("Earth",Award.earth);
		}
		else if(Rate <= 50)
		{
			UIs.Text1.text = Award.wood.ToString();
			UIs.Text2.text = Award.rock.ToString();
			UIs.Text3.text = Award.cotton.ToString();
			UIs.Img1.sprite = WoodSprite;
			UIs.Img1.rectTransform.sizeDelta = new Vector2(UIs.Img1.rectTransform.sizeDelta.x/2,UIs.Img1.rectTransform.sizeDelta.y/2);
			UIs.Img2.sprite = RockSprite;
			UIs.Img3.sprite = CottonSprite;
			Meterial.AddMaterial("Wood",Award.wood);
			Meterial.AddMaterial("Rock",Award.rock);
			Meterial.AddMaterial("Cotton",Award.cotton);
		}
		else if(Rate <= 75)
		{
			UIs.Text1.text = Award.wood.ToString();
			UIs.Text2.text = Award.earth.ToString();
			UIs.Text3.text = Award.cotton.ToString();
			UIs.Img1.sprite = WoodSprite;
			UIs.Img1.rectTransform.sizeDelta = new Vector2(UIs.Img1.rectTransform.sizeDelta.x/2,UIs.Img1.rectTransform.sizeDelta.y/2);
			UIs.Img2.sprite = EarthSprite;
			UIs.Img3.sprite = CottonSprite;
			Meterial.AddMaterial("Wood",Award.wood);
			Meterial.AddMaterial("Earth",Award.earth);
			Meterial.AddMaterial("Cotton",Award.cotton);
		}
		else if(Rate <= 100)
		{
			UIs.Text1.text = Award.rock.ToString();
			UIs.Text2.text = Award.earth.ToString();
			UIs.Text3.text = Award.cotton.ToString();
			UIs.Img1.sprite = RockSprite;
			UIs.Img2.sprite = EarthSprite;
			UIs.Img3.sprite = CottonSprite;
			Meterial.AddMaterial("Rock",Award.rock);
			Meterial.AddMaterial("Earth",Award.earth);
			Meterial.AddMaterial("Cotton",Award.cotton);
		}


	}
	
	void Stop()
	{
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject enemy in enemys)
		{
			if(enemy.GetComponent<AIPath>()!= null)
				enemy.GetComponent<AIPath>().enabled = false;
			if(enemy.GetComponent<FlyMonster>()!= null)
				enemy.GetComponent<FlyMonster>().enabled = false;
		}

		GameObject[] Towers = GameObject.FindGameObjectsWithTag("turret");
		foreach(GameObject Tower in Towers)
		{
			Tower.GetComponent<Tower>().enabled = false;
		}

		GameObject[] UPTowers = GameObject.FindGameObjectsWithTag("UpgradedTurret");
		foreach(GameObject Tower in UPTowers)
		{
			Tower.GetComponent<Tower>().enabled = false;
		}
	}
}
