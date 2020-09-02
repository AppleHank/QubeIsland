using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Pathfinding;


public class NetPlayer : NetworkBehaviour {

	private NODE NowSelectNode;
	public NODE Buildnode;
	public GameManagerNew GameManager;
	public GameObject NameLeft;
	public GameObject NameRight;
	public bool CanAddCaptureRate;
	public float AddedCaptureRate;
	private Text CountDownText;
	public GameObject DoubleDamageEffectPrefab;
	public GameObject HurtEffectPrefab;
	public GameObject HurtPicturePrefab;

	[Header("Icon")]
	public Sprite HorseIcon;
	public Sprite TurtleIcon;
	public Sprite PigIcon;
	public Sprite LorisIcon;
	public Sprite CatIcon;
	public Sprite ChameleonIcon;
	private Sprite WarriorIcon;

	[Header("TurretData")]
	private GameObject TurretToBuild;
	public GameObject path;
	public GameObject Standardturret;
	public GameObject Fireturret;
	public GameObject Bloodturret;
	public GameObject Iceturret;
	public GameObject SnowPath;
	public GameObject Holyturret;
	public GameObject Frozeturret;
	public GameObject Moneyturret;
	public GameObject Lavaturret;
	public GameObject pathWithoutSound;

	[Header("UpdateData")]
	private GameObject TurretToUpgrade;
	public GameObject STUpgrade;
	public GameObject BTUpgrade;
	public GameObject FTUpgrade;
	public GameObject ITUpgrade;
	public GameObject HTUpgrade;
	public GameObject LVUpgrade;
	public GameObject FZUpgrade;


	[Header("EnemyData")]
	public GameObject DodgeText;
	private GameObject WavePointOBJ;
	public int Monsterinterval;
	private Vector3 LastEnemyPosition;
	private GameObject EnemyToSpawn;
	public GameObject Goat;
	public GameObject Cat;
	public GameObject BearCat;
	public GameObject Pig;
	public GameObject Chicken;
	public GameObject Horse;
	public GameObject Turtle;
	public GameObject Owl;
	public GameObject Loris;
	public GameObject Chameleon;
	public GameObject UpGoat;
	public GameObject UpCat;
	public GameObject UpPig;
	public GameObject UpBearCat;
	public GameObject UpChicken;
	public GameObject UpHorse;
	public GameObject UpLoris;
	public GameObject UpOwl;
	public GameObject UpChameleon;
	public GameObject UpTurtle;
	public float InvisibleTime;
	public float InvisibleRate;
	public float InvisibleInterval;
	private bool NowSound = true;
	private bool GoatSound = true;
	private bool CatSound = true;
	private bool BearCatSound = true;
	private bool PigSound = true;
	private bool ChickenSound = true;
	private bool HorseSound = true;
	private List<bool> SoundList = new List<bool>();
	public float JumpRate;
	public float Owl_SpeedUpHealthRate;
	public float Owl_AddedSpeed;
	public GameObject HorseAssistance;
	public GameObject BearCatAssistance;
	public GameObject TurtleAssistance;
	public GameObject SmallLoris;
	public GameObject UpSmallLorisFlag;
	public GameObject UpSmallLorisFire;
	private GameObject SmallLorisToSpawn;
	public GameObject ChameleonTongue;
	[Header("FrontSprite")]
	public Sprite FrontGoatImg;
	public Sprite FrontCatImg;
	public Sprite FrontPigImg;
	public Sprite FrontBearCatImg;
	public Sprite FrontChickenImg;
	public Sprite FrontHorseImg;
	public Sprite FrontTurtleImg;
	public Sprite FrontLorisImg;
	public Sprite FrontOwlImg;
	public Sprite FrontChameleonImg;
	public Sprite FrontUpGoatImg;
	public Sprite FrontUpCatImg;
	public Sprite FrontUpPigImg;
	public Sprite FrontUpBearCatImg;
	public Sprite FrontUpChickenImg;
	public Sprite FrontUpHorseImg;
	public Sprite FrontUpLorisImg;
	public Sprite FrontUpOwlImg;
	public Sprite FrontUpTurtleImg;
	public Sprite FrontUpChameleonImg;
	
	[Header("Sprite")]
	private Sprite WarriorImg;
	public Sprite GoatImg;
	public Sprite CatImg;
	public Sprite PigImg;
	public Sprite BearCatImg;
	public Sprite ChickenImg;
	public Sprite HorseImg;
	public Sprite TurtleImg;
	public Sprite LorisImg;
	public Sprite OwlImg;
	public Sprite ChameleonImg;
	public Sprite UpGoatImg;
	public Sprite UpCatImg;
	public Sprite UpPigImg;
	public Sprite UpBearCatImg;
	public Sprite UpChickenImg;
	public Sprite UpHorseImg;
	public Sprite UpLorisImg;
	public Sprite UpOwlImg;
	public Sprite UpTurtleImg;
	public Sprite UpChameleonImg;
	public Sprite ChameleonAngry;
	public Sprite ChameleonAngryTongue;
	public Sprite UpChameleonAngry;
	public Sprite UpChameleonAngryTongue;
	private Sprite WarriorSprite;


	[Header("PetBuff")]
	public float AddSpeed;
	public float DodgeRate;
	public float DoubleDamageRate;
	public float AddLife;
	public float IncreseDamageRate;
	public float GuardRate;
	private float InitialGuardRate = 100;

	[Header("Player")]
	public Sprite Cyn;
	public Sprite How;
	public Sprite Chen;
	public Sprite Fun;
	private Sprite PlayerImg;

	void Start()
	{
		NameLeft = GameObject.FindGameObjectWithTag("Tree");
		NameRight = GameObject.FindGameObjectWithTag("Board");
		StartCoroutine(wait());
	}
	
