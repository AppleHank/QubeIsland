using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;

public class NodeUI : MonoBehaviour {

	public GameObject ui;
	private DrawCircle draw;
	private bool isOffline;

	private NODE target;
	private NodeOff targetOffline;
	public NodeUI startPosition;
	public Text selltext;
	public Text Upgradetext;
	public GameObject SellButtom;
	public GameObject UpgradeButtom;
	public RectTransform NodeUICanvas;
	public GameObject SellBlock;

	[Header("UpgradeBlueprint")]
	public Blueprint UST;
	public Blueprint UBT;
	public Blueprint UFT;
	public Blueprint UIT;

	private List<NodeOff> NodeList = new List<NodeOff>();

	void Update()
	{
		if(isOffline)
		{
			if(PlayerOff.Money < targetOffline.turretBlueprint.UpgradeCost)
				UpgradeButtom.GetComponent<Image>().color = new Color32(75,75,75,255);
			else
				UpgradeButtom.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
		else
		{
			if(target != null)
			{
				if(Player.Money < target.turretBlueprint.UpgradeCost)
					UpgradeButtom.GetComponent<Image>().color = new Color32(75,75,75,255);
				else
					UpgradeButtom.GetComponent<Image>().color = new Color32(255,255,255,255);
			}
		}
	}

	public void SetTarget (NODE _target) {
		target = _target;

		Vector3 temp = target.transform.position;
		temp.z = -2;
		temp.y -= 5;
		transform.position = temp;
		ui.SetActive(true);
		if(_target.turret.tag == "path" | _target.turret.tag == "UpgradedTurret")
		{
			NodeUICanvas.sizeDelta = new Vector2 (950,500);
			UpgradeButtom.SetActive(false);
			SellButtom.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
			SellBlock.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
		}
		else
		{
			NodeUICanvas.sizeDelta = new Vector2 (950,1000);
			UpgradeButtom.SetActive(true);
			SellButtom.GetComponent<RectTransform>().localPosition = new Vector2(0,-250);
			SellBlock.GetComponent<RectTransform>().localPosition = new Vector2(0,-250);
		}


		GameObject drawcircle = target.GetComponent<NODE>().turret;
		#if UNITY_EDITOR
		Selection.activeGameObject = drawcircle;
		#endif
		
		setprice();
	}

	public void SetTargetoff (NodeOff _target) {
		isOffline = true;
		targetOffline = _target;


		Vector3 temp = targetOffline.transform.position;
		temp.z = -2;
		temp.y -= 5;
		transform.position = temp;
		ui.SetActive(true);
		if(_target.turret.tag == "path" | _target.turret.tag == "UpgradedTurret")
		{
			NodeUICanvas.sizeDelta = new Vector2 (950,500);
			UpgradeButtom.SetActive(false);
			SellButtom.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
			SellBlock.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
		}
		else
		{
			NodeUICanvas.sizeDelta = new Vector2 (950,1000);
			UpgradeButtom.SetActive(true);
			SellButtom.GetComponent<RectTransform>().localPosition = new Vector2(0,-250);
			SellBlock.GetComponent<RectTransform>().localPosition = new Vector2(0,-250);
		}


		GameObject drawcircle = targetOffline.GetComponent<NodeOff>().turret;
		#if UNITY_EDITOR
		Selection.activeGameObject = drawcircle;
		#endif
		
		setpriceoff();
	}

	public void setprice()
	{
		selltext.text =  target.turretBlueprint.Sell().ToString();
		Upgradetext.text =  target.turretBlueprint.UpgradeCost.ToString();
	}

	public void setpriceoff()
	{
		selltext.text = targetOffline.turretBlueprint.Sell().ToString();
		Upgradetext.text = targetOffline.turretBlueprint.UpgradeCost.ToString();
	}

	public void Hide () {
		ui.SetActive(false);
	}

	public void sell ()
	{
		if(target == null)//means offline play
		{
			selloff();
			return;
		}
//		FindNode(target);
		CheckViolation();
		target.SellCheck();
		buildmanager.instance.DisableCanvas();
	}

	public void selloff()
	{
//		StartFindNode(targetOffline);
		CheckViolationOffline();
		targetOffline.SellCheck();
		buildmanager.instance.DisableCanvas();
	}

	public void Upgrade()
	{
			
		buildmanager.instance.DisableCanvas();
		if(Player.Money < target.turretBlueprint.UpgradeCost)
			return;

		GameObject turret = target.turret;
		int turretnum = 0;
		if(turret.name == "Standardturret(Clone)")
			turretnum = 1;
		else if(turret.name == "Bloodturret(Clone)")
			turretnum = 2;
		else if(turret.name == "Fireturret(Clone)")
			turretnum = 3;
		else if(turret.name == "Iceturret(Clone)")
			turretnum = 4;
		else if(turret.name == "Holyturret(Clone)")
			turretnum = 5;
		else if(turret.name == "Lavaturret(Clone)")
			turretnum = 6;
		else if(turret.name == "Frozeturret(Clone)")
			turretnum = 7;
				
		Debug.Log(turretnum);
		target.Upgrade(turretnum);
	}

	public void Upgradeoff()
	{
		buildmanager.instance.DisableCanvas();
		if(PlayerOff.Money < targetOffline.turretBlueprint.UpgradeCost)
			return;
		GameObject turret = targetOffline.turret;
		int turretnum = 0;
		targetOffline.Upgrade();
	}

	void CheckViolationOffline()
	{
		Debug.Log("StartCheck");
		GameObject[] Enemys = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject E in Enemys)
		{
			enemy Enemy = E.GetComponent<enemy>();
			if(Enemy.PreviousPosition.Count >= 30)
			{
				Enemy.CheckViolationOffline();
			}
		}
	}

