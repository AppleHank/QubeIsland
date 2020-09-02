using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
[System.Serializable]
public class Blueprint  {

	public GameObject prefab;
	public GameObject Preturret;
	public int cost;
	public Text turretcost;
	public GameObject Buttom;
	public GameObject UpgradedTurret;
	public GameObject Disbutton;

	public int UpgradeCost;

	public Blueprint(int cost)
	{
		this.cost = cost;
	}

	public int Sell ()
	{
		return (int)(cost * 0.6f);
	}

}
