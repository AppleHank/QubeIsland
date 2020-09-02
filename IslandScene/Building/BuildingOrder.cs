using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingOrder : MonoBehaviour {
/* 
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

	void Update()
	{ 
        if (Input.touchCount > 0) {
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
				return;
			if(BuildingManager.isBuildingMode)
				return;

            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			if(hitInformation != null)
			{
				if(CanEdit)
				{
					Debug.Log("CanEdit");
					LongPress(hitInformation);
				}
				if(hitInformation.collider.gameObject == gameObject)
				{

					if(GameObject.Find("BuildingManager").GetComponent<BuildingManager>().EditingBuilding != null)
						return;
					
					CanEdit = true;
					Debug.Log("GameObject");
					LongPress(hitInformation);
					BuildingOrder[] Buildings = FindObjectsOfType<BuildingOrder>();
					foreach(BuildingOrder building in Buildings)
					{
						if(building != this)
						{
							building.GetComponent<BuildingOrder>().enabled = false;
						}
					}
				}
				
			}
		}
	}

	void LongPress(RaycastHit2D hitInformation)
	{
		Touch touch = Input.GetTouch(0);
		Debug.Log(touch.phase);
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

				GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetEditBuilding(gameObject);
				enableMoving = true;
				EnterEditMode(hitInformation);
				GetComponent<BoxCollider2D>().enabled = false;
				this.GetComponent<SpriteRenderer>().color = new Color32 (255,255,255,100);
			}
		}
		if(touch.phase == TouchPhase.Moved & enableMoving)
		{
			Debug.Log(hitInformation.collider.name);
			GetComponent<BoxCollider2D>().enabled = false;
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
			GetComponent<BoxCollider2D>().enabled = true;
			BuildingOrder[] Buildings = FindObjectsOfType<BuildingOrder>();
			foreach(BuildingOrder building in Buildings)
				building.GetComponent<BuildingOrder>().enabled = true;
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



	void EnterEditMode(RaycastHit2D hitInformation)
	{
	//	GameObject EditingBuilding = Instantiate(PreBuilding,transform.position,Quaternion.identity);
	//	gameObject.SetActive(false);
	//	EditingBuilding.SetActive(false);
		UICanvas = GameObject.Find("UICanvas");
		CancelBuildingButton = GameObject.Find("BuildingManager").GetComponent<BuildingManager>().CancelButton;
	//	GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetEditBuilding(gameObject);
	//	GameObject.Find("BuildingManager").GetComponent<BuildingManager>().SetMoveEditBuilding(EditingBuilding);

		ChangeOrderCanvas = GameObject.Find("ChnageOrderCanvas");
		if(UICanvas != null)
			UICanvas.SetActive(false);
		CancelBuildingButton.SetActive(true);

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
*/
}



