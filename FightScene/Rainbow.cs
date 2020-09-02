using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour {

	private GameObject Horse;

	public void SetHorse(GameObject Horse)
	{
		this.Horse = Horse;
	}

	// Update is called once per frame
	void Update () {
		Vector3 dir = Horse.transform.position - transform.position;
		transform.Translate(dir.normalized * 30 * Time.deltaTime, Space.World);
	}
}