	IEnumerator wait()
	{
		while(GameObject.FindGameObjectsWithTag("Play").Length <2)
		{
			Debug.Log("Player Num:" + GameObject.FindGameObjectsWithTag("Play").Length);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.25f);
		if(isServer && isLocalPlayer)
			{
				Debug.Log("HostStart");
				GameObject[] Nodes = GameObject.FindGameObjectsWithTag("HostNode");
				foreach(GameObject Node in Nodes)
				{
					Debug.Log(Node);
					NODE n = Node.GetComponent<NODE>();
					n.NetPlayer = this;
					if(n.isStartNode == true && (n.turret == null))
					{
						Debug.Log("Start Build");
					//	build(Node,n.startground.prefab,n.GetBuildPosition());
						Cmdbuild(Node.GetComponent<NetworkIdentity>().netId,99,n.GetBuildPosition());
					}
				}
				GameObject[] SpawnManagers = GameObject.FindGameObjectsWithTag("HostSpawnManager");
				foreach(GameObject SManager in SpawnManagers)
				{
					SManager.GetComponent<enemyspawn>().NetPlayer = this;
//					SManager.GetComponent<enemyspawn>().WavePoint = GameObject.FindGameObjectsWithTag("WavePointHost")[0];
					SManager.GetComponent<enemyspawn>().LastEnemyPosition = SManager.GetComponent<enemyspawn>().WavePoint.transform.position;
				}
				GameObject HostHealthControl = GameObject.FindGameObjectsWithTag("HostHealth")[0];
				HostHealthControl.GetComponent<Player>().NetPlayer = this;
				Debug.Log("HostAssignFullFill");
				CmdAssignClientAuthority(HostHealthControl.GetComponent<NetworkIdentity>().netId);
//				GameObject HostPropManager = GameObject.FindGameObjectsWithTag("HostPropManager")[0];
//				HostPropManager.GetComponent<Prop>().NetPlayer = this;
//				HostPropManager.GetComponent<Prop>().player = HostHealthControl.GetComponent<Player>();
	//			WavePointOBJ= GameObject.FindGameObjectsWithTag("WavePointHost")[0];
			}
		
			else if(!isServer && isLocalPlayer)
			{
				Debug.Log("clientStart");
				GameObject[] Nodes = GameObject.FindGameObjectsWithTag("ClientNode");
				foreach(GameObject Node in Nodes)
				{
					NODE n = Node.GetComponent<NODE>();
					n.NetPlayer = this;
					if(n.isStartNode == true && (n.turret == null))
					{
						Debug.Log("Start Build");
					//	build(Node,n.startground.prefab,n.GetBuildPosition());
						Cmdbuild(Node.GetComponent<NetworkIdentity>().netId,99,n.GetBuildPosition());
					}
				}
				GameObject[] SpawnManagers = GameObject.FindGameObjectsWithTag("ClientSpawnManager");
				foreach(GameObject SManager in SpawnManagers)
				{
					Debug.Log("CLietSpawn");
					SManager.GetComponent<enemyspawn>().NetPlayer = this;
//					SManager.GetComponent<enemyspawn>().WavePoint = GameObject.FindGameObjectsWithTag("WavePointClient")[0];
					SManager.GetComponent<enemyspawn>().LastEnemyPosition = SManager.GetComponent<enemyspawn>().WavePoint.transform.position;
				}
				GameObject ClientHealthControl = GameObject.FindGameObjectsWithTag("ClientHealth")[0];
				ClientHealthControl.GetComponent<Player>().NetPlayer = this;
				Debug.Log("ClientAssignFullfill");
				CmdAssignClientAuthority(ClientHealthControl.GetComponent<NetworkIdentity>().netId);
//				GameObject ClientPropManager = GameObject.FindGameObjectsWithTag("ClientPropManager")[0];
//				ClientPropManager.GetComponent<Prop>().NetPlayer = this;
//				ClientPropManager.GetComponent<Prop>().player = ClientHealthControl.GetComponent<Player>();
		//		WavePointOBJ = GameObject.FindGameObjectsWithTag("WavePointClient")[0];
			}

			yield return new WaitForSeconds(.2f);
			Debug.Log(GameObject.FindGameObjectsWithTag("WavePointHost")[0]);
			if(isServer & isLocalPlayer)//Acesse From Server,Assign Server(Self,because isServer) PlayerPrefab
			{
				Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!_----------------------");
				WavePointOBJ= GameObject.FindGameObjectsWithTag("WavePointHost")[0];
			}
			if(isServer & !isLocalPlayer)//Acesse From Server,Assign Server PlayerPrefab
				WavePointOBJ = GameObject.FindGameObjectsWithTag("WavePointClient")[0];

			
			GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerNew>();
		
	}

	public void build(GameObject node,GameObject prefab,Vector3 position)
	{
		Debug.Log(prefab.name);
		int buildnum = 0;
		if(prefab.name == "path")
			buildnum = 0;
		else if(prefab.name == "Standardturret")
			buildnum = 1;
		else if(prefab.name == "Bloodturret")
			buildnum = 2;
		else if(prefab.name == "Fireturret")
			buildnum = 3;
		else if(prefab.name == "Iceturret")
			buildnum = 4;
		else if(prefab.name == "SnowPath")
			buildnum = 5;
		else if(prefab.name == "Holyturret")
			buildnum = 6;
		else if(prefab.name == "Frozeturret")
			buildnum = 7;
		else if(prefab.name == "Moneyturret")
			buildnum = 8;
		else if(prefab.name == "Lavaturret")
			buildnum = 9;
		Debug.Log(buildnum);
		Cmdbuild(node.GetComponent<NetworkIdentity>().netId,buildnum,position);
	}

	public void buildSnowPath(GameObject node,Vector3 position)
	{
		Cmdbuild(node.GetComponent<NetworkIdentity>().netId,5,position);
	}

	public void ReplaceSnowPath(GameObject node,Vector3 position)
	{
		Cmdbuild(node.GetComponent<NetworkIdentity>().netId,99,position);
	}

