using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Pathfinding;

public class enemy : NetworkBehaviour {

	public float health = 150;
	public int Money;

	public float healthNow;

	public Image healthBar;
	public NetPlayer NetPlayer; 

	public bool isFly = false;
	public Transform Destination;
	public float InitailSpeed;
	public float ChangeTime;
	public GameObject CapturableStatus;
	public float CaptureRate;
	public bool isOffline;
	public GameObject HitEffectPrefab;
	
	private PetManager petmanager;
	private Vector3 prevLoc;
	public GameObject DodgeText;
	public Text MoneyTextInfo;
	public GameObject MoneyText;
	public List<Vector3> PreviousPosition = new List<Vector3>(); 
	private GameObject Ice;
	public GameObject FrozedEffectPrefab;
	public Sprite HitSprite;
	public Sprite DeadSprite;
	public Image HealthBarBack;
	public GameObject WindEffectPrefab;

	[Header("Setting")]
	public float IceSize;
	public bool CanDodge;
	public float DodgeRate;
	private float AddSpeed;
	private bool CanSpeedUp;
	private bool CanDoubleDamage;
	private bool CanChangeTurret;
	private bool CanAvoidExplode;
	public float JumpRate;
	public float SpeedUpHealthRate;
	public float OwlSpeed;
	public GameObject HorseAssistance;
	public bool isOwl;
	private bool FrozedNow;


	private bool IncreseDamage;
	private float OffLineIncreseDamageRate;
	private float GuardRate = 100;
	private bool OwlSpeedUp;
	private bool AlreadSpeedUp;
	private GameObject FrozedEffect;
	private bool isdead;
	private SpriteRenderer enemysprite;
	private int ColdTime = 0;
	private List<GameObject> Ices = new List<GameObject>();

	void Start ()
	{
		enemysprite = this.GetComponent<SpriteRenderer>();
		if(isFly)
		{
			Destination = FindClosestPlayer();
		}

		GameObject[] NetPlayers = GameObject.FindGameObjectsWithTag("Play");

		if(!isOffline)
		{
			foreach(GameObject NP in NetPlayers)
			{
					if(!isServer)
					{
						if(NP.GetComponent<NetworkIdentity>().isLocalPlayer)
							NetPlayer = NP.GetComponent<NetPlayer>();
					}
					if(isServer)
					{
						if(NP.GetComponent<NetworkIdentity>().isLocalPlayer)
							NetPlayer = NP.GetComponent<NetPlayer>();
					}
			}
		}
		healthNow = health;
		prevLoc = transform.position;
		InvokeRepeating("ChangeDirection",0f,0.2f);
		InvokeRepeating("RecordPosition",0f,0.1f);		

	}

	void ChangeDirection()
	{
		Vector3 curVel = (transform.position - prevLoc) / Time.deltaTime;
		if(curVel.x <= -0.5)//it's moving left
		{
			this.GetComponent<SpriteRenderer>().flipX = true;
		} else if(curVel.x >=0.5) {
			
			this.GetComponent<SpriteRenderer>().flipX = false;
		}

		if(curVel.y > 20)
			gameObject.GetComponent<Animator>().SetBool("Back",true);
		else
			gameObject.GetComponent<Animator>().SetBool("Back",false);
		prevLoc = transform.position;
	}

	void RecordPosition()
	{
		PreviousPosition.Add(transform.position);
	}

	public void CheckViolationOffline()
	{
		StartCoroutine(Checkoff(PreviousPosition[PreviousPosition.Count-30],PreviousPosition[PreviousPosition.Count-20]));
	}

	IEnumerator Checkoff(Vector3 PreviousPos30,Vector3 PreviousPos20)
	{
		Vector3 PreviousPos ;
		if(this.GetComponent<AIPath>().maxSpeed >= 10)
		{
			yield return new WaitForSeconds(2f);
			PreviousPos = PreviousPos20;
		}
		else
		{
			yield return new WaitForSeconds(3f);
			PreviousPos = PreviousPos30;
		}

		Debug.Log("STARTCHECK");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(PreviousPos,6f);
		foreach(Collider2D col in colliders)
		{
			if(GameObject.ReferenceEquals(col.gameObject,gameObject))
			{
				PlayerOff player =  GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<PlayerOff>();
				player.GetDamage();
				Debug.Log("Finddd!!!!");
			}
		}
	}

