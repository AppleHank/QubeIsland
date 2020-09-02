using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NODE : NetworkBehaviour {

	
	private Renderer rend;
	private Color start_color;
	public Color hover_color;
	private Vector3 positionOffset = new Vector3(0,0,0);

	public GameObject turret;
	[HideInInspector]
	public Blueprint turretBlueprint;

	public Blueprint startground;

	buildmanager buildmanager;

	public bool isStartNode = false ;
	public bool isStone = false;
	public bool isPlayer = false;
	private GameObject lastRange;

	public NetPlayer NetPlayer;

	public GameObject FirstNode;
	public GameObject FinalNode;

	public AudioSource SelectAudiance;


	// Use this for initialization
	void Start () {
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
		// Safe to update graphs here
		var node = AstarPath.active.GetNearest(transform.position).node;
		node.Walkable = false;
		}));

		rend = GetComponent<Renderer>();
		start_color = rend.material.color;

		buildmanager = buildmanager.instance;
	}
	
	public Vector3 GetBuildPosition(){
		return transform.position + positionOffset;
	}
	private bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
	}
	void OnMouseDown(){
		Debug.Log("!!!!!DO");
		if(IsPointerOverUIObject())
			return;
//		buildmanager.DisableCanvas();
		DisTurretRange();

		if(isServer)
			if(this.tag !="HostNode")
				return;
		if(!isServer)
			if(this.tag !="ClientNode")
				return;

		if(EventSystem.current.IsPointerOverGameObject())
			return;

		if(turret != null){
			buildmanager.DisBuild();
			buildmanager.ResetNodeUIColor();
			SelectAudiance.Play();
			if(turret.tag == "turret" | turret.tag == "UpgradedTurret")
			{	
				DisTurretRange();
				Tower tower =  turret.GetComponent<Tower>();
				tower.ShowRange();
				buildmanager.SelectNode(this);
			}
			else if(turret.tag == "path")
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
				// Safe to update graphs here
				var node = AstarPath.active.GetNearest(transform.position).node;
				node.Walkable = false;
				}));
				StartCoroutine(WaitForAstar());			
			}
			else if(turret.tag == "MoneyTurret")
			{
				Debug.Log("TouchMony!!!");
				turret.GetComponent<MoneyTurret>().Money();
			}
			else if(turret.tag == "HolyTurret")
			{
				Debug.Log("Heal!!!!");
				turret.GetComponent<HolyTurret>().Heal();
			}

			return;
		}
		else
			buildmanager.OffNodeUI();

		if(!buildmanager.CanBuild)
			return;

	}

	public void Build()
	{
		buildmanager.DestroyPreturret();
		BuildTurret(buildmanager.GetTurretToBuild());	 //GetTurretToBuild會回傳要蓋的塔的藍圖
	}

	IEnumerator WaitForAstar()
	{
		yield return new WaitForEndOfFrame();
		GraphNode node1 = AstarPath.active.GetNearest(FirstNode.transform.position).node;
		GraphNode node2 = AstarPath.active.GetNearest(FinalNode.transform.position).node;
		if(!PathUtilities.IsPathPossible(node1,node2))
		{
			buildmanager.NodeUIBlock();
		}
		buildmanager.SelectNode(this);
		AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
		var Anode = AstarPath.active.GetNearest(transform.position).node;
		Anode.Walkable = true;
		}));
		print(Time.time);
	}

	public void DisTurretRange()
	{
		GameObject[] Ranges =  GameObject.FindGameObjectsWithTag("TowerRange");
		foreach(GameObject range in Ranges)
			range.SetActive(false);
	}




	void BuildTurret (Blueprint blueprint)
	{
		Debug.Log(blueprint.prefab.name);
		if(blueprint == null)
			return;
		if(Player.Money < blueprint.cost){
			Debug.Log("Dont have money");
			return;
		}
		turretBlueprint = blueprint;
		turretBlueprint.prefab.GetComponent<AudioSource>().Play();
		NetPlayer.build(gameObject,blueprint.prefab,GetBuildPosition());

		Player.Money -= blueprint.cost;
		Debug.Log(Player.Money);
	}

	public void SetTurret(GameObject _turret)
	{
		turret = _turret;
	}

	public void SellCheck (){
		if(turret.tag == "path")
		{
			Debug.Log("isPath");
			AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
			var node = AstarPath.active.GetNearest(transform.position).node;
			node.Walkable = false;
			}));
			Invoke("CheckIsPathPossible",0.025f);//I don't want to use Invoke QQ
		}
		else
		{
			Sell();
		}
	}

	public void CheckIsPathPossible()
	{
		GraphNode node1 = AstarPath.active.GetNearest(FirstNode.transform.position).node;
		GraphNode node2 = AstarPath.active.GetNearest(FinalNode.transform.position).node;
		if(!PathUtilities.IsPathPossible(node1,node2))
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(() => {
				var node = AstarPath.active.GetNearest(transform.position).node;
				node.Walkable = true;
				}));
				Debug.Log("Path Error!!!");
				return;
			}
		//means Path Possible
		Sell();
	}

	void Sell()
	{
		if(turret.name == "Iceturret(Clone)"  | turret.name == "UpgradedIceturret(Clone)")
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, turret.GetComponent<Tower>().range);
			StartCoroutine(Paththaw(colliders));	
		}
		NetPlayer.Cmdsell(turret.GetComponent<NetworkIdentity>().netId,gameObject.GetComponent<NetworkIdentity>().netId);
		Player.Money += turretBlueprint.Sell();
	}

	IEnumerator Paththaw(Collider2D[] colliders)
	{
		yield return new WaitForSeconds(3f);
		
			foreach (Collider2D collider in colliders)
			{
				if (collider.tag == "HostNode" | collider.tag == "ClientNode")
				{
					NODE node = collider.GetComponent<NODE>();
					if(node.turret != null)
					{
						if( node.turret.tag == "path")
						{
							node.NetPlayer.CmdDestroy(node.turret.GetComponent<NetworkIdentity>().netId);
							node.NetPlayer.ReplaceSnowPath(collider.gameObject,node.GetBuildPosition());
						}
					}
				}
			}
	}


	public void Upgrade(int turretnum)
	{
		NetPlayer.CmdDestroy(turret.GetComponent<NetworkIdentity>().netId);
		NetPlayer.CmdUpgrade(gameObject.GetComponent<NetworkIdentity>().netId,turretnum,GetBuildPosition());
		Player.Money -= turretBlueprint.UpgradeCost;
		int NewCost = turretBlueprint.cost + turretBlueprint.UpgradeCost;
		turretBlueprint = new Blueprint(NewCost);
	}
	
	
	void OnMouseEnter(){
		if(!isServer)
			if(this.tag !="ClientNode")
				return;
		if(isServer)
			if(this.tag !="HostNode")
				return;
		if(!buildmanager.CanBuild)
			return;
		if(turret!=null)
			buildmanager.DestroyPreturret();
		else{
			rend.material.color = hover_color;
			buildmanager.BuildPreturret(this);
		}
	}

	void OnMouseExit(){
		rend.material.color = start_color;
		buildmanager.DestroyPreturret();
	}		

	void OnDrawGizmosSelected(){
		#if UNITY_EDITOR
		UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position , transform.forward, 2f);
		#endif
	}

}
