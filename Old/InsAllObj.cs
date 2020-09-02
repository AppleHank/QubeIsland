using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsAllObj : MonoBehaviour {

	public bool isFriendIsland;

	// Use this for initialization
	void Start () {
		Debug.Log("in Island");
		InvokeRepeating("ReleaseMemory",0f,30f);
		Debug.Log("in Island Next");
		if(isFriendIsland)
			Instantiate(Resources.Load<GameObject>("FriendIsland"));
		else
			Instantiate(Resources.Load<GameObject>("Obj 1"));
		Debug.Log("Instantiate");
//		StartCoroutine(InsPet());
	}

	void ReleaseMemory()
	{
		Debug.Log("Release Memory");
		Resources.UnloadUnusedAssets();
	}

	IEnumerator InsPet()
	{
		yield return new WaitForSeconds(30f);

		Debug.Log("StartInstantiatePet");
		Instantiate(Resources.Load<GameObject>("Obj1"));
	}
	
}