	public void CheckViolation()
	{
		StartCoroutine(Check(PreviousPosition[PreviousPosition.Count-30],PreviousPosition[PreviousPosition.Count-20]));
	}

	IEnumerator Check(Vector3 PreviousPos30,Vector3 PreviousPos20)
	{
		Vector3 PreviousPos ;
		if(this.GetComponent<AIPath>().maxSpeed >= 10)
		{
			yield return new WaitForSeconds(2f);
			PreviousPos = PreviousPos20;
		}
		else
		{
			yield return new WaitForSeconds(3f);
			PreviousPos = PreviousPos30;
		}

		Debug.Log("STARTCHECK");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(PreviousPos, 6f);
		foreach(Collider2D col in colliders)
		{
			if(GameObject.ReferenceEquals(col.gameObject,gameObject))
			{
				this.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
				if(isServer)
				{
					Player playerHost =  GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<Player>();
					playerHost.GetDamage();
				}
				if(!isServer)
				{
					Player playerClient =  GameObject.FindGameObjectsWithTag("ClientHealth")[0].GetComponent<Player>();
					playerClient.GetDamage();
				}
				Debug.Log("Finddd!!!!");
			}
		}
	}

	void Update()
	{
		if(Time.time - ChangeTime > 0.25f)
		{
			if(FrozedEffect != null)
				Destroy(FrozedEffect);
		}
		if(CanSpeedUp)
		{
			if(this.GetComponent<AIPath>() != null)
				this.GetComponent<AIPath>().maxSpeed += AddSpeed;
			if(this.GetComponent<FlyMonster>() != null)
			{
				Debug.Log(this.GetComponent<FlyMonster>().StartSpeed);
	//			this.GetComponent<FlyMonster>().speed += AddSpeed;
			}
		}
	}

	public void assignOffLine()
	{
		isOffline = true;
	}