	void CheckViolation()
	{
		GameObject[] Enemys = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject E in Enemys)
		{
			if(!E.GetComponent<NetworkIdentity>().hasAuthority)
			{
				enemy Enemy = E.GetComponent<enemy>();
				if(Enemy.PreviousPosition.Count >= 30)
				{
					Enemy.CheckViolation();
				}
			}
		}
	}

	
	void StartFindNode(NodeOff node) //OFFLINE MODE
	{
		int Nodenum = 0;
		Debug.Log("!!!!");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(node.transform.position, 8.2f);
		foreach(Collider2D col in colliders)
		{
			if(col.tag == "HostNode" | col.tag == "ClientNode")
				{
				Vector3 CTP = col.transform.position;
				Vector3 NTP = node.transform.position;
				if(CTP.x <= NTP.x - 8.1 & CTP.x >= NTP.x - 8.3)
				{
					if(CTP.y == NTP.y & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path")
						{
							Nodenum += 1;
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
				if(CTP.x <= NTP.x + 8.3 & CTP.x >= NTP.x + 8.1)
				{
					if(CTP.y == NTP.y & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path")
						{
							Nodenum += 1;
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
				if(CTP.y <= NTP.y - 8.1 & CTP.y >= NTP.y - 8.5)
				{
					if(CTP.x == NTP.x & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path")
						{
							Nodenum += 1;
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
				if(CTP.y <= NTP.y + 8.5 & CTP.y >= NTP.y + 8.1)
				{
					if(CTP.x == NTP.x & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path")
						{
							Nodenum += 1;
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
			}
		}
		if(Nodenum >= 3)
		{
			NodeList.Add(node);
			foreach(NodeOff n in NodeList)
				Debug.Log(n);
		}
	}


	void FindNode(NodeOff LastNode,NodeOff node) //OFFLINE MODE
	{
		int Nodenum = 0;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(node.transform.position, 8.2f);
		foreach(Collider2D col in colliders)
		{
			if(col.tag == "HostNode" | col.tag == "ClientNode")
				{
				Vector3 CTP = col.transform.position;
				Vector3 NTP = node.transform.position;
				
				if(CTP.x <= NTP.x - 8.1 & CTP.x >= NTP.x - 8.3)
				{
					if(CTP.y == NTP.y & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path" & col.name != LastNode.name)
						{
							Nodenum += 1;
							
							Debug.Log(col);
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
				if(CTP.x <= NTP.x + 8.3 & CTP.x >= NTP.x + 8.1)
				{
					if(CTP.y == NTP.y & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path" & col.name != LastNode.name)
						{
							Nodenum += 1;
							Debug.Log(col);
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
				if(CTP.y <= NTP.y - 8.1 & CTP.y >= NTP.y - 8.5)
				{
					if(CTP.x == NTP.x & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path" & col.name != LastNode.name)
						{
							Nodenum += 1;
							
							Debug.Log(col);
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
				if(CTP.y <= NTP.y + 8.5 & CTP.y >= NTP.y + 8.1)
				{
					if(CTP.x == NTP.x & col.GetComponent<NodeOff>().turret != null)
					{
						if(col.GetComponent<NodeOff>().turret.tag == "path" & col.name != LastNode.name)
						{
							Nodenum += 1;
				
							Debug.Log(col);
							FindNode(node,col.GetComponent<NodeOff>());
						}
					}
				}
			}
		}
		if(Nodenum >= 3)
		{
			NodeList.Add(node);
		}
	}



}
