using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Tower : NetworkBehaviour {

	public Transform target;
	public string enemyTag = "enemy";
	private bool isOffline;

	[Header("Attributes")]
	public int range = 2;
	public float fireRate = 100f;
	private float fireCountdown = 0f;

	[Header("Setting")]
	public GameObject bulletPrefab;
	public Transform firePoint;
	public GameObject Mesh;
	public bool canAttackFly;
	public bool isHolyturret;
	public bool isST;
	public bool isBT;
	public bool isFT;
	private bool isInitialTurret;

	[Header("Specialturrret")]
	public bool isIceTurret;
	public bool isLavaTurret;
	public GameObject Effect;
	public GameObject Path;
	public float DecreseSpeed;
	public float LavaRate;
	private Vector3 tempPosition;

	private bool ChangeToBearCat;

	// Use this for initialization
	void Start () {

		Debug.Log("Start");

		if(gameObject.GetComponent<AudioSource>() != null & !isInitialTurret)
		{
			this.GetComponent<AudioSource>().Play();
		}
		tempPosition = transform.position;
		tempPosition.y += 3.5f;
		transform.position = tempPosition;

		if(isST)
		{
			Vector3 STtemp = transform.position;
			STtemp.y += 1.2f;
			transform.position = STtemp;
		}

		if(isBT)
		{
			Vector3 BTtemp = transform.position;
			BTtemp.y += 1.5f;
			transform.position = BTtemp;
		}

		if(isFT)
		{
			Vector3 FTtemp = transform.position;
			FTtemp.y += 0.7f;
			transform.position = FTtemp;
		}

		if(isIceTurret)
		{
			Vector3 Stemp = Effect.transform.position;
			Stemp.z = 35;
			Effect.transform.position = Stemp;
			Vector3 Temp = transform.position;
			Temp.y += 1;
			transform.position = Temp;
			if(isOffline)
				InvokeRepeating("OfflinePathSnow",7f,7f);
			else
				InvokeRepeating("pathsnow",7f,7f);
		}
		if(isHolyturret)
		{
			Vector3 Htemp = transform.position;
	//		Htemp.z = -3;
			transform.position = Htemp;
		}
		Debug.Log("asdasdds");
		if(isLavaTurret)
		{
			Debug.Log("isLava");
			InvokeRepeating("Burn",0f,LavaRate);
		}
		else
			InvokeRepeating("UpdateTarget",0f,0.5f);
		Vector3 temp = transform.localScale;
		temp.x = range;
		temp.y = range;
		if(Mesh!=null)
			Mesh.transform.localScale = temp;
	}

	public void SetOffLine()
	{
		isOffline = true;
	}

	public void SetInitialTurret()
	{
		isInitialTurret = true;
	}

	void pathsnow()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
			foreach (Collider2D collider in colliders)
			{
				if (collider.tag == "HostNode" | collider.tag == "ClientNode")
				{
					NODE node = collider.GetComponent<NODE>();
					Debug.Log(node.turret);
					if(node.turret != null)
					{
						if( node.turret.tag == "path")
						{
							node.NetPlayer.CmdDestroy(node.turret.GetComponent<NetworkIdentity>().netId);
							node.NetPlayer.buildSnowPath(collider.gameObject,node.GetBuildPosition());
						}
					}
				}
			}
	}

	void OfflinePathSnow()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
			foreach (Collider2D collider in colliders)
			{
				if (collider.tag == "HostNode" | collider.tag == "ClientNode")
				{
					NodeOff node = collider.GetComponent<NodeOff>();
					if(node.turret != null)
					{
						if( node.turret.tag == "path")
						{
							Destroy(node.turret);
							node.buildsnow(Path,node.GetBuildPosition());
						}
					}
				}
			}		
	}

	void Frozen()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
		foreach (Collider2D collider in colliders)
		{
			if (collider.tag == "enemy")
			{
				collider.GetComponent<enemy>().Frozed(DecreseSpeed);
				target = collider.transform;
				shoot();
			}
		}
	}

	void Burn()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
		foreach (Collider2D collider in colliders)
		{
			if (collider.tag == "enemy")
			{
				Debug.Log("shoot");
				target = collider.transform;
				shoot();
				
			}
		}
	}
	
	void UpdateTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
			foreach (GameObject enemy in enemies){
				if(canAttackFly | enemy.GetComponent<enemy>().isFly == false)
				{
					float distanceToEnemy = Vector3.Distance(transform.position,enemy.transform.position);
					if (distanceToEnemy < shortestDistance)
					{
						shortestDistance = distanceToEnemy;
						nearestEnemy = enemy;
					}
				}
			}
		if (nearestEnemy !=null && shortestDistance <=range){
			target = nearestEnemy.transform;
		}
		else{
			target = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if(isIceTurret)
		{
			Frozen();
		}
		else
		{
			if (target == null)
				return;
			if(fireCountdown <=0f & LavaRate == 0)
			{
				shoot();
				fireCountdown = 1f / fireRate;
			}
		
		}
		fireCountdown -= Time.deltaTime;
	}

	void shoot(){
		GameObject bulletGo =  (GameObject)Instantiate (bulletPrefab,firePoint.position,firePoint.rotation);
		Bullet bullet = bulletGo.GetComponent<Bullet>();

		if (bullet != null){
			bullet.seek(target);
		}

	}

	public void ShowRange ()
	{
		Mesh.SetActive(true);
	}
	public void HideRange ()
	{
		Mesh.SetActive(false);
	}

	void OnDrawGizmosSelected(){
		#if UNITY_EDITOR 
		UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position , transform.forward, range);
		#endif
	}

	public bool GetOffline()
	{
		return isOffline;
	}

	public bool GetChangeStatus()
	{
		return ChangeToBearCat;
	}
	public void ChangeStatus()
	{
		ChangeToBearCat = !ChangeToBearCat;	
	}
}

