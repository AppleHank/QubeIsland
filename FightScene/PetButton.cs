using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetButton : MonoBehaviour {

	public int Petnum;
	private bool NowAssignIsland;
	public PetView petview;
	public GameObject PetViewPanel;
	public GameObject CaveViewCanvas;
    private Vector3 touchPosWorld;
	private bool StillTouch;

	void Update()
	{
		if (Input.touchCount > 0) {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
 
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
 
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			if(hitInformation.collider.tag == "PetButton")
			{
				Debug.Log("PETEEEEEEEEEEEEEEEEEEEEEEEEEE");
				DecideAction();
			}
				Debug.Log("AAAAA");

		}
	}

	void DecideAction()
	{
		Debug.Log("AAA");
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began)
		{
			Debug.Log(Time.time);
			StillTouch = true;
			StartCoroutine(DecideLongTouch());
		}
		if(touch.phase == TouchPhase.Stationary)
		{
			if(NowAssignIsland)
			{
				CaveManager cavemanager = FindObjectsOfType<CaveManager>()[0].GetComponent<CaveManager>();
				cavemanager.PetOnIsland(Petnum);
			}
		}
		if (touch.phase == TouchPhase.Ended)
		{
			if(!NowAssignIsland)
			{
				petview.WatchPet(Petnum);
				PetViewPanel.SetActive(true);
				CaveViewCanvas.SetActive(false);
			}
			StillTouch = false;
			NowAssignIsland = false;
		}
	}
	
	IEnumerator DecideLongTouch()
	{
		yield return new WaitForSeconds(0.5f);
		if(StillTouch)
		{
			NowAssignIsland = true;
			CaveManager cavemanager = FindObjectsOfType<CaveManager>()[0].GetComponent<CaveManager>();
			cavemanager.PetOnIsland(Petnum);
		}

	}
}
