using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public int radius = 30;
	public Prop Prop;

	void Update ()
	{
		fireMouse();
	}

	public void fireMouse ()
	{
		Vector3 temp = Input.mousePosition;
		temp = Camera.main.ScreenToWorldPoint(temp);
		temp.z = -2;
		transform.position = temp;
	}

	void OnMouseDown ()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius);
		foreach (Collider2D collider in colliders)
		{
			Debug.Log("sadsadsdas");
			if (collider.tag == "enemy")
			{
				Debug.Log("enemyHIT");
				enemy enemy = collider.GetComponent<enemy>();
				enemy.TakeDamage(false,false,800,0);
			}
		}
		Prop.fire();
		Destroy(gameObject);
	}
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
