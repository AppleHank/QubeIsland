using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Loris : MonoBehaviour {

	public GameObject SmallLoris;
	private float Vx;
	private float Vy;
	public bool isUpgrade;
	public GameObject UpSmallLorisFire;
	public GameObject UpSmallLorisFlag;
	private GameObject SmallLorisToSpawn;
	public GameObject SpawnEffectPrefab;

	public void SetVelocity(float x,float y)
	{
		Vx = x;
		Vy = y;
	}

	void OnDestroy()
	{
		if(!this.GetComponent<enemy>().isOffline)
			return;

		Debug.Log(transform.position);
		Debug.Log("!!!!!");
		GameObject SpawnEffect = Instantiate(SpawnEffectPrefab,transform.position,Quaternion.identity);
		Destroy(SpawnEffect,4f);
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
			GameObject SmallOne = Instantiate(SmallLorisToSpawn,transform.position,Quaternion.identity);
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
			if(this.GetComponent<enemy>().isOffline)
				SmallOne.GetComponent<enemy>().isOffline = true;
		}
	} 

}
