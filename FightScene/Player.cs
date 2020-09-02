using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	private int WarriorIconNum = 0;

	public CountDown CountDownManager;
	public enemyspawn SpawnManager;

	public Text liveText;
	public Image LivesBar;

	public static int Money;
	public int startMoney;

	public static int AddedMoney = 50;
	private float initialAddTime;
	public Image ProgressImg;

	[SyncVar]
	public float Lives ;
	public float startLives = 20;

	public ClonePlayer Clone;
	public NetPlayer NetPlayer;
	public float Addtime;

	public GameObject Doll;
	private bool CanFly;

	private float LastTimeLife;
	public Text Name;
//	public GameObject WaitPanel;
	private int WarriorNum;
	public GameObject HurtPicturePrefab;
	public Text WaitText;

	public void GetDamage()
	{
		if(NetPlayer == null)
			return;

		if(CanFly)
			NetPlayer.CmdDecidePlayerDodge(this.GetComponent<NetworkIdentity>().netId);
		else
		{
			Debug.Log(NetPlayer);
			Debug.Log(gameObject.GetComponent<NetworkIdentity>().netId);
			NetPlayer.CmdGetDamage(gameObject.GetComponent<NetworkIdentity>().netId,PetWarrior.isUpgrade);
		}
	}

	public void InsHurtPicture()
	{
		if(this.tag == "HostHealth")
		{
			if(isServer)
			{
				GameObject HurtObj = Instantiate(HurtPicturePrefab,new Vector3(0,0,0),Quaternion.identity);
				HurtObj.transform.parent = GameObject.FindObjectOfType<Camera>().transform;
				HurtObj.transform.localPosition = new Vector3(0,0,1);
				Destroy(HurtObj,1f);
			}
		}
		else if(this.tag == "ClientHealth")
		{
			if(isClient)
			{				
				GameObject HurtObj = Instantiate(HurtPicturePrefab,new Vector3(0,0,0),Quaternion.identity);
				HurtObj.transform.parent = GameObject.Find("Main Camera").transform;
				HurtObj.transform.localPosition = new Vector3(0,0,1);
				Destroy(HurtObj,1f);
			}
		}
	}
	
	void Start()
	{	
		Money = startMoney;
		Lives = startLives;
		AddedMoney = 50;
        
	//	LobbyManager LM = GameObject.FindObjectsOfType<LobbyManager>();
     //   GameObject player = (GameObject)Instantiate(LM.playerPrefab, Vector3.zero, Quaternion.identity);
     //   player.GetComponent<Player>().color = Color.red;
     //   NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		liveText.text = Lives.ToString() + "/" + startLives.ToString();
		Clone.SetLive(Lives,startLives);
		if(isServer & this.tag =="HostHealth")
		{
			InvokeRepeating("CheckLossLife",0,0.2f);
			InvokeRepeating("WaitOtherPlayer",0f,.5f);
			
		}
		if(!isServer & this.tag =="ClientHealth")
		{
			InvokeRepeating("CheckLossLife",0,0.2f);
			InvokeRepeating("WaitOtherPlayer",0f,.5f);
		}
	}

	void CheckLossLife()
	{
		if(Lives == LastTimeLife-1 | Lives == LastTimeLife-2)
			this.GetComponent<AudioSource>().Play();
		LastTimeLife = Lives;
	}

	void WaitOtherPlayer()
	{
		NetPlayer[] NPs = FindObjectsOfType<NetPlayer>();
		if(NPs.Length <2)
		{
			Debug.Log(NPs.Length);
		}
		else
		{	
			StartCoroutine(WaitForAssignAuthority());
			CancelInvoke("WaitOtherPlayer");
		}
	}


	IEnumerator WaitForAssignAuthority()
	{		
		

		WaitText.text = "建立同步";
		yield return new WaitForSeconds(5f);
		
		if(isServer && this.tag =="HostHealth")
		{
			Debug.Log(this.name);
			Debug.Log("HOSTADDSTART");
			initialAddTime = Time.time;
			InvokeRepeating("AutoAdd",0.1f,.1f);

		}
		if(!isServer && this.tag =="ClientHealth")
		{
			Debug.Log(this.name);
			Debug.Log("ClientADDSTART");
			initialAddTime = Time.time;
			InvokeRepeating("AutoAdd",0.1f,.1f);
		}
		Debug.Log("startPet");

		if(isServer)
			if(this.tag == "HostHealth")
			{
				StartCoroutine(StartCountDown());
				DecideWarrior();
			}
		if(!isServer)
			if(this.tag == "ClientHealth")
			{
		//		CountDownManager.SetNetPlayer(NetPlayer);
				DecideWarrior();
			}

		if(PlayerData.PlayerName == null)
			PlayerData.PlayerName = "我從Island進來";

		
		if(isServer & this.tag =="HostHealth")	
		{
			Debug.Log(Meterial.CanCake);
			Debug.Log("-------------------SetDoll-----------------");
			Debug.Log(PlayerData.HairStyle.Color[PlayerData.HairColor].name);
			Debug.Log(Resources.Load<Sprite>("Doll/Head/"+PlayerData.HairStyle.Color[PlayerData.HairColor].name));
			NetPlayer.CmdSetDoll(PlayerData.PlayerName,0,PlayerData.HairStyle.Color[PlayerData.HairColor].name,PlayerData.EyesStyle.Color[PlayerData.EyesColor].name,PlayerData.MouseStyle.Color[0].name,PlayerData.ClothStyle.Color[PlayerData.ClothColor].name,PlayerData.PantsStyle.Color[PlayerData.PantsColor].name,PlayerData.UpperArmStand.name,(int)PlayerData.UserId);
			NetPlayer.CmdAssingName(0,PlayerData.PlayerName.ToString(),WarriorIconNum,Meterial.CanCake,WarriorNum,PetWarrior.isUpgrade);
			if(Meterial.CanCake)
				NetPlayer.CmdCake(0);
		}
	//		NetPlayer.CmdSetName(0,PlayerData.PlayerName.ToString());
		if(!isServer & this.tag =="ClientHealth")
		{
			Debug.Log("-------------------SetDoll-----------------");
			NetPlayer.CmdSetDoll(PlayerData.PlayerName,1,PlayerData.HairStyle.Color[PlayerData.HairColor].name,PlayerData.EyesStyle.Color[PlayerData.EyesColor].name,PlayerData.MouseStyle.Color[0].name,PlayerData.ClothStyle.Color[PlayerData.ClothColor].name,PlayerData.PantsStyle.Color[PlayerData.PantsColor].name,PlayerData.UpperArmStand.name,(int)PlayerData.UserId);
			NetPlayer.CmdAssingName(1,PlayerData.PlayerName.ToString(),WarriorIconNum,Meterial.CanCake,WarriorNum,PetWarrior.isUpgrade);
			if(Meterial.CanCake)
				NetPlayer.CmdCake(1);
		}
	//		NetPlayer.CmdSetName(1,PlayerData.PlayerName.ToString());

	}

	IEnumerator StartCountDown()
	{
		yield return new WaitForSeconds(4f);
				CountDownManager.SetNetPlayer(NetPlayer);
	}

	public void UpdateLive () 
	{
		NetPlayer.CmdUpdateLive(gameObject.GetComponent<NetworkIdentity>().netId);
	}

	void AutoAdd()
	{
		float NowProgressTime = Time.time - initialAddTime;
		ProgressImg.fillAmount = NowProgressTime / Addtime;
		if(NowProgressTime >= Addtime)
		{
			Money += AddedMoney;
			initialAddTime = Time.time;
		}

	}

	IEnumerator Check()
	{
		while(liveText.text == "20/20")
		{
			NetPlayer.CmdUpdateLive(this.GetComponent<NetworkIdentity>().netId);
			Debug.Log("Check!!!!!!!!!!!!!!!!!!!!!!");
			yield return new WaitForSeconds(1f);

		}
	}


	void DecideWarrior()
	{
		if(hasAuthority)
		{
			WarriorNum = 0;
			if(PetWarrior.GoatWarrior)
			{
				WarriorNum = 1;
				if(liveText.text == "20/20")
				{
					StartCoroutine(Check());
				}
			}
			else if(PetWarrior.CatWarrior)
			{
				WarriorNum = 2;
				SpawnManager.AllowDodge();
				WarriorIconNum = 1;
			}
			else if(PetWarrior.PigWarrior)
			{
				WarriorNum = 3;
				SpawnManager.AllowAddLife();
				WarriorIconNum = 2;
			}
			else if(PetWarrior.BearCatWarrior)
			{
				WarriorNum = 4;
				SpawnManager.AllowBearCat();
			}
			else if(PetWarrior.ChickenWarrior)
			{
				WarriorNum = 5;
				SpawnManager.AllowChicken();
		//		CanFly = true;
		//		NetPlayer.CmdOpenAnimate(this.GetComponent<NetworkIdentity>().netId);	
		//		for owl
			}
			else if(PetWarrior.HorseWarrior)
			{
				SpawnManager.AllowSpeedUp();
				WarriorNum = 6;
				WarriorIconNum = 3;
			}
			else if(PetWarrior.LorisWarrior)
			{
				WarriorNum = 7;
				SpawnManager.AllowDoubleDamage();
				WarriorIconNum = 4;
			}
			else if(PetWarrior.OwlWarrior)
			{
				WarriorNum = 8;
				CanFly = true;
				NetPlayer.CmdOpenAnimate(this.GetComponent<NetworkIdentity>().netId,PetWarrior.isUpgrade);	
			}
			else if(PetWarrior.ChameleonWarrior)
			{
				WarriorNum = 9;
				SpawnManager.AllowAvoidExplode();
				WarriorIconNum = 5;
			}
			else if(PetWarrior.TurtleWarrior)
			{
				WarriorNum = 10;
				SpawnManager.AllowGuard();
				WarriorIconNum = 6;
			}

			if(WarriorNum !=0)
				NetPlayer.CmdChangeWarrior(this.GetComponent<NetworkIdentity>().netId,WarriorNum,PetWarrior.isUpgrade);
			else
				NetPlayer.CmdSetPlayerImg(this.GetComponent<NetworkIdentity>().netId,PlayerData.CharacterNum);
		}	
	}
}
