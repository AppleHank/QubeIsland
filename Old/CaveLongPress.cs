using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CaveLongPress : MonoBehaviour {

	public PetView PetView;
    private Vector3 touchPosWorld;
	private bool StillTouch;
	public GameObject PetViewG;
	public GameObject CaveView;
	private RaycastHit2D PrehitInformation;
	private float StartTouchTime;
	private float TouchTime;
	private float FinishTime;

	void Update()
	{
		if (Input.touchCount > 0) {
			
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
 
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
 
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
				foreach(GameObject Building in Buildings)
				{
					Building.GetComponent<BoxCollider2D>().enabled = false;
				}
				StartTouchTime = Time.time;
				Debug.Log("STARTTOUCTIMe = " + StartTouchTime);
				FinishTime = 0.5f;
				Debug.Log("FInishTime=" +  FinishTime);
	//			StillTouch = true;
	//			StartCoroutine(DecideLongTouch(hitInformation));
	//			PrehitInformation = hitInformation;
			}
			if (touch.phase == TouchPhase.Stationary)
			{
				TouchTime = Time.time - StartTouchTime;
				Debug.Log(TouchTime);
				if(TouchTime > FinishTime)
					LongPress(hitInformation);
			}
			if (touch.phase == TouchPhase.Ended)
			{	
				GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
				foreach(GameObject Building in Buildings)
				{
					Building.GetComponent<BoxCollider2D>().enabled = true;
				}
				TouchTime = 0f;
			}

			
		}
	}

	void LongPress(RaycastHit2D hitInformation)
	{
		GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
		foreach(GameObject Building in Buildings)
		{
			Building.GetComponent<BoxCollider2D>().enabled = true;
		}
		
		Debug.Log(hitInformation.collider.name);
		if(hitInformation.collider.name == "GoatButton")
		{
			PetView.WatchPet(1);
			CaveView.SetActive(false);
			PetViewG.SetActive(true);
		}
		else if(hitInformation.collider.name == "CatButton")
		{
			PetView.WatchPet(2);
			CaveView.SetActive(false);
			PetViewG.SetActive(true);
		}
		else if(hitInformation.collider.name == "PigButton")
		{
			PetView.WatchPet(3);
			CaveView.SetActive(false);
			PetViewG.SetActive(true);
		}
		else if(hitInformation.collider.name == "BearCatButton")
		{
			PetView.WatchPet(4);
			CaveView.SetActive(false);
			PetViewG.SetActive(true);
		}
		else if(hitInformation.collider.name == "ChickenButton")
		{
			PetView.WatchPet(5);
			CaveView.SetActive(false);
			PetViewG.SetActive(true);
		}
		else if(hitInformation.collider.name == "HorseButton")
		{
			PetView.WatchPet(6);
			CaveView.SetActive(false);
			PetViewG.SetActive(true);
		}
		else if(hitInformation.collider.name == "TutorCave(Clone)")
			PetView.WatchPet(1);
	}
}