	public Transform FindClosestPlayer()
	{
		GameObject[] Players = GameObject.FindGameObjectsWithTag("Destination");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject Player in Players)
		{
			Vector3 diff = Player.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = Player;
				distance = curDistance;
			}
		}
			return closest.transform;
	}	

	public void TakeDamage (bool enabeldblood,bool isIceDamage,float amount,float IncreseDamageRate)
	{
		
		Debug.Log("Damage:"+amount);
		Debug.Log(healthNow);
		if(isOffline)
		{
			if(!isIceDamage && CanDodge)
			{
				float DRate = Random.Range(0f,100f);
				if(DRate <= DodgeRate)
				{
					Debug.Log("Dodge!");
					GameObject Dodgetext = Instantiate(DodgeText,transform.position,Quaternion.identity);
					Dodgetext.transform.parent = gameObject.transform;
					Destroy(Dodgetext,0.5f);
					return;
				}
			}

			if(HorseAssistance != null & !isIceDamage)
			{
				float JRate = Random.Range(0f,100f);
				if(JRate <= JumpRate)
				{
					Debug.Log("Jump!");
					GameObject HAssistance = Instantiate(HorseAssistance,transform.position,Quaternion.identity);
					HAssistance.GetComponent<HorseAssistance>().Jump(gameObject);
				}
			}

			else if(isOwl)
			{
				if(healthNow / health <= (SpeedUpHealthRate/100) & !AlreadSpeedUp)
				{
					this.GetComponent<FlyMonster>().speed += OwlSpeed;
					AlreadSpeedUp = true;
					GameObject WindEffect = Instantiate(WindEffectPrefab,transform.position,Quaternion.identity);
					WindEffect.transform.parent = gameObject.transform;
					if(FrozedNow)
					{
						foreach(GameObject Ice in Ices)
						{
							Destroy(Ice);
						}
						this.GetComponent<FlyMonster>().enabled = true;
						this.GetComponent<Animator>().enabled = true;
					}
				}
			}


		Debug.Log(healthNow);
			if(IncreseDamage)
				healthNow -= amount*OffLineIncreseDamageRate*GuardRate/100;
			else
				healthNow -= amount*GuardRate/100;
			healthBar.fillAmount = healthNow / health;

		Debug.Log(healthNow);
			if(!isIceDamage & ! FrozedNow)
				GetDamageEffect();

		Debug.Log(healthNow);
			if(healthNow <= 0)
			{
				if(Ice != null)
					Destroy(Ice);
				if(this.name == "AutoEnemy(Clone)")
				{
					GameObject MoneyT = Instantiate(MoneyText,transform.position,Quaternion.identity);
					MoneyTextInfo.text = "+" + Money.ToString();
					PlayerOff.Money += Money;
					Debug.Log("AddMoney");
					Destroy(MoneyT,.5f);
					FindObjectsOfType<GameManageNewOff>()[0].GetComponent<GameManageNewOff>().AddScore(10);
				}

				float rate = Random.Range(0f, 100.0f);
				if(rate <= CaptureRate & this.name == "AutoEnemy(Clone)")
				{
					Debug.Log("Cap");
					Capture();
				}
				if(this.GetComponent<Loris>() != null)
					this.GetComponent<Loris>().SetVelocity(this.GetComponent<AIPath>().velocity.x,this.GetComponent<AIPath>().velocity.y);
				else if(this.GetComponent<Turtle>() != null)
					this.GetComponent<Turtle>().HealMonster();

				if(isdead)
					return;
				DeadProcess();
			}
			return;
		}
		else if(CanSpeedUp)
		{
			if(healthNow / health <= (SpeedUpHealthRate/100) & !AlreadSpeedUp)
			{
				this.GetComponent<FlyMonster>().speed += OwlSpeed;
				AlreadSpeedUp = true;
				Debug.Log(healthNow);
				Debug.Log(health);
			}
		}	

		if(!isIceDamage && CanDodge)
		{
			if(hasAuthority)
				return;
			NetPlayer.CmdDecideMonsterDodge(this.GetComponent<NetworkIdentity>().netId,amount,IncreseDamage);
		}
		else
		{
			if(hasAuthority)
				return;
			NetPlayer.CmdMonsterGetDamage(this.GetComponent<NetworkIdentity>().netId,amount,IncreseDamage);
		}

		if(healthNow <=0)
		{
			Destroy(Ice);
			ReadyToDestroy();
		}	
	}

	void DeadProcess()
	{
		
		isdead = true;
		this.GetComponent<Animator>().enabled = false;
		if(this.GetComponent<AIPath>() != null)
			this.GetComponent<AIPath>().enabled = false;
		else
			this.GetComponent<FlyMonster>().enabled = false;
		gameObject.tag = "Untagged";
		enemysprite.sprite = DeadSprite;
		StartCoroutine(Fadeout());
				
		Destroy(gameObject,1.25f);
	}

	void StartHit()
	{
		gameObject.GetComponent<Animator>().enabled = false;
		enemysprite.color = new Color32(255,168,168,255);
		enemysprite.sprite = HitSprite;
//		if(gameObject.GetComponent<AIPath>() != null)
//			gameObject.GetComponent<AIPath>().enabled = false;
//		else
//			gameObject.GetComponent<FlyMonster>().enabled = false;
	}

	public void GetDamageEffect()
	{
		
			GameObject HitEffect = Instantiate(HitEffectPrefab,transform.position,Quaternion.identity);
			StartHit();
			StartCoroutine(EndHit());
			Destroy(HitEffect,1f);
	}

	IEnumerator Fadeout()
	{
		
		enemysprite.color = new Color32(255,255,255,255);
		int color = 1;
		while(color <10)
		{
			yield return new WaitForSeconds(.1f);
			HealthBarBack.color = new Color32(255,255,255,(byte)(255-(25.5*color)));
			enemysprite.color = new Color32(255,255,255,(byte)(255-(25.5*color)));
			color += 1;
		}
	}

	IEnumerator EndHit()
	{
//		yield return new WaitForSeconds(.05f);
//		if(gameObject.GetComponent<AIPath>() != null)
//			gameObject.GetComponent<AIPath>().enabled = true;
//		else
//			gameObject.GetComponent<FlyMonster>().enabled = true;
		yield return new WaitForSeconds(.3f);
		enemysprite.color = new Color32(255,255,255,255);
		if(healthNow>0 & !FrozedNow)
			gameObject.GetComponent<Animator>().enabled = true;
	}

	public void ReadyToDestroy()
	{
		float rate = Random.Range(0f, 100.0f);
		if(isServer && !hasAuthority)
		{
			GameObject MoneyT = Instantiate(MoneyText,transform.position,Quaternion.identity);
			MoneyTextInfo.text = "+" + Money.ToString();
			Destroy(MoneyT,.5f);
			Player.Money += Money;
			if(rate <= CaptureRate)
				Capture();
		}
		else if(!isServer && !hasAuthority)
		{		
			GameObject MoneyT = Instantiate(MoneyText,transform.position,Quaternion.identity);
			MoneyTextInfo.text = "+" + Money.ToString();
			Destroy(MoneyT,.5f);
			Debug.Log("add MOney");
			Debug.Log(Money);
			Player.Money += Money;
			if(rate <= CaptureRate)
				Capture();
		}
		
		DeadProcess();
	}

	void Capture()
	{
		if(CapturableStatus == null)
			return;
		GameObject CEnemyInfo = Instantiate (CapturableStatus,transform.position,Quaternion.identity);
		Vector3 temp = CEnemyInfo.transform.position;
		temp.z = -3;
		CEnemyInfo.transform.position = temp;
	}

	[Command]
	void CmdSpawnCapturable(NetworkInstanceId NetPlayerId,NetworkInstanceId MonsterId)
	{
		GameObject CEnemyInfo = Instantiate (CapturableStatus,transform.position,Quaternion.identity);
		Vector3 temp = CEnemyInfo.transform.position;
		temp.z = -3;
		CEnemyInfo.transform.position = temp;
		NetworkServer.SpawnWithClientAuthority(CEnemyInfo,connectionToClient);
		NetworkServer.Destroy(ClientScene.FindLocalObject(MonsterId));
		Destroy(ClientScene.FindLocalObject(MonsterId));
	}

	public void Frozed(float DecreseSpeed)
	{
		if(isFly)
		{
			if(AlreadSpeedUp)
				return;
				
			this.GetComponent<FlyMonster>().speed = this.GetComponent<FlyMonster>().StartSpeed*DecreseSpeed;
			if(FrozedEffect == null)
			{
				FrozedEffect = Instantiate(FrozedEffectPrefab,transform.position,Quaternion.identity);
				FrozedEffect.transform.parent = gameObject.transform;
				FrozedEffect.transform.localScale = new Vector3 (4,4,4);
			}

		}

		else
		{
			if(FrozedEffect == null)
			{
				FrozedEffect = Instantiate(FrozedEffectPrefab,transform.position,Quaternion.identity);
				FrozedEffect.transform.parent = gameObject.transform;
				FrozedEffect.transform.localScale = new Vector3 (4,4,4);
			}
			this.GetComponent<AIPath>().maxSpeed = this.GetComponent<AIPath>().InitialSpeed*DecreseSpeed;
		}
		ChangeTime = Time.time;
	}

	public void Stop(float FreezeTime,float OffLineIncreseDamageRate,GameObject Ice)
	{
		ColdTime += 1;
		Ices.Add(Ice);
		this.OffLineIncreseDamageRate = OffLineIncreseDamageRate;
		FrozedNow = true;
		if(isFly & this.GetComponent<FlyMonster>() != null)
		{
			if(isOwl)
				Ice.transform.position += new Vector3(0,4,0);
			else
				Ice.transform.position += new Vector3(0,1,0);

			Ice.GetComponent<SpriteRenderer>().sortingLayerName = "flyEnemy";
			if(!AlreadSpeedUp)
			{
				this.GetComponent<FlyMonster>().enabled = false;
				this.GetComponent<Animator>().enabled = false;
			}
		}
		else
		{
			this.GetComponent<AIPath>().enabled = false;
			this.GetComponent<Animator>().enabled = false;
		}
		IncreseDamage = true;
		Ice.transform.localScale = new Vector2(IceSize,IceSize);
		Ice.transform.localPosition = new Vector3(0,Ice.transform.localPosition.y,Ice.transform.localPosition.z);

		if(IceSize <.8f)
			Ice.transform.localPosition = new Vector3(0,0,0);

		Debug.Log("Frozed!!!");
		StartCoroutine(EnableMove(FreezeTime,Ice));
	}

	IEnumerator EnableMove(float FreezeTime,GameObject Ice)
	{ 
		yield return new WaitForSeconds(FreezeTime);
		ColdTime -= 1;
		Destroy(Ices[0]);
		Ices.RemoveAt(0);
		if(ColdTime > 0)
		{
			Debug.Log("still Frozed:"+ColdTime);
		}
		else
		{
			Debug.Log("thaw");
			Destroy(Ice);
			Debug.Log(ColdTime);
			if(!isdead)
			{
				if(isFly)
					this.GetComponent<FlyMonster>().enabled = true;
				else
					this.GetComponent<AIPath>().enabled = true;
				
					this.GetComponent<Animator>().enabled = true;
				IncreseDamage = false;
				FrozedNow = false;
			}
		}
	}

	public void SetDead()
	{
		isdead = true;
	}

	public bool AlreadyDead()
	{
		return isdead;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "HostHealth")
		{
			if(!isOffline & isServer)
			{
				Player player =  col.gameObject.GetComponent<Player>();
				if(CanDoubleDamage)
				{
					Debug.Log(col.gameObject);
					NetPlayer.CmdDecideDoubleDamage(col.gameObject.GetComponent<NetworkIdentity>().netId);
				}
				else
					player.GetDamage();
				NetworkServer.Destroy(gameObject);
			}
			if(isOffline)
			{
				PlayerOff player =  col.gameObject.GetComponent<PlayerOff>();
				player.GetDamage();
				Destroy(gameObject);
			}
		}
		if(col.gameObject.tag == "ClientHealth")
		{
			if(!isOffline & !isServer)
			{
				Player player =  col.gameObject.GetComponent<Player>();
				if(CanDoubleDamage)
				{
					NetPlayer.CmdDecideDoubleDamage(col.gameObject.GetComponent<NetworkIdentity>().netId);
				}
				else
					player.GetDamage();
				Debug.Log("DEstroy" + gameObject);
				NetPlayer.CmdDestroy(gameObject.GetComponent<NetworkIdentity>().netId);
			}
			if(isOffline)
			{
				if(CanDoubleDamage)
				{
					float Rate = Random.Range(0f,100f);
					PlayerOff player = col.gameObject.GetComponent<PlayerOff>();
					EnemySpawnOff SpawnManager = FindObjectsOfType<EnemySpawnOff>()[0].GetComponent<EnemySpawnOff>();
					Debug.Log(Rate);
					Debug.Log(SpawnManager.DoubleDamageRate);
					if(Rate <= SpawnManager.DoubleDamageRate)
					{
						Debug.Log("Double");
						player.Lives -= 2;
						player.UpdateLive();
//						player.gameObject.GetComponent<AudioSource>().Play();
						if(player.Lives <= 0)
						{
							FindObjectsOfType<GameManageNewOff>()[0].GetComponent<GameManageNewOff>().DefineWinLose(player);
						}
						Destroy(gameObject);
					}
					else
					{
						player =  col.gameObject.GetComponent<PlayerOff>();
						player.GetDamage();
						Destroy(gameObject);
					}
				}
				else
				{
					PlayerOff player2 =  col.gameObject.GetComponent<PlayerOff>();
					player2.GetDamage();
					Destroy(gameObject);
				}
			}
		}
	}

	public void SpeedUp(float Speed)
	{
		AddSpeed = Speed;
		CanSpeedUp = true;
	}

	public void DoubleDamage()
	{
		CanDoubleDamage = true;
	}

	public void AddLife(float Life)
	{
		health += Life;
		healthNow += Life;
		if(isOffline)
			healthBar.fillAmount = healthNow / health;
		else
			NetPlayer.CmdMonsterGetDamage(this.GetComponent<NetworkIdentity>().netId,0,false);  // To Update enemy's health
	}

	public void AddCaptureRate(float AddedCaptureRate)
	{
		CaptureRate += AddedCaptureRate;
	}

	public void Guard(float GuardRate)
	{
		this.GuardRate = GuardRate;
	}

	public void AllowChangeTurret()
	{
		CanChangeTurret = true;
	}

	public bool CanChangeT()
	{
		return CanChangeTurret;
	}

	public void AllowAvoidExplode()
	{
		CanAvoidExplode = true;
	}

	public bool GetCanAvoidExplode()
	{
		return CanAvoidExplode;
	}

	public void SetSpeedUp()
	{
		OwlSpeedUp = true;
	}
	public bool isSpeedUp()
	{
		return OwlSpeedUp;
	}

	public void SetAlreadySpeedUp()
	{
		AlreadSpeedUp = true;
	}

	public bool hasSpeedUp()
	{
		return AlreadSpeedUp;
	}



}
