using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour {

	private bool invisiblestatus;
	public float offLineinvisibleTime;
	public float offLineinvisibleInterval;
	public float offLineinvisibleRate;
	private bool isInvisible;

	void Update()
	{
		if(isInvisible)
			this.GetComponent<SpriteRenderer>().color = new Color32(255,255,0,100);
	}

	public void SetInvisible()
	{
		invisiblestatus = !invisiblestatus;
	}

	public bool GetInvisible()
	{
		return invisiblestatus;
	}

	void Start()
	{
		if(this.GetComponent<enemy>().isOffline)
			StartCoroutine(Invisible());
		else
			this.enabled = false;
	}

	IEnumerator Invisible()
	{
		while(gameObject != null)
		{
			yield return new WaitForSeconds(offLineinvisibleInterval);
			if(GetInvisible())
				yield return new WaitForSeconds(offLineinvisibleTime);
			float IRate = Random.Range(0,100);
			if(IRate <= offLineinvisibleRate)
			{ 
				SetInvisible();
				isInvisible = true;
				this.tag = "Tree";
				StartCoroutine(Visible());
			}
			else		
				SetInvisible();
		}
	}

	IEnumerator Visible()
	{
		yield return new WaitForSeconds(offLineinvisibleTime);
		this.GetComponent<SpriteRenderer>().color = new Color32 (255,255,0,255);
		this.tag = "enemy";
		isInvisible = false;
	}


}
