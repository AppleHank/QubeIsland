using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class MonsterBlueprint  {

	public int MonsterNum;
	public GameObject prefab;
	public int cost;
	public int autoadd;
	public Text CostText;
	public bool CanSound = true;
	public Image Buttom;
	public GameObject Disbutton;
	public Image ColdDownImg;
	public float ColdDownTime;
	public GameObject Block;
	private float InitialTime;
	private float CanSpawnTime;

	public float GetInitialTime()
	{
		return InitialTime;
	}

	public void SetCanSpawnTime()
	{
		CanSpawnTime = InitialTime + ColdDownTime;
	}

	public void SetInitialTime()
	{
		InitialTime = Time.time;
	}

	public float GetCanSpawnTime()
	{
		return CanSpawnTime;
	}


}
