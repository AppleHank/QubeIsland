using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateIndex : MonoBehaviour {

	public static List<int> TreeList = new List<int>();

	public static int GetIndex()
	{
		return TreeList.Count;
	}

	public static void AddListNum()
	{
//		TreeList.Add(1);
		PlayerPrefs.SetInt("TreeListCount",TreeList.Count);
		Debug.Log(PlayerPrefs.GetInt("TreeListCount",-1));
	}

	void Start()
	{
		for(int i=1;i<=PlayerPrefs.GetInt("TreeListCount",0);i++)
		{
			TreeList.Add(i);
		}
//		GetComponent<BuildingMemory>().BuildTree();
	}




}
