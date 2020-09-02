using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseAssistance : MonoBehaviour {

	public GameObject RainbowPrefab;

	public void Jump(GameObject Horse)
	{
		StartCoroutine(HelpJump(Horse));
	}

	IEnumerator HelpJump(GameObject Horse)
	{
		yield return new WaitForSeconds(.1f);
		Debug.Log("StartJump");
		GameObject Rainbow = Instantiate(RainbowPrefab,Horse.transform.position,Quaternion.identity);
		Rainbow.GetComponent<Rainbow>().SetHorse(Horse);
		Horse.transform.position = gameObject.transform.position;
		Destroy(Rainbow,.6f);
		Destroy(gameObject);
	}
}
