using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlePortion : MonoBehaviour {

	public int HealtTime;
	public int RecoverHealth;
	private GameObject TurtlePath;
	public GameObject HealEffectPrefab;

	public void SetPath(GameObject Path)
	{
		TurtlePath = Path;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Debug.Log("enemyhela");
		if(col.gameObject.tag == "enemy"| col.gameObject.tag == "Tree")
		{
			Debug.Log("Heal");
			enemy E = col.gameObject.GetComponent<enemy>();

			E.healthNow += RecoverHealth;
			if(E.healthNow > E.health)
				E.healthNow = E.health;
			E.healthBar.fillAmount = E.healthNow / E.health;
			HealtTime -= 1;

			GameObject HealEffect = Instantiate(HealEffectPrefab,transform.position,Quaternion.identity);
			HealEffect.transform.parent = E.transform;
			HealEffect.transform.localScale = new Vector3(3,3,3);
			HealEffect.transform.position += new Vector3(0,10,0);


			if(HealtTime <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
