using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuilding : MonoBehaviour {

    private Vector3 touchPosWorld;

	void Update()
	{
		
        if (Input.touchCount > 0) 
			Moving();
	}


	void Moving()
	{

		Touch touch = Input.GetTouch(0);

		if(touch.phase == TouchPhase.Moved)
		{	
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
			RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			if(hitInformation.collider.name == "Obstacle")
			{
				Debug.Log("OUT");
				return;
			}
			Vector3 temp = touch.position;
			temp = Camera.main.ScreenToWorldPoint(temp);
			temp.z = 10;
			transform.position = temp;
		}
	}
}