	[Command]
	void Cmdbuild(NetworkInstanceId nodeid,int buildnum,Vector3 position)
	{
		if(buildnum == 0)
			TurretToBuild = path;
		else if(buildnum == 1)
			TurretToBuild = Standardturret;
		else if(buildnum ==2)
			TurretToBuild = Bloodturret;
		else if(buildnum == 3)
			TurretToBuild = Fireturret;
		else if(buildnum == 4)
			TurretToBuild = Iceturret;
		else if(buildnum == 5)
			TurretToBuild = SnowPath;
		else if(buildnum == 6)
			TurretToBuild = Holyturret;
		else if(buildnum == 7)
			TurretToBuild = Frozeturret;
		else if(buildnum == 8)
			TurretToBuild = Moneyturret;
		else if(buildnum == 9)
			TurretToBuild = Lavaturret;
		else if(buildnum == 99)
			TurretToBuild = pathWithoutSound;

		Debug.Log(TurretToBuild);

		GameObject _turret = (GameObject) Instantiate(TurretToBuild,position,Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(_turret, connectionToClient);
//		NetworkServer.Spawn(_turret);
		Rpcsetturret(nodeid,_turret.GetComponent<NetworkIdentity>().netId);
	}

	[ClientRpc]
	void Rpcsetturret(NetworkInstanceId nodeid,NetworkInstanceId turretid)
	{
		ClientScene.FindLocalObject(nodeid).GetComponent<NODE>().SetTurret(ClientScene.FindLocalObject(turretid));
		GameObject Turret = ClientScene.FindLocalObject(turretid);
	//	if(Turret.name == "UpgradedBloodturret(Clone)")
	//	{
	//		Vector3 turretTemp = Turret.transform.position;
	//		turretTemp.y += 0.8f;
	//		Turret.transform.position = turretTemp;
	//	}	
		if(Turret.tag == "turret" | Turret.tag == "HolyTurret" | Turret.tag == "MoneyTurret" | Turret.tag == "UpgradedTurret")
		{		
			Vector3 temp = Turret.transform.position;
			temp.y -= 3.1f;
			Turret.transform.position = temp;
		}		
	}

	[Command]
	public void CmdUpgrade(NetworkInstanceId nodeid,int turretnum,Vector3 position)
	{
		if(turretnum == 1)
			TurretToUpgrade = STUpgrade;
		else if(turretnum == 2)
			TurretToUpgrade = BTUpgrade;
		else if(turretnum == 3)
			TurretToUpgrade = FTUpgrade;
		else if(turretnum == 4)
			TurretToUpgrade = ITUpgrade;
		else if(turretnum == 5)
			TurretToUpgrade = HTUpgrade;
		else if(turretnum == 6)
			TurretToUpgrade = LVUpgrade;
		else if(turretnum == 7)
			TurretToUpgrade = FZUpgrade;

		GameObject UpgradedTurret = Instantiate(TurretToUpgrade,position,Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(UpgradedTurret, connectionToClient);
		Rpcsetturret(nodeid,UpgradedTurret.GetComponent<NetworkIdentity>().netId);
	}

	[Command]
	public void Cmdsell(NetworkInstanceId sellTid,NetworkInstanceId nodid)
	{
		GameObject sellTobj = ClientScene.FindLocalObject(sellTid);
		NetworkServer.Destroy(sellTobj);
		Destroy(ClientScene.FindLocalObject(sellTid));
		if(sellTobj.tag == "path")
			RpcUpdateWaklability(nodid);
	}

	[ClientRpc]
	void RpcUpdateWaklability(NetworkInstanceId nodeid)
	{
		GameObject n = ClientScene.FindLocalObject(nodeid);
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
			// Safe to update graphs here
			var node = AstarPath.active.GetNearest(n.transform.position).node;
			node.Walkable = false;
		}));
	}

	[Command]
	public void Cmdspawn(int spawnnum,int OrderInLayer,bool CanDodge,bool CanSpeedUp,bool CanDoubleDamage,bool CanAddLife,bool CanAddCaptureRate,bool CanChangeTurret,bool CanGuard,bool CanAvoidExplode)
	{
		if(spawnnum == 1)
			EnemyToSpawn = Goat;
		else if(spawnnum == 2)
			EnemyToSpawn = Cat;
		else if(spawnnum == 3)
			EnemyToSpawn = BearCat;
		else if(spawnnum == 4)
			EnemyToSpawn = Pig;
		else if(spawnnum == 5)
			EnemyToSpawn = Chicken;
		else if(spawnnum == 6)
			EnemyToSpawn = Horse;
		else if(spawnnum == 7)
			EnemyToSpawn = Loris;
		else if(spawnnum == 8)
			EnemyToSpawn = Owl;
		else if(spawnnum == 9)
			EnemyToSpawn = Chameleon;
		else if(spawnnum == 10)
			EnemyToSpawn = Turtle;
		else if(spawnnum == 11)
			EnemyToSpawn = UpGoat;
		else if(spawnnum == 12)
			EnemyToSpawn = UpCat;
		else if(spawnnum == 13)
			EnemyToSpawn = UpBearCat;
		else if(spawnnum == 14)
			EnemyToSpawn = UpPig;
		else if(spawnnum == 15)
			EnemyToSpawn = UpChicken;
		else if(spawnnum == 16)
			EnemyToSpawn = UpHorse;
		else if(spawnnum == 17)
			EnemyToSpawn = UpLoris;
		else if(spawnnum == 18)
			EnemyToSpawn = UpOwl;
		else if(spawnnum == 19)
			EnemyToSpawn = UpChameleon;
		else if(spawnnum == 20)
			EnemyToSpawn = UpTurtle;
		UpdateLastEnemyPosition();
		Vector3 WavePoint = GetWavePosition();
		GameObject spawnInfo = (GameObject) Instantiate(EnemyToSpawn,WavePoint,Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(spawnInfo, connectionToClient);

		NetworkInstanceId MonsterId = spawnInfo.GetComponent<NetworkIdentity>().netId;
		if(CanDodge)
			RpcCanDodge(MonsterId);
		else if(CanSpeedUp)
			RpcCanSpeedUp(MonsterId);
		else if(CanDoubleDamage)
			RpcCanDoubleDamage(MonsterId);
		else if(CanAddLife)
			RpcCanAddLife(MonsterId);
		else if(CanAddCaptureRate)
			RpcCandAddCaptureRate(MonsterId);
		else if(CanChangeTurret)	
		{
			if(spawnnum == 3 || spawnnum == 13)
				RpcCanChangeTurret(MonsterId);
		}
		else if(CanGuard)
			RpcCanGuard();
		else if(CanAvoidExplode)
			RpcCanAvoidExplode(MonsterId);
		
		
		if(spawnInfo.name == "Chameleon(Clone)" | spawnInfo.name == "UpgradeChameleon(Clone)")
			StartCoroutine(Invisible(MonsterId));
		
		RpcSound(MonsterId,spawnnum);
		RpcAssignSortingOrder(MonsterId,OrderInLayer);
	}

	IEnumerator Invisible(NetworkInstanceId netId)
	{
		GameObject Monster = ClientScene.FindLocalObject(netId);
		while(Monster != null)
		{
			yield return new WaitForSeconds(InvisibleInterval);
			if(Monster.GetComponent<Chameleon>().GetInvisible())
				yield return new WaitForSeconds(InvisibleTime);
			float IRate = Random.Range(0,100);
			if(IRate <= InvisibleRate)
			{ 
				if(Monster == null)
					yield return null;
				Monster.GetComponent<Chameleon>().SetInvisible();
				Monster.GetComponent<SpriteRenderer>().color = new Color32(29,255,0,100);
				Monster.tag = "Tree";
				StartCoroutine(Visible(netId));
			}
		}	
	}

	IEnumerator Visible(NetworkInstanceId netId)
	{
		yield return new WaitForSeconds(InvisibleTime);
		GameObject Monster = ClientScene.FindLocalObject(netId);
		Monster.GetComponent<SpriteRenderer>().color = new Color32 (30,255,0,255);
		Monster.tag = "enemy";
		Monster.GetComponent<Chameleon>().SetInvisible();
	}
	
	[ClientRpc]
	void RpcCanAvoidExplode(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().AllowAvoidExplode();
	}


	[ClientRpc]
	void RpcCanGuard()
	{
		InitialGuardRate = GuardRate;
	}

	[ClientRpc]
	void RpcCanChangeTurret(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().AllowChangeTurret();
	}

	[ClientRpc]
	void RpcCanDodge(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().CanDodge = true;
	}
	[ClientRpc]
	void RpcCanSpeedUp(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().SpeedUp(AddSpeed);
	}

	[ClientRpc]
	void RpcCanDoubleDamage(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().DoubleDamage();
	}

	[ClientRpc]
	void RpcCanAddLife(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().AddLife(AddLife);
	}

	[ClientRpc]
	void RpcCandAddCaptureRate(NetworkInstanceId SpawnId)
	{
		ClientScene.FindLocalObject(SpawnId).GetComponent<enemy>().AddCaptureRate(AddedCaptureRate);
	}


	[ClientRpc]
	void RpcSound(NetworkInstanceId SpawnId,int SpawnNum) //Super Ugly Code
	{

		GameObject SpawnInfo = ClientScene.FindLocalObject(SpawnId);
		Debug.Log(!SpawnInfo.GetComponent<NetworkIdentity>().hasAuthority);
		Debug.Log(isServer);
		Debug.Log(NowSound);
		if(SpawnNum == 1)
			NowSound = GoatSound;
		else if(SpawnNum == 2)
			NowSound = CatSound;
		else if(SpawnNum == 3)
			NowSound = BearCatSound;
		else if(SpawnNum == 4)
			NowSound = PigSound;
		else if(SpawnNum == 5)
			NowSound = ChickenSound;
		else if(SpawnNum == 6)
			NowSound = HorseSound;
		if(isServer & !SpawnInfo.GetComponent<NetworkIdentity>().hasAuthority & NowSound)
		{
			Debug.Log("Server Sound");
			SpawnInfo.GetComponent<AudioSource>().Play();
			if(SpawnNum == 1)
				GoatSound = false;
			else if(SpawnNum == 2)
				CatSound = false;
			else if(SpawnNum == 3)
				BearCatSound = false;
			else if(SpawnNum == 4)
				PigSound = false;
			else if(SpawnNum == 5)
				ChickenSound = false;
			else if(SpawnNum == 6)
				HorseSound = false;
				
			StartCoroutine(ReSetSound(SpawnNum));
		}
		else if(!isServer & !SpawnInfo.GetComponent<NetworkIdentity>().hasAuthority & NowSound)
		{
			Debug.Log("Client Sound");
			SpawnInfo.GetComponent<AudioSource>().Play();
			if(SpawnNum == 1)
				GoatSound = false;
			else if(SpawnNum == 2)
				CatSound = false;
			else if(SpawnNum == 3)
				BearCatSound = false;
			else if(SpawnNum == 4)
				PigSound = false;
			else if(SpawnNum == 5)
				ChickenSound = false;
			else if(SpawnNum == 6)
				HorseSound = false;
			
			StartCoroutine(ReSetSound(SpawnNum));
		}
	}

	IEnumerator ReSetSound(int Num)
	{
		yield return new WaitForSeconds(2f);
		if(Num == 1)
				GoatSound = true;
			else if(Num == 2)
				CatSound = true;
			else if(Num == 3)
				BearCatSound = true;
			else if(Num == 4)
				PigSound = true;
			else if(Num == 5)
				ChickenSound = true;
			else if(Num == 6)
				HorseSound = true;
	}

	[ClientRpc]
	void RpcAssignSortingOrder(NetworkInstanceId MonsterId,int OrderInLayer)
	{
		ClientScene.FindLocalObject(MonsterId).GetComponent<SpriteRenderer>().sortingOrder = OrderInLayer;
	}

	void UpdateLastEnemyPosition()
	{
		GameObject[] Enemys = GameObject.FindGameObjectsWithTag("enemy");
		Vector3 HostTemp = new Vector3 (0,0,0);
		foreach (GameObject Enemy in Enemys)
		{
			if(WavePointOBJ.transform.position.x-1<Enemy.transform.position.x & Enemy.transform.position.x<=WavePointOBJ.transform.position.x)
			{
				if(Enemy.transform.position.y > HostTemp.y)
					HostTemp = Enemy.transform.position;
			}
		}
		if(HostTemp.y<WavePointOBJ.transform.position.y-Monsterinterval)
			HostTemp = WavePointOBJ.transform.position;
		LastEnemyPosition = HostTemp;
	}

	public Vector3 GetWavePosition()
	{
		Vector3 temp = LastEnemyPosition;
		temp.y += Monsterinterval;
		LastEnemyPosition = temp;
		return LastEnemyPosition;
	}

	[Command]
	public void CmdDecidePlayerDodge(NetworkInstanceId PlayerId)
	{
		float Rate = Random.Range(0f,100f);
		Player player = ClientScene.FindLocalObject(PlayerId).GetComponent<Player>();
		if(Rate < DodgeRate)
			RpcPlayerDodge(PlayerId);
		else
			CmdGetDamage(PlayerId,false);
	}

	[ClientRpc]
	void RpcPlayerDodge(NetworkInstanceId PlayerId)
	{
		Player player = ClientScene.FindLocalObject(PlayerId).GetComponent<Player>();
		player.Doll.GetComponent<Animator>().SetBool("Dodge",true);
		StartCoroutine(StopDodgeAnimate(PlayerId));
	}

	IEnumerator StopDodgeAnimate(NetworkInstanceId PlayerId)
	{
		yield return new WaitForSeconds(0.17f);
		Player player = ClientScene.FindLocalObject(PlayerId).GetComponent<Player>();
		player.Doll.GetComponent<Animator>().SetBool("Dodge",false);
	}


	[Command]
	public void CmdGetDamage(NetworkInstanceId healthid,bool isUpgrade)
	{
		RpcGetDamage(healthid,isUpgrade);
	}

	[ClientRpc]
	void RpcGetDamage(NetworkInstanceId healthid,bool isUpgrade)
	{
		Player HealthControl = ClientScene.FindLocalObject(healthid).GetComponent<Player>();
		HealthControl.Lives -= 1;
		CmdUpdateLive(healthid);
		GameObject HurtEffect = Instantiate(HurtEffectPrefab,HealthControl.transform.position,Quaternion.identity);
//		GameObject HurtPicture = Instantiate(HurtPicturePrefab,HealthControl.transform.position,Quaternion.identity);
		Destroy(HurtEffect,2.5f);
//		Destroy(HurtPicture,1f);
		HealthControl.InsHurtPicture();

		if(HealthControl.Lives <= 10)
		{
			if( WarriorImg == ChameleonImg | WarriorImg == UpChameleonImg)
			{
				if(isUpgrade)
					HealthControl.Doll.GetComponent<SpriteRenderer>().sprite = UpChameleonAngryTongue;
				else
					HealthControl.Doll.GetComponent<SpriteRenderer>().sprite = ChameleonAngryTongue;
				Debug.Log("TONGUE");
				StartCoroutine(CloseMouth(HealthControl,isUpgrade));
				CmdTongue(healthid);
			}
		}
		if(HealthControl.Lives <= 0)
		{
			GameManager.DefineWinLose(HealthControl);
		}
	}

	[Command]
	public void CmdServerSurrender()
	{
		RpcServerSurrender();
	}

	[ClientRpc]
	void RpcServerSurrender()
	{
		Debug.Log("Start Server Surrender");
		if(isServer)
			GameManager.Lose();
		else if(isClient)
			GameManager.Win();
	}

	[Command]
	public void CmdClientSurrender()
	{
		RpcClientSurrender();
	}

	[ClientRpc]
	void RpcClientSurrender()
	{
		Debug.Log("Start Client Surrender");
		if(isServer)
			GameManager.Win();
		else if(isClient)
			GameManager.Lose();
	}

	IEnumerator CloseMouth(Player HealthControl,bool isUpgrade)
	{
		yield return new WaitForSeconds(2f);
		if(isUpgrade)
			HealthControl.Doll.GetComponent<SpriteRenderer>().sprite = UpChameleonAngry;
		else
			HealthControl.Doll.GetComponent<SpriteRenderer>().sprite = ChameleonAngry;
	}

	[Command]
	void CmdTongue(NetworkInstanceId healthid)
	{
		Player HealthControl = ClientScene.FindLocalObject(healthid).GetComponent<Player>();
		Vector3 temp = HealthControl.transform.position;
		temp.y += 3;
		GameObject Tongue = Instantiate(ChameleonTongue,temp,Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(Tongue, connectionToClient);
		Destroy(Tongue,2f);
	}

	[Command]
	public void CmdUpdateLive(NetworkInstanceId healthid)
	{
		RpcUpdateLive(healthid);
	}

	[ClientRpc]
	public void RpcUpdateLive(NetworkInstanceId healthid)
	{
		Player HealthControl = ClientScene.FindLocalObject(healthid).GetComponent<Player>();
		HealthControl.liveText.text = HealthControl.Lives.ToString();
		HealthControl.LivesBar.fillAmount = HealthControl.Lives / HealthControl.startLives;
		HealthControl.Clone.UpdateLive(HealthControl.Lives,HealthControl.startLives);
	}

	[Command]
	public void CmdDestroy(NetworkInstanceId objid)
	{
		GameObject obj = ClientScene.FindLocalObject(objid);
		NetworkServer.Destroy(obj);
		Destroy(obj);
	}

	[Command]
	public void CmdAssignClientAuthority(NetworkInstanceId PlayerID)
	{
		ClientScene.FindLocalObject(PlayerID).GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
	}

	[Command]
	public void CmdChangeWarrior(NetworkInstanceId WarriorId,int WarriorNum,bool isUpgrade)
	{
		RpcChangeWarrior(WarriorId,WarriorNum,isUpgrade);
	}

	[ClientRpc]
	public void RpcChangeWarrior(NetworkInstanceId WarriorId,int WarriorNum,bool isUpgrade)
	{
		Debug.Log("CHangeWarrior");
		Player Warrior = ClientScene.FindLocalObject(WarriorId).GetComponent<Player>();
		if(WarriorNum == 1)
		{
			Debug.Log("GoatWarrior");
			if(isUpgrade)
				WarriorImg = UpGoatImg;
			else
				WarriorImg = GoatImg;
			Warrior.startLives += 3;
			Warrior.Lives = Warrior.startLives;
			CmdUpdateLive(WarriorId);
		}
		else if(WarriorNum == 2)
		{
			if(isUpgrade)
				WarriorImg = UpCatImg;
			else
			WarriorImg = CatImg;
		}
		else if(WarriorNum == 3)
		{
			if(isUpgrade)
				WarriorImg = UpPigImg;
			else
				WarriorImg = PigImg;
		}
		else if(WarriorNum == 4)
		{
			if(isUpgrade)
				WarriorImg = UpBearCatImg;
			else
				WarriorImg = BearCatImg;
		}
		else if(WarriorNum == 5)
		{
			if(isUpgrade)
				WarriorImg = UpChickenImg;
			else
				WarriorImg = ChickenImg;
		}
		else if(WarriorNum == 6)
		{
			if(isUpgrade)
				WarriorImg = UpHorseImg;
			else
				WarriorImg = HorseImg;
		}
		else if(WarriorNum == 7)
		{
			if(isUpgrade)
				WarriorImg = UpLorisImg;
			else
				WarriorImg = LorisImg;
		}
		else if(WarriorNum == 8)
		{
			if(isUpgrade)
				WarriorImg = UpOwlImg;
			else
				WarriorImg = OwlImg;
		}
		else if(WarriorNum == 9)
		{
			if(isUpgrade)
				WarriorImg = UpChameleonImg;
			else
				WarriorImg = ChameleonImg;
		}
		else if(WarriorNum == 10)
		{
			if(isUpgrade)
				WarriorImg = UpTurtleImg;
			else
				WarriorImg = TurtleImg;
		}

		GameObject WarriorDoll = Warrior.GetComponent<Player>().Doll;
		Debug.Log(WarriorDoll);
		WarriorDoll.GetComponent<SpriteRenderer>().sprite = WarriorImg;
		Debug.Log(WarriorDoll.GetComponent<SpriteRenderer>().sprite);
		Warrior.Clone.ReSetImg(WarriorImg); 
	}
	
	[Command]
	public void CmdSetPlayerImg(NetworkInstanceId PlayerId,int CharacterNum)
	{
		RpcSetPlayerImg(PlayerId,CharacterNum);
	}

	[ClientRpc]
	void RpcSetPlayerImg(NetworkInstanceId PlayerId,int CharacterNum)
	{
		if(CharacterNum == 1)
			PlayerImg = Cyn;
		else if(CharacterNum == 2)
			PlayerImg = How;
		else if(CharacterNum == 3)
			PlayerImg = Chen;
		else if(CharacterNum == 4)
			PlayerImg = Fun;

		Player player = ClientScene.FindLocalObject(PlayerId).GetComponent<Player>();
		player.Doll.GetComponent<SpriteRenderer>().sprite = PlayerImg;
		player.Clone.Doll.GetComponent<SpriteRenderer>().sprite = PlayerImg;
		Debug.Log(PlayerImg);
	}


	[Command]
	public void CmdMonsterGetDamage(NetworkInstanceId MonsterId,float amount,bool IncreseDamage)
	{
		RpcMonsterGetDamage(MonsterId,amount,false);
	}

	[ClientRpc]
	void RpcMonsterGetDamage(NetworkInstanceId MonsterId,float amount,bool IncreseDamage)
	{
		if(ClientScene.FindLocalObject(MonsterId) == null)
		{
			Debug.Log("RETURN!!!!!");
			return;
		}
		enemy Monster = ClientScene.FindLocalObject(MonsterId).GetComponent<enemy>();
		if(Monster.name == "Horse(Clone)" | Monster.name == "UpgradeHorse(Clone)")
		{
			float JRate = Random.Range(0f,100f);
			Debug.Log(JRate);
			Debug.Log(JumpRate);
			if(JRate <= JumpRate)
			{
				GameObject HAssistance = Instantiate(HorseAssistance,Monster.transform.position,Quaternion.identity);
				NetworkServer.SpawnWithClientAuthority(HAssistance, connectionToClient);
				RpcHorseJump(MonsterId,HAssistance.GetComponent<NetworkIdentity>().netId);
		//		HAssistance.GetComponent<HorseAssistance>().Jump(ClientScene.FindLocalObject(MonsterId));
		//		Debug.Log(ClientScene.FindLocalObject(MonsterId));
			}
		}
		Monster.healthNow -= amount*IncreseDamageRate*(InitialGuardRate/100);
		Monster.healthBar.fillAmount = Monster.healthNow / Monster.health;

		Monster.GetDamageEffect();
		
		if(Monster.name == "Owl(Clone)" | Monster.name == "UpOwl(Clone)")
		{
			
			if(Monster.healthNow / Monster.health <= Owl_SpeedUpHealthRate/100)
			{
				if(Monster.GetComponent<enemy>().isSpeedUp())
				{
					Debug.Log("!!!!");
				}
				else
				{
					Monster.GetComponent<enemy>().SetSpeedUp();
					RpcOwlSpeedUp(MonsterId);
				}
			}
		}

		if(Monster.healthNow <=0 )
		{
			if(Monster.AlreadyDead())
				return;
			Monster.SetDead();
			Debug.Log(Monster);
			if(Monster.CanChangeT())
				CmdSpawnBearCatAssistance(Monster.gameObject.transform.position);
			else if(Monster.name == "Turtle(Clone)" | Monster.name == "UpTurtle(Clone)")
				CmdSpawnTurtleAssistance(Monster.gameObject.transform.position);
			else if(Monster.name == "Loris(Clone)")
				CmdSpawnLittleLoris(Monster.GetComponent<NetworkIdentity>().netId,Monster.gameObject.transform.position,false);
			else if(Monster.name == "UpgradeLoris(Clone)")
				CmdSpawnLittleLoris(Monster.GetComponent<NetworkIdentity>().netId,Monster.gameObject.transform.position,true);
			
			Monster.ReadyToDestroy();
		}
	}

	[ClientRpc]
	void RpcHorseJump(NetworkInstanceId MonsterId,NetworkInstanceId AssistanceId)
	{
		StartCoroutine(HorseJump(MonsterId,AssistanceId));
	}

	IEnumerator HorseJump(NetworkInstanceId MonsterId,NetworkInstanceId AssistanceId)
	{
		yield return new WaitForSeconds(.1f);
		Debug.Log("Jump");
		GameObject H = ClientScene.FindLocalObject(MonsterId);
		GameObject HA= ClientScene.FindLocalObject(AssistanceId);

		H.transform.position = HA.transform.position;
	}

	[ClientRpc]
	void RpcOwlSpeedUp(NetworkInstanceId MonsterId)
	{
		Debug.Log(ClientScene.FindLocalObject(MonsterId).GetComponent<FlyMonster>().speed);
		ClientScene.FindLocalObject(MonsterId).GetComponent<FlyMonster>().speed += Owl_AddedSpeed;
		ClientScene.FindLocalObject(MonsterId).GetComponent<enemy>().SetAlreadySpeedUp();
		Debug.Log(ClientScene.FindLocalObject(MonsterId).GetComponent<FlyMonster>().speed);
	}

	[Command]
	void CmdSpawnBearCatAssistance(Vector3 position)
	{
		GameObject spawnInfo = (GameObject) Instantiate(BearCatAssistance,position,Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(spawnInfo, connectionToClient);
		float Rate = Random.Range(0f,100f);
		RpcBearCat(spawnInfo.GetComponent<NetworkIdentity>().netId,Rate);
	}

	[ClientRpc]
	void RpcBearCat(NetworkInstanceId MonsterId,float Rate)
	{
		Debug.Log("CLIENT");
		GameObject BCAssistance = ClientScene.FindLocalObject(MonsterId);
		BCAssistance.GetComponent<BearCatAssistance>().SetOnline();
		BCAssistance.GetComponent<BearCatAssistance>().DecideChange(Rate);
	}

	[Command]
	void CmdSpawnLittleLoris(NetworkInstanceId MonsterId,Vector3 position,bool isUpgrade)
	{
		Debug.Log("SPAWNSMALL");
		float Vx = ClientScene.FindLocalObject(MonsterId).GetComponent<AIPath>().velocity.x;
		float Vy = ClientScene.FindLocalObject(MonsterId).GetComponent<AIPath>().velocity.y;
		for(int i=0;i<2;i++)
		{
			if(isUpgrade)
			{
				if(i == 0)
					SmallLorisToSpawn = UpSmallLorisFlag;
				else
					SmallLorisToSpawn = UpSmallLorisFire;
			}
			else
				SmallLorisToSpawn = SmallLoris;
			GameObject SmallOne = Instantiate(SmallLorisToSpawn,position,Quaternion.identity);	
			NetworkServer.SpawnWithClientAuthority(SmallOne, connectionToClient);
			Vector3 temp = SmallOne.transform.position;
			Debug.Log(Mathf.Abs(Vx));
			Debug.Log(Mathf.Abs(Vy));
			if(Mathf.Abs(Vx) > Mathf.Abs(Vy))
			{
				if(i==1)
				{
					temp.x = temp.x-3.5f;
					SmallOne.transform.position = temp;
				}
			}
			else
			{
				if(i==1)
				{
					temp.y = temp.y-3.5f;
					SmallOne.transform.position = temp;
				}
			}
			
			Debug.Log(SmallOne.transform.position);
			if(ClientScene.FindLocalObject(MonsterId).GetComponent<enemy>().isOffline)
				SmallOne.GetComponent<enemy>().isOffline = true;
		}
	}

	[Command]
	void CmdSpawnTurtleAssistance(Vector3 position)
	{
		GameObject spawnInfo = (GameObject) Instantiate(TurtleAssistance,position,Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(spawnInfo, connectionToClient);
		RpcGetTurtlePath(spawnInfo.GetComponent<NetworkIdentity>().netId,position);
		Debug.Log("TurtleCMd");
	}

	[ClientRpc]
	void RpcGetTurtlePath(NetworkInstanceId netId,Vector3 position)
	{
		GameObject Portion = ClientScene.FindLocalObject(netId);
		Collider[] colliders = Physics.OverlapSphere(Portion.transform.position, 5);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestPath = null;
		foreach (Collider collider in colliders){
			Debug.Log(collider);
			Debug.Log(collider.tag);
			if(collider.tag == "path")
			{	
				float distanceToPath = Vector3.Distance(Portion.transform.position,collider.transform.position);
				if (distanceToPath < shortestDistance)
				{
					shortestDistance = distanceToPath;
					nearestPath = collider.gameObject;
				}
			}
		}
		Debug.Log(nearestPath);

	//	nearestPath.GetComponent<SpriteRenderer>().color = new Color32 (255,255,0,255);
		Portion.GetComponent<TurtlePortion>().SetPath(nearestPath);
		Debug.Log("SetPath");
	}

	[Command]
	void CmdHorseJump(NetworkInstanceId MonsterId,Vector3 position)
	{
		
		
	}

	[Command]
	public void CmdDecideMonsterDodge(NetworkInstanceId MonsterId,float amount,bool IncreseDamage)
	{
		float Rate = Random.Range(0f,100f);
		enemy Monster = ClientScene.FindLocalObject(MonsterId).GetComponent<enemy>();
		if(Rate <= Monster.DodgeRate)
		{
			Debug.Log("Dodge!");
			GameObject Dodgetext = Instantiate(DodgeText,Monster.transform.position,Quaternion.identity);
			NetworkServer.Spawn(Dodgetext);
			RpcDodgetext(MonsterId,Dodgetext.GetComponent<NetworkIdentity>().netId);
			Destroy(Dodgetext,0.5f);
			return;
		}
		else
			CmdMonsterGetDamage(MonsterId,amount,IncreseDamage);
	}

	[ClientRpc]
	void RpcDodgetext(NetworkInstanceId MonsterId,NetworkInstanceId DodgeTextId)
	{
		ClientScene.FindLocalObject(DodgeTextId).transform.parent = ClientScene.FindLocalObject(MonsterId).gameObject.transform;
	}

	[Command]
	public void CmdDecideDoubleDamage(NetworkInstanceId PlayerId)
	{
		float Rate = Random.Range(0f,100f);
		Player player = ClientScene.FindLocalObject(PlayerId).GetComponent<Player>();
		if(Rate <= DoubleDamageRate)
		{
			RpcDoubleDamage(PlayerId);
		}
		else
			CmdGetDamage(PlayerId,false);
	}

	[ClientRpc]
	void RpcDoubleDamage(NetworkInstanceId playerId)
	{
		Player player = ClientScene.FindLocalObject(playerId).GetComponent<Player>();
		player.Lives -= 2;
		GameObject DoubleDamageEffect = Instantiate(DoubleDamageEffectPrefab,player.transform.position,Quaternion.identity);
		Destroy(DoubleDamageEffect,1.5f);
		CmdUpdateLive(playerId);
		if(player.Lives <= 0)
		{
			GameManager.DefineWinLose(player);
		}
	}

	[Command]
	public void CmdOpenAnimate(NetworkInstanceId PlayerId,bool isUpgrade)
	{
		Debug.Log("CMDOPEN"); 
		RpcOpenAnimate(PlayerId,isUpgrade);
	}

	[ClientRpc]
	void RpcOpenAnimate(NetworkInstanceId PlayerId,bool isUpgrade)
	{
		Debug.Log("RPCOPEN");
		Animator DollAnima = ClientScene.FindLocalObject(PlayerId).GetComponent<Player>().Doll.GetComponent<Animator>();
		DollAnima.enabled = true;
		if(isUpgrade)
			DollAnima.SetBool("Upgrade",true);

	}

	[Command]
	public void CmdSetName(int Direction,string name)
	{
		RpcSetName(Direction,name);
	}

	[ClientRpc]
	void RpcSetName(int Direction,string Name)
	{
		if(Direction == 0)
			NameRight.GetComponent<Text>().text = Name;
		if(Direction == 1)
			NameLeft.GetComponent<Text>().text = Name;
	}

	[Command]
	public void CmdSetDoll(string Name,int Direction,string HeadName,string EyeName,string MouseName,string ClothName,string PantsName,string UpperArmName,int ID)
	{
		RpcSetDoll(Name,Direction,HeadName,EyeName,MouseName,ClothName,PantsName,UpperArmName,ID);
	}

	[ClientRpc]
	public void RpcSetDoll(string Name,int Direction,string HeadName,string EyeName,string MouseName,string ClothName,string PantsName,string UpperArmName,int ID)
	{
		if(Direction == 0)//server
		{
			GameManager.ServerHead = Resources.Load<Sprite>("Doll/Head/"+HeadName);
			GameManager.ServerEye = Resources.Load<Sprite>("Doll/Eye/"+EyeName);
			GameManager.ServerMouse = Resources.Load<Sprite>("Doll/Mouse/"+MouseName);
			GameManager.ServerCloth = Resources.Load<Sprite>("Doll/Cloth/"+ClothName);
			GameManager.ServerPants = Resources.Load<Sprite>("Doll/Pants/"+PantsName);
			GameManager.ServerUpperArm = Resources.Load<Sprite>("Doll/Walk/"+UpperArmName);
			GameManager.ServerID = ID;
			GameManager.ServerName = name;

		}
		else
		{
			GameManager.ClientHead = Resources.Load<Sprite>("Doll/Head/"+HeadName);
			GameManager.ClientEye = Resources.Load<Sprite>("Doll/Eye/"+EyeName);
			GameManager.ClientMouse = Resources.Load<Sprite>("Doll/Mouse/"+MouseName);
			GameManager.ClientCloth = Resources.Load<Sprite>("Doll/Cloth/"+ClothName);
			GameManager.ClientPants = Resources.Load<Sprite>("Doll/Pants/"+PantsName);
			GameManager.ClientUpperArm = Resources.Load<Sprite>("Doll/Walk/"+UpperArmName);
			GameManager.ClientID = ID;
			GameManager.ClientName = name;
		}
		Debug.Log("In NetPlayer SetDoll");
		GameManager.SetDoll();
	}

	[Command]
	public void CmdAssingName(int PlayerStatus,string name,int StatusIconNum,bool CanCake,int WarriorNum,bool isUpgrade)
	{
		RpcAssignName(PlayerStatus,name,StatusIconNum,CanCake,WarriorNum,isUpgrade);
	}

	[ClientRpc]
	void RpcAssignName(int PlayerStatus,string name,int StatusIconNum,bool CanCake,int WarriorNum,bool isUpgrade)
	{
		if(StatusIconNum == 1)
			WarriorIcon = CatIcon;
		else if(StatusIconNum == 2)
			WarriorIcon = PigIcon;
		else if(StatusIconNum == 3)
			WarriorIcon = HorseIcon;
		else if(StatusIconNum == 4)
			WarriorIcon = LorisIcon;
		else if(StatusIconNum == 5)
			WarriorIcon = ChameleonIcon;
		else if(StatusIconNum == 6)
			WarriorIcon = TurtleIcon;

		if(WarriorNum == 1)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpGoatImg;
            else
                WarriorSprite = FrontGoatImg;
        }
		else if(WarriorNum == 2)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpCatImg;
            else
                WarriorSprite = FrontCatImg;
        }
		else if(WarriorNum == 3)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpBearCatImg;
            else
                WarriorSprite = FrontBearCatImg;
        }
		else if(WarriorNum == 4)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpPigImg;
            else
                WarriorSprite = FrontPigImg;
        }
		else if(WarriorNum == 5)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpChickenImg;
            else
                WarriorSprite = FrontChickenImg;
        }
		else if(WarriorNum == 6)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpHorseImg;
            else
                WarriorSprite = FrontHorseImg;
        }
		else if(WarriorNum == 7)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpLorisImg;
            else
                WarriorSprite = FrontLorisImg;
        }
		else if(WarriorNum == 8)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpOwlImg;
            else
                WarriorSprite = FrontOwlImg;
        }
		else if(WarriorNum == 9)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpChameleonImg;
            else
                WarriorSprite = FrontChameleonImg;
        }
		else if(WarriorNum == 10)
        {
            if(isUpgrade)
			    WarriorSprite = FrontUpTurtleImg;
            else
                WarriorSprite = FrontTurtleImg;
        }

		if(PlayerStatus == 0)//server
		{
			GameManager.ServerName = name;
			GameManager.ServerIcon = WarriorIcon;
			GameManager.ServerPet = WarriorSprite;
		}
		else
		{
			GameManager.ClientName = name;
			GameManager.ClientIcon = WarriorIcon;
			GameManager.ClientPet = WarriorSprite;
		}

		GameManager.SetName(PlayerStatus,StatusIconNum,CanCake);
	}

	[Command]
	public void CmdCake(int Direction)
	{
		RpcCake(Direction);
	}

	[ClientRpc]
	void RpcCake(int Direction)
	{
		enemyspawn SpawnManager = GameObject.FindObjectOfType<enemyspawn>();
/* 		if(Direction == 0)
		{
			SpawnManager = GameObject.FindGameObjectsWithTag("HostSpawnManager")[0];
		}
		else if(Direction == 1)
		{
			SpawnManager = GameObject.FindGameObjectsWithTag("ClientSpawnManager")[0];
		}*/

		if(SpawnManager != null)
		{
			Debug.Log(SpawnManager.tag);
			SpawnManager.AddCaptureRate();
		}
	}

	[Command]
	public void CmdCountTime(int Minute,int Second)
	{
		RpcCountTime(Minute,Second);
	}

	[ClientRpc]
	void RpcCountTime(int Minute,int Second)
	{
		GameManager.SetGameStart();
		if(CountDownText == null)
			CountDownText = GameObject.FindObjectOfType<CountDown>().CountDownText;

		if(Second < 10)
			CountDownText.text = Minute.ToString() + ":0" + Second.ToString();
		else
			CountDownText.text = Minute.ToString() + ":" + Second.ToString();

		if(Minute == 0 & Second == 0)
		{
			GameManager.DefineTimeWinLose();
		}
		
		
	}

}
