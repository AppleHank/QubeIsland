using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour {

	public GameObject HealthPortion;

	public void HealMonster()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestPath = null;
		foreach (Collider collider in colliders){
			Debug.Log(collider);
			Debug.Log(collider.tag);
			if(collider.tag == "path")
			{	
				float distanceToPath = Vector3.Distance(transform.position,collider.transform.position);
				if (distanceToPath < shortestDistance)
				{
					shortestDistance = distanceToPath;
					nearestPath = collider.gameObject;
				}
			}
		}
		Debug.Log(nearestPath);

//		nearestPath.GetComponent<SpriteRenderer>().color = new Color32 (255,255,0,255);
		GameObject Portion = Instantiate(HealthPortion,nearestPath.transform.position,Quaternion.identity);
		Portion.GetComponent<TurtlePortion>().SetPath(nearestPath);
	}

}
