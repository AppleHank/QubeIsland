using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingBuilding : MonoBehaviour {

	public GameObject UICanvas;
	public GameObject CancelBuildingButton;
	public GameObject ChangeOrderCanvas;
	public EditOrder EditOrder;
	public GameObject PreBuilding;
	private bool StillTouch;
	private bool enableMoving;
    private Vector3 touchPosWorld;
	private bool CanEdit;
	public bool isMoving;
	private float StartTouchTime;
	private float TouchTime;
	private float FinishTime;
	private GameObject EditingBuilding;
	public BuildingManager buildingmanager;
 
     //Change me to change the touch phase used.
	private TouchPhase touchPhase = TouchPhase.Ended;

	void Start()
	{
		BuildNode[] BuildNodes = FindObjectsOfType<BuildNode>();
		foreach(BuildNode BuildNode in BuildNodes)
		{
			Physics2D.IgnoreCollision(BuildNode.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}
		private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
		}

 	void Update()
	{ 
       if (Input.touchCount > 0) {
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
				return;

			if(IsPointerOverUIObject())
				return;
//			Debug.Log(BuildingManager.isBuildingMode);
	//		if(BuildingManager.isBuildingMode)
	//			return;

            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			
			if(hitInformation.collider!= null)
			{
				if(BuildingManager.isEditMode)
				{
		//			if(hitInformation.collider.gameObject.name != "Cave(Clone)" & hitInformation.collider.gameObject.name != "WareHouse(Clone)")
		//			{
						if(hitInformation.collider.name == "Canoe(Clone)" | hitInformation.collider.tag == "Tree")
						{
							EditBuildingVersion2(hitInformation);
							return;
						}

						else if(hitInformation.collider.gameObject.tag == "Building")
						{
								hitInformation.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
								touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
								touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
								hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
							
						}
		//3/18


					/* 	if(CanEdit)
						{
							Debug.Log("CanEdit");
							SecondLongPress(EditingBuilding);
						}*/
		//				Debug.Log(BuildingManager.isEditMode);

		//				Debug.Log(hitInformation.collider);

						if(BuildingManager.isEditNow)
						{
							EditBuildingVersion2(hitInformation);
							return;
						}
						if(hitInformation.collider.gameObject.tag == "BuildNode" )
						{ 
			//				if(GameObject.Find("BuildingManager").GetComponent<BuildingManager>().EditingBuilding != null)
			//					return;
							if(BuildingManager.isEditMode)
								EditBuildingVersion2(hitInformation);
					//		LongPress(hitInformation);
						}
				//	}
				}
			}
		}
	}

	void EditBuildingVersion2(RaycastHit2D hitInformation)
	{
		Touch touch = Input.GetTouch(0);
//		Debug.Log(touch.phase);
		if (touch.phase == TouchPhase.Began)
		{
			Debug.Log("BEGAN");
			BuildingManager.isEditNow = true;
			if(hitInformation.collider.gameObject.name == "Canoe(Clone)" | hitInformation.collider.tag == "Tree")
				hitInformation.collider.gameObject.GetComponent<Building>().GetNode().SetPreBuildingForEdit();
			else
				hitInformation.collider.gameObject.GetComponent<BuildNode>().SetPreBuildingForEdit();

//			GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetEditBuilding(hitInformation.collider.gameObject);
		}
		if(touch.phase == TouchPhase.Moved)
		{
			isMoving = true;
		}
		if (touch.phase == TouchPhase.Ended)
		{
			isMoving = false;
			BuildingManager.isEditNow = false;
			Debug.Log("ENded");
			buildingmanager.ResetContinuousCickButtonNum();
			buildingmanager.Build();
			if(!buildingmanager.GetPositionError())
				buildingmanager.EditResetPreBuilding();
//			GameObject.Find("BuildingManager").GetComponent<BuildingManager>().EditResetPreBuilding();
		}
	}

/* 	void LongPress(RaycastHit2D hitInformation)
	{
		Touch touch = Input.GetTouch(0);
		Debug.Log(touch.phase);
		Debug.Log("EnterFIrst");
		if (touch.phase == TouchPhase.Began)
		{
			
			StartTouchTime = Time.time;
			FinishTime = 0.5f;
			if(hitInformation.collider.name == "Obstacle")
				isMoving = false;
			else
				isMoving = true;
		}
		if (touch.phase == TouchPhase.Stationary)
		{
			TouchTime = Time.time - StartTouchTime;
			
			if(TouchTime > FinishTime)
			{
				Debug.Log("OVER"+FinishTime);
				CanEdit = true;
				GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetEditBuilding(hitInformation.collider.gameObject);
				enableMoving = true;
				EnterEditMode(hitInformation);
				EditingBuilding.GetComponent<BoxCollider2D>().enabled = false;
				EditingBuilding.GetComponent<SpriteRenderer>().color = new Color32 (255,255,255,100);
			}
		}
		if(touch.phase == TouchPhase.Moved & enableMoving)
		{
			Debug.Log(hitInformation.collider.name);
			EditingBuilding.GetComponent<BoxCollider2D>().enabled = false;
			if(hitInformation.collider.name == "Obstacle")
			{
				Debug.Log("OUT");
				return;
			}
			Debug.Log("!!!!!!!!!!!!!!!!");
			Vector3 temp = touch.position;
			temp.z = 10;
			GameObject.Find("BuildingManager").GetComponent<BuildingManager>().EditingBuilding.transform.position = Camera.main.ScreenToWorldPoint(temp);
		}
		if (touch.phase == TouchPhase.Ended)
		{
			isMoving = false;
			StillTouch = false;
			EditingBuilding.GetComponent<BoxCollider2D>().enabled = true;
			BuildingOrder[] Buildings = FindObjectsOfType<BuildingOrder>();
			foreach(BuildingOrder building in Buildings)
				building.GetComponent<BuildingOrder>().enabled = true;
			TouchTime = 0f;
		}
	}*/

	void SecondLongPress(GameObject EBuilding)
	{
		Touch touch = Input.GetTouch(0);
		Debug.Log("EnterSecond");
		if(touch.phase == TouchPhase.Moved & enableMoving)
		{
			EditingBuilding.GetComponent<BoxCollider2D>().enabled = false;
			
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			if(hitInformation.collider.name == "Obstacle")
			{
				Debug.Log("OUT");
				return;
			}
			
			isMoving = true;
			Vector3 temp = touch.position;
			temp.z = 10;
			GameObject.Find("BuildingManager").GetComponent<BuildingManager>().EditingBuilding.transform.position = Camera.main.ScreenToWorldPoint(temp);
		}
		if (touch.phase == TouchPhase.Ended)
		{
			isMoving = false;
			StillTouch = false;
			EditingBuilding.GetComponent<BoxCollider2D>().enabled = true;
			TouchTime = 0f;
		}
	}
	

	IEnumerator DecideLongTouch(RaycastHit2D hitInformation)
	{
		yield return new WaitForSeconds(.5f);
		if(FindObjectsOfType<BuildingManager>()[0].EditingBuilding != null)
			yield break;
		Debug.Log("Still");
		if(StillTouch)
		{
			
		}

	}

	public void ResetEnableMoving()
	{
		enableMoving = false;
	}



	public void EnterEditMode(RaycastHit2D hitInformation)
	{
	//	GameObject EditingBuilding = Instantiate(PreBuilding,transform.position,Quaternion.identity);
	//	gameObject.SetActive(false);
	//	EditingBuilding.SetActive(false);
		UICanvas = GameObject.Find("UICanvas");
	//	CancelBuildingButton = GameObject.Find("BuildingManager").GetComponent<BuildingManager>().CancelButton;
	//	GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetEditBuilding(gameObject);
	//	GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetMoveEditBuilding(EditingBuilding);

		ChangeOrderCanvas = GameObject.Find("ChnageOrderCanvas");
		if(UICanvas != null)
			UICanvas.SetActive(false);
	//	CancelBuildingButton.SetActive(true);

		GameObject ReverseEditBuildingButton = GameObject.Find("BuildingManager").GetComponent<BuildingManager>().ReverseEditBuildingButton;

		ReverseEditBuildingButton.SetActive(true);

		ChangeOrderCanvas.GetComponent<Canvas>().sortingLayerName = "Player";

		EditOrder = GameObject.FindObjectsOfType<EditOrder>()[0].GetComponent<EditOrder>();
		
		EditOrder.NowChangeBuilding = hitInformation.collider.gameObject;
	}

	public void ResetCanEdit()
	{
		CanEdit = false;
	}

}
