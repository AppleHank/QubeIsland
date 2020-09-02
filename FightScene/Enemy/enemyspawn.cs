using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class enemyspawn : NetworkBehaviour {

	public GameObject WavePoint;
	private MonsterBlueprint MonsterToSpawn;
	[Header("Normal")]
	public MonsterBlueprint CatBlueprint;
	public MonsterBlueprint GoatBlueprint;
	public MonsterBlueprint BearCatBlueprint;
	public MonsterBlueprint ChickenBlueprint;
	public MonsterBlueprint PigBlueprint;
	public MonsterBlueprint HorseBlueprint;
	public MonsterBlueprint LorisBlueprint;
	public MonsterBlueprint OwlBlueprint;
	public MonsterBlueprint ChameleonBlueprint;
	public MonsterBlueprint TurtleBlueprint;
	[Header("Upgrade")]
	public MonsterBlueprint UpgradeCat;
	public MonsterBlueprint UpgradeGoat;
	public MonsterBlueprint UpgradeBearCat;
	public MonsterBlueprint UpgradeChicken;
	public MonsterBlueprint UpgradePig;
	public MonsterBlueprint UpgradeHorse;
	public MonsterBlueprint UpgradeLoris;
	public MonsterBlueprint UpgradeOwl;
	public MonsterBlueprint UpgradeChameleon;
	public MonsterBlueprint UpgradeTurtle;

	[Header("MonsterImg")]
	public Image GoatImg;
	public Image CatImg;
	public Image PigImg;
	public Image BearCatImg;
	public Image ChickenImg;
	public Image HorseImg;
	public Image LorisImg;
	public Image OwlImg;
	public Image ChameleonImg;
	public Image TurtleImg;

	[Header("UpgradeImg")]
	public Sprite UpgradeGoatSprite;
	public Sprite UpgradeCatSprite;
	public Sprite UpgradePigSprite;
	public Sprite UpgradeBearCatSprite;
	public Sprite UpgradeChickenSprite;
	public Sprite UpgradeHorseSprite;
	public Sprite UpgradeLorisSprite;
	public Sprite UpgradeOwlSprite;
	public Sprite UpgradeChameleonSprite;
	public Sprite UpgradeTurtleSprite;

	public Vector3 LastEnemyPosition;
	public NetPlayer NetPlayer;
	public int Monsterinterval;
	private int OrderInLayer;
	public bool CanDodge;
	private bool CanSpeedUp;
	private bool CanDoubleDamage;
	private bool CanAddLife;
	private bool CanAddCaptureRate;
	private bool CanChangeTurret;
	private bool CanGuard;
	private bool CanAvoidExplode;
	public float AddedRate;


//	private GameObject spawnInfo;

	void Start()
	{
		BearCatBlueprint.CostText.text = "$" + BearCatBlueprint.cost.ToString();
		CatBlueprint.CostText.text = "$" + CatBlueprint.cost.ToString();
		GoatBlueprint.CostText.text = "$" + GoatBlueprint.cost.ToString();
		PigBlueprint.CostText.text = "$" + PigBlueprint.cost.ToString();
		ChickenBlueprint.CostText.text = "$" + ChickenBlueprint.cost.ToString();

		if(isServer)
			this.tag = "HostSpawnManager";
		else
			this.tag = "ClientSpawnManager";
		OrderInLayer = 999;
		InvokeRepeating("UpdateButtonV0",0,0.2f);	

		if(Unlock.Horse)
		{
			HorseBlueprint.Disbutton.SetActive(false);
			HorseBlueprint.CostText.text = "$" + HorseBlueprint.cost.ToString();
		}
		if(Unlock.Loris)
		{
			LorisBlueprint.Disbutton.SetActive(false);
			LorisBlueprint.CostText.text = "$" + LorisBlueprint.cost.ToString();
		}
		if(Unlock.Owl)
		{
			OwlBlueprint.Disbutton.SetActive(false);
			OwlBlueprint.CostText.text = "$" + OwlBlueprint.cost.ToString();
		}
		if(Unlock.Chameleon)
		{
			ChameleonBlueprint.Disbutton.SetActive(false);
			ChameleonBlueprint.CostText.text = "$" + ChameleonBlueprint.cost.ToString();
		}
		if(Unlock.Turtle)
		{
			TurtleBlueprint.Disbutton.SetActive(false);
			TurtleBlueprint.CostText.text = "$" + TurtleBlueprint.cost.ToString();
		}

		if(Unlock.GoatUpgrade)
			GoatImg.sprite = UpgradeGoatSprite;
		if(Unlock.CatUpgrade)
			CatImg.sprite = UpgradeCatSprite;
		if(Unlock.PigUpgrade)
			PigImg.sprite = UpgradePigSprite;
		if(Unlock.BearCatUpgrade)
			BearCatImg.sprite = UpgradeBearCatSprite;
		if(Unlock.ChickenUpgrade)
			ChickenImg.sprite = UpgradeChickenSprite;
		if(Unlock.HorseUpgrade)
			HorseImg.sprite = UpgradeHorseSprite;
		if(Unlock.LorisUpgrade)
			LorisImg.sprite = UpgradeLorisSprite;
		if(Unlock.OwlUpgrade)
			OwlImg.sprite = UpgradeOwlSprite;
		if(Unlock.ChameleonUpgrade)
			ChameleonImg.sprite = UpgradeChameleonSprite;
		if(Unlock.TurtleUpgrade)
			TurtleImg.sprite = UpgradeTurtleSprite;
	}
	



	void UpdateButtonV0()
	{
		if(Player.Money < GoatBlueprint.cost)
			GoatBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			GoatBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < CatBlueprint.cost)
			CatBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			CatBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < PigBlueprint.cost)
			PigBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			PigBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < BearCatBlueprint.cost)
			BearCatBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			BearCatBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < ChickenBlueprint.cost)
			ChickenBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			ChickenBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < HorseBlueprint.cost)
			HorseBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			HorseBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < LorisBlueprint.cost)
			LorisBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			LorisBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < OwlBlueprint.cost)
			OwlBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			OwlBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < ChameleonBlueprint.cost)
			ChameleonBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			ChameleonBlueprint.Buttom.color = new Color32(255,255,255,255);
		if(Player.Money < TurtleBlueprint.cost)
			TurtleBlueprint.Buttom.color = new Color32(75,75,75,255);
		else
			TurtleBlueprint.Buttom.color = new Color32(255,255,255,255);
	}

	void UpdateButtom()
	{
		if(GoatBlueprint.cost>Player.Money)
		{
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			HorseBlueprint.Disbutton.SetActive(true);
			BearCatBlueprint.Disbutton.SetActive(true);
			ChickenBlueprint.Disbutton.SetActive(true);
			PigBlueprint.Disbutton.SetActive(true);
			CatBlueprint.Disbutton.SetActive(true);
			GoatBlueprint.Disbutton.SetActive(true);
	//		FireTurret.turretcost.text = "沒有足夠的金錢!";
	//		BloodTurret.turretcost.text = "沒有足夠的金錢!";
	//		standardTurret.turretcost.text = "沒有足夠的金錢!";
		}

		else if(CatBlueprint.cost>Player.Money)
		{
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			HorseBlueprint.Disbutton.SetActive(true);
			BearCatBlueprint.Disbutton.SetActive(true);
			ChickenBlueprint.Disbutton.SetActive(true);
			PigBlueprint.Disbutton.SetActive(true);
			CatBlueprint.Disbutton.SetActive(true);
	//		BloodTurret.turretcost.text = "沒有足夠的金錢!";
	//		FireTurret.turretcost.text = "沒有足夠的金錢!";
		}
		else if(PigBlueprint.cost>Player.Money)
		{
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			HorseBlueprint.Disbutton.SetActive(true);
			BearCatBlueprint.Disbutton.SetActive(true);
			ChickenBlueprint.Disbutton.SetActive(true);
			PigBlueprint.Disbutton.SetActive(true);
	//		FireTurret.turretcost.text = "沒有足夠的金錢!";
		}
		else if(ChickenBlueprint.cost>Player.Money)
		{			
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			HorseBlueprint.Disbutton.SetActive(true);
			BearCatBlueprint.Disbutton.SetActive(true);
			ChickenBlueprint.Disbutton.SetActive(true);
		}		
		else if(BearCatBlueprint.cost>Player.Money)
		{			
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			HorseBlueprint.Disbutton.SetActive(true);
			BearCatBlueprint.Disbutton.SetActive(true);
		}		
		else if(HorseBlueprint.cost>Player.Money)
		{			
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			HorseBlueprint.Disbutton.SetActive(true);
		}
		//Have money desision

		if(HorseBlueprint.cost<=Player.Money)
		{
			HorseBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			HorseBlueprint.Disbutton.SetActive(false);
			BearCatBlueprint.Disbutton.SetActive(false);
			ChickenBlueprint.Disbutton.SetActive(false);
			PigBlueprint.Disbutton.SetActive(false);
			CatBlueprint.Disbutton.SetActive(false);
			GoatBlueprint.Disbutton.SetActive(false);
	//		FireTurret.turretcost.text = '$' + FireTurret.cost.ToString();
	//		BloodTurret.turretcost.text = '$' + BloodTurret.cost.ToString();
	//		standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
		}
		else if(BearCatBlueprint.cost<=Player.Money)
		{
			BearCatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			BearCatBlueprint.Disbutton.SetActive(false);
			ChickenBlueprint.Disbutton.SetActive(false);
			PigBlueprint.Disbutton.SetActive(false);
			CatBlueprint.Disbutton.SetActive(false);
			GoatBlueprint.Disbutton.SetActive(false);
	//		BloodTurret.turretcost.text = '$' + BloodTurret.cost.ToString();
	//		standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
		}
		else if(ChickenBlueprint.cost<=Player.Money)
		{
			ChickenBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			ChickenBlueprint.Disbutton.SetActive(false);
			PigBlueprint.Disbutton.SetActive(false);
			CatBlueprint.Disbutton.SetActive(false);
			GoatBlueprint.Disbutton.SetActive(false);
	//		BloodTurret.turretcost.text = '$' + BloodTurret.cost.ToString();
	//		standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
		}
		else if(PigBlueprint.cost<=Player.Money)
		{
			PigBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			PigBlueprint.Disbutton.SetActive(false);
			CatBlueprint.Disbutton.SetActive(false);
			GoatBlueprint.Disbutton.SetActive(false);
	//		BloodTurret.turretcost.text = '$' + BloodTurret.cost.ToString();
	//		standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
		}
		else if(CatBlueprint.cost<=Player.Money)
		{
			CatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			CatBlueprint.Disbutton.SetActive(false);
			GoatBlueprint.Disbutton.SetActive(false);
	//		standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
		}		
		else if(GoatBlueprint.cost<=Player.Money)
		{
			GoatBlueprint.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			GoatBlueprint.Disbutton.SetActive(false);
	//		standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
		}
	}

	void UpdateLastEnemyPosition()
	{
		GameObject[] Enemys = GameObject.FindGameObjectsWithTag("enemy");
		Vector3 HostTemp = new Vector3 (0,0,0);
		foreach (GameObject Enemy in Enemys)
		{
			if(WavePoint.transform.position.x-1<Enemy.transform.position.x & Enemy.transform.position.x<=WavePoint.transform.position.x)
			{
				if(Enemy.transform.position.y > HostTemp.y)
					HostTemp = Enemy.transform.position;
			}
		}
		if(HostTemp.y<WavePoint.transform.position.y-Monsterinterval)
			HostTemp = WavePoint.transform.position;
		LastEnemyPosition = HostTemp;
		Spawn();
	}

	public Vector3 GetWavePosition()
	{
		Vector3 temp = LastEnemyPosition;
		temp.y += Monsterinterval;
		LastEnemyPosition = temp;
		return LastEnemyPosition;
	}

	public void assignmonster(int number)
	{

		if(number == 1)
		{
			if(Unlock.GoatUpgrade)
				MonsterToSpawn = UpgradeGoat;
			else
				MonsterToSpawn = GoatBlueprint;
		}
		else if(number == 2)
		{
			if(Unlock.CatUpgrade)
				MonsterToSpawn = UpgradeCat;
			else
				MonsterToSpawn = CatBlueprint;
		}
		else if(number == 3)
		{
			if(Unlock.BearCatUpgrade)
				MonsterToSpawn = UpgradeBearCat;
			else
				MonsterToSpawn = BearCatBlueprint;
		}
		else if(number == 4)
		{
			if(Unlock.PigUpgrade)
				MonsterToSpawn = UpgradePig;
			else
				MonsterToSpawn = PigBlueprint;	
		}
		else if(number == 5)
		{
			if(Unlock.ChickenUpgrade)
				MonsterToSpawn = UpgradeChicken;
			else
				MonsterToSpawn = ChickenBlueprint;
		}
		else if(number == 6)
		{
			if(Unlock.HorseUpgrade)
				MonsterToSpawn = UpgradeHorse;
			else
				MonsterToSpawn = HorseBlueprint;
		}
		else if(number == 7)
		{
			if(Unlock.LorisUpgrade)
				MonsterToSpawn = UpgradeLoris;
			else
				MonsterToSpawn = LorisBlueprint;
		}
		else if(number == 8)
		{
			if(Unlock.OwlUpgrade)
				MonsterToSpawn = UpgradeOwl;
			else
				MonsterToSpawn = OwlBlueprint;
		}
		else if(number == 9)
		{
			if(Unlock.ChameleonUpgrade)
				MonsterToSpawn = UpgradeChameleon;
			else
				MonsterToSpawn = ChameleonBlueprint;
		}
		else if(number == 10)
		{
			if(Unlock.TurtleUpgrade)
				MonsterToSpawn = UpgradeTurtle;
			else
				MonsterToSpawn = TurtleBlueprint;
		}

		if(Player.Money < MonsterToSpawn.cost){
			Debug.Log("Dont have money");
			return;
		}
		UpdateLastEnemyPosition();
		StartCoroutine(ColdDown(MonsterToSpawn));
	}
	IEnumerator ColdDown(MonsterBlueprint M)
	{
		
		M.Block.SetActive(true);
		float InitialTime = Time.time;
		float CanSpawnTime = InitialTime + M.ColdDownTime;
		bool StillColdDown = true;
		while(StillColdDown)
		{
			float Progrees = (CanSpawnTime - Time.time) / M.ColdDownTime;
			M.ColdDownImg.fillAmount = Progrees;
			if( Progrees <= 0)
			{
				M.Block.SetActive(false);
				StillColdDown = false;
			}
			yield return null;
		}
//		InvokeRepeating("RefreshColdDownImg",0f,0.01f);
	}
	void Spawn()
	{
		if(OrderInLayer == 0)
			OrderInLayer = 999;
		NetPlayer.Cmdspawn(MonsterToSpawn.MonsterNum,OrderInLayer,CanDodge,CanSpeedUp,CanDoubleDamage,CanAddLife,CanAddCaptureRate,CanChangeTurret,CanGuard,CanAvoidExplode);
		Player.Money -= MonsterToSpawn.cost;
		Player.AddedMoney += MonsterToSpawn.autoadd;
		Debug.Log("AddMoney"+Player.AddedMoney);
		OrderInLayer -= 1;
	
	}

	public void AllowDodge()
	{
		CanDodge = true;
	}

	public void AllowSpeedUp()
	{
		CanSpeedUp = true;
	}

	public void AllowDoubleDamage()
	{
		CanDoubleDamage = true;
	}

	public void AllowAddLife()
	{
		CanAddLife = true;
	}

	public void AddCaptureRate()
	{
		CanAddCaptureRate = true;
	}

	public bool ifCanAddRate()
	{
		return CanAddCaptureRate;
	}

	public void AllowChicken()
	{
		InvokeRepeating("Egg",60f,60f);
	}

	public void AllowBearCat()
	{
		CanChangeTurret = true;
	}

	public void AllowGuard()
	{
		CanGuard = true;
	}

	public void AllowAvoidExplode()
	{
		CanAvoidExplode = true;
	}

	void Egg()
	{
		if(GameObject.FindObjectOfType<GameManagerNew>().GameOver())
		{
			CancelInvoke();
			return;
		}
		MonsterToSpawn = ChickenBlueprint;
		if(OrderInLayer == 0)
			OrderInLayer = 999;
		NetPlayer.Cmdspawn(MonsterToSpawn.MonsterNum,OrderInLayer,CanDodge,CanSpeedUp,CanDoubleDamage,CanAddLife,CanAddCaptureRate,CanChangeTurret,CanGuard,CanAvoidExplode);
		OrderInLayer -= 1;
	}

}
