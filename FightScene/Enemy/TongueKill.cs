using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueKill : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "enemy")
		{
			Debug.Log(collision.gameObject.name);
			collision.gameObject.GetComponent<enemy>().ReadyToDestroy();
		}
	}
	void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "enemy")
		{
			Debug.Log(collision.gameObject.name);
			collision.gameObject.GetComponent<enemy>().ReadyToDestroy();
		}
	}
}
