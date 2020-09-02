using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildNode : MonoBehaviour {

	public GameObject Building;
	public BuildingBlueprint BuildingBlueprint;
	public GameObject PreBuilding;
	private Vector3 RaycastPos;
	private BuildingManager buildingmanager;
	private Vector3 OriginPos;
	private BuildingBlueprint OriginBlueprint;
    private Vector3 touchPosWorld;
	private GameObject TempBuilding;
	private int flip;
	private bool isNowPointNode;
	private List<BuildNode> BrotherNodeList = new List<BuildNode>();
	private bool CanMove;
	public bool CanBuildCanoe;
	public bool Canoeflip;
	private string OriginTreeName;
	public int Order;

	public void SetBM(BuildingManager BM)
	{
		buildingmanager = BM;
	}

	public void SetBroNode(BuildNode Node)
	{
		BrotherNodeList.Add(Node);
	}

	public void SetPreBuildingForEdit()
	{
		Debug.Log("SETPRE");
		if(Building != null)
		{	
			buildingmanager.ReleasePutStoredBuilding();//prevent user from touching building after touch Storedbuildingbutton to enter wrong decition in buildlingmanager.SavePositon(if tree)

			if(buildingmanager.GetPositionError())
				return;

			if(Building.tag == "Tree")
			{
				buildingmanager.SetLastTreeName(Building.name);
				Debug.Log("SetName"+Building.name);
			}

			if(Building.GetComponent<Building>().UI.activeSelf)
			{
				buildingmanager.AssignEditBuilding(BuildingBlueprint.Building,BuildingBlueprint);
				isNowPointNode = true;
				Debug.Log("SetPrebuildingOver");
			}
			else
			{
				if(buildingmanager.EditingBuilding != null)
				{
					buildingmanager.EditingBuilding.GetComponent<Building>().UI.SetActive(false);
				}
				Debug.Log("OOOOOOOOOOOOORRRRRRRRRRRIIIIIIIIIIIIGN");
				buildingmanager.AssignEditBuildingOriginInfo(OriginPos,OriginBlueprint);
				if(Building.name == "Canoe(Clone)")
					buildingmanager.AssignCanoeOriginNode(Building.GetComponent<Building>().GetNode());
				buildingmanager.SetEditingBuilding(Building);
				buildingmanager.isFlip = Building.GetComponent<SpriteRenderer>().flipX;
				Building.GetComponent<Building>().UI.SetActive(true);
				if(buildingmanager.GetContinuousCickButtonNum() > 0)
					buildingmanager.ReSetLastTouchedButton();
			}
			
			
//			TempBuilding = Instantiate(Building,Building.transform.position,Quaternion.identity);
//			TempBuilding.GetComponent<SpriteRenderer>().color = new Color32 (255,255,255,100);
			Debug.Log("TOUCH");
		}
	}

	public void SetOriginInfo(Vector3 position,BuildingBlueprint B,int flip)
	{	
		this.flip = flip;
		OriginPos = position;
		OriginBlueprint = B;
	}

	public string GetOriginTreeName()
	{
		return OriginTreeName;
	}

	public void SetOriginTreeName(string name)
	{
		OriginTreeName = name;
	}
	

	public BuildingBlueprint GetOriginBlueprint()
	{

		return OriginBlueprint;
	}
	public Vector3 GetOriginPos()
	{
		return OriginPos;
	}
	
	void OnMouseEnter()
	{
		Debug.Log("enter");

		if(buildingmanager.GoBack)
			return;

	//	if(buildingmanager.GetPositionError() & Building != null)
	//		return;

/*/		if(buildingmanager.GetPreBuilding() == null)
		{
			if(!BuildingManager.isEditMode)
				return;
			else
			{
				Debug.Log(BuildingManager.isEditNow);
				if(!BuildingManager.isEditNow)
				{
					if(BuildingBlueprint == null)
						return;
					buildingmanager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
					buildingmanager.AssignEditBuilding(BuildingBlueprint.Building,BuildingBlueprint);
					Debug.Log("SET!!");
					return;
				}
			}
		}*/

		if(buildingmanager.GetPreBuilding() == null)
		{
			Debug.Log("1");
			return;
		}

//		if(Building != null)
//			return;
		
/* 		if(BuildingManager.isEditNow & Building != null)//First Node
			if(BuildingBlueprint == buildingmanager.GetBuilding())
			{
				Debug.Log("DESTROY");
				Collider2D[] colliders2 = Physics2D.OverlapCircleAll(Building.transform.position, BuildingBlueprint.NodeNum);
				foreach (Collider2D collider in colliders2)
				{
					if (collider.tag == "BuildNode")
					{
						if(collider.gameObject.GetComponent<BuildNode>().Building == Building)
						{
							Debug.Log(collider.gameObject);
							collider.GetComponent<BuildNode>().Building = null;
						}
					}
				}
				Destroy(Building);
			}*/




		buildingmanager.SetRayCastPos(transform.position);

		Vector3 position = transform.position;
//		GameObject Fuck = GameObject.Find(buildingmanager.GetPreBuilding().name+"(Clone)");
//		if(Fuck != null)
//		{
//			Debug.Log(Fuck);
//			Destroy(Fuck);
//		}
		

		buildingmanager.DestroyEditBuildingList(BuildingBlueprint);
		if(buildingmanager.EditingBuilding != null)
		{
			Debug.Log("DESSSSSSSSSSSSTRRRRRRRRRRRRRRRRRRIIIIIIIIIIYYYYYYYYYYYY");
			Destroy(buildingmanager.EditingBuilding);
		}
		PreBuilding = Instantiate(buildingmanager.GetPreBuilding(),position,Quaternion.identity);
		
		buildingmanager.AddEditingBuilding(PreBuilding);
		Building PreBuildingScript = PreBuilding.GetComponent<Building>();
		if(buildingmanager.isFlip)
			PreBuilding.GetComponent<SpriteRenderer>().flipX = true;
		if(buildingmanager.GetPreBuilding().name == "Canoe" | buildingmanager.GetPreBuilding().name == "PreCanoe")
		{
			Vector3 CanoeTemp = transform.position;
			if(Canoeflip)
			{
				PreBuilding.GetComponent<SpriteRenderer>().flipX = true;
				CanoeTemp.x -= 7;
				CanoeTemp.y += 1.6f;
				PreBuildingScript.Foundations[0].transform.position = PreBuildingScript.CanoeFlipFoundationPosition.transform.position;
			}
			else
			{
				PreBuilding.GetComponent<SpriteRenderer>().flipX = false;
				CanoeTemp.x += 6.7f;
				CanoeTemp.y += 1.9f;
				PreBuildingScript.Foundations[0].transform.position = PreBuildingScript.CanoeNoFlipFoundationPosition.transform.position;
			}
			PreBuilding.transform.position = CanoeTemp;
		}


		if(buildingmanager.GetPreBuildingNow() != null)
		{
	//		Debug.Log(PreBuilding.name);
	//		Debug.Log(buildingmanager.GetPreBuildingNow().name);
			if(buildingmanager.GetPreBuildingNow().name == PreBuilding.name)
			{
//				Debug.Log("Destroy2" + buildingmanager.GetPreBuildingNow().name);
//				Destroy(buildingmanager.GetPreBuildingNow());
			}
		}
		if(!BuildingManager.isEditMode)
		{
			buildingmanager.AssignBuildPreBuilding(PreBuilding);

		}
		else
		{
			buildingmanager.AssignEditBuildingNow(PreBuilding);
			PreBuilding.GetComponent<SpriteRenderer>().color = new Color32 (255,255,255,100);
			PreBuilding.GetComponent<BoxCollider2D>().enabled = false;
		}

		RaycastPos = transform.position;
		Vector3 temp0 = transform.position;
		temp0.x += 5;

		
		if(buildingmanager.GetBuilding().NodeNum != 1)
			RaycastPos = temp0;

			Debug.Log(buildingmanager.GetBuilding().Building.tag);
		Vector3 temp = transform.position;
		temp.x += 5;
		
		if(PreBuildingScript.isForest)
		{
			temp.x += 5;
			Debug.Log("@#@#@!#@#@");
		}
		if(buildingmanager.GetBuilding().Building.tag == "Tree")
		{
			temp.y += 5;
			temp.x -= 5;
			if(PreBuildingScript.isNewTree)
				temp.y -= 3;
			if(PreBuilding.name == "PreTrebleTree(Clone)")
				temp.y -= 4.5f;

			PreBuilding.transform.position = temp;
		}
			

 		if(buildingmanager.GetBuilding().NodeNum == 4)
		{

			if(PreBuilding.name == "HolyChurch(Clone)" | PreBuilding.name == "PreHolyChurch(Clone)")
				temp.y += 2f;
			if(PreBuilding.name == "UpgradeHolyChurch(Clone)")
			{
				temp.y += .5f;
				temp.x -= .9f;
			}
			PreBuilding.transform.position = temp;
 			Collider2D[] colliders = Physics2D.OverlapCircleAll(RaycastPos, buildingmanager.GetBuilding().NodeNum);	
			int RaycastNodeNum = 0;
			foreach (Collider2D collider in colliders)
			{
				if (collider.tag == "BuildNode")
					RaycastNodeNum += 1;
			}
			if(RaycastNodeNum != buildingmanager.GetBuilding().NodeNum)//Build position over border
			{
				Debug.Log(RaycastNodeNum);
				Debug.Log("Number error!");
			//	Destroy(PreBuilding);
				PreBuilding.transform.position = buildingmanager.GetLastPreBuildingPos();
			//	PreBuilding = Instantiate(buildingmanager.GetPreBuilding(),buildingmanager.GetLastPreBuildingPos(),Quaternion.identity);
			//	buildingmanager.AssignBuildPreBuilding(PreBuilding);
			}
		}

		buildingmanager.SetLastPreBuildingPos(PreBuilding.transform.position);

		foreach(GameObject Foundation in PreBuildingScript.Foundations)
		{
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(Foundation.transform.position,.1f);
			foreach (Collider2D collider in colliders2)
			{
				if (collider.tag == "BuildNode")
				{
					if(buildingmanager.GetPreBuilding().name == "Canoe" | buildingmanager.GetPreBuilding().name == "PreCanoe")
					{
						if(collider.gameObject.GetComponent<BuildNode>().Building != null)
						{
							if(collider.gameObject.GetComponent<BuildNode>().Building.name != PreBuilding.name)
							{
								Debug.Log("Beacuse has BUilding");
								Debug.Log("Node:" + collider.gameObject);
								Debug.Log("Building:" +collider.gameObject.GetComponent<BuildNode>().Building.name );
								PreBuilding.GetComponent<SpriteRenderer>().color = new Color32 (255,50,50,135);
								if(BuildingManager.isEditMode)
									buildingmanager.SetPositionError(true);
								return;
							}
						}
						else
						{
							if(!collider.gameObject.GetComponent<BuildNode>().CanBuildCanoe)
							{
								Debug.Log("Beacuse Cant BUild Canoe");
								PreBuilding.GetComponent<SpriteRenderer>().color = new Color32 (255,50,50,135);
								if(BuildingManager.isEditMode)
									buildingmanager.SetPositionError(true);
								return;
							}	
						}
					}
					else if(collider.gameObject.GetComponent<BuildNode>().Building != null)
					{
						Debug.Log(buildingmanager.GetBuilding().NodeNum);
						if(collider.gameObject.GetComponent<BuildNode>().Building.name != PreBuilding.name)
						{
							PreBuilding.GetComponent<SpriteRenderer>().color = new Color32 (255,50,50,135);
							if(BuildingManager.isEditMode)
								buildingmanager.SetPositionError(true);
							return;
						}
					}
				}
			}
		}

		buildingmanager.SetPositionError(false);
	}


	void OnMouseExit()
	{
		Debug.Log("Exit");
		if(IsPointerOverUIObject())
		{
			Debug.Log("OPINTUI");
			return;
		}

		if(Input.touchCount > 0)
		{
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
			RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			
			if(hitInformation.collider != null)
			{
				Debug.Log(hitInformation.collider.name);
				if(hitInformation.collider.name == "Obstacle" |  hitInformation.collider.tag == "Building" | hitInformation.collider.tag == "Tree")
					return;
			}
		}
	//	if(buildingmanager.GetPositionError() & Building != null)
	//		return;

		if(TempBuilding != null)
		{
			Debug.Log("---------------------------------------");
			Destroy(TempBuilding);
		}

		if(PreBuilding != null)
		{
				Debug.Log("Destroy"+PreBuilding.name);
				Destroy(PreBuilding);
		}

		if(Building != null & BuildingManager.isEditMode)
		{
			Debug.Log("================");
			if(BuildingBlueprint == buildingmanager.GetBuilding())
			{
				Debug.Log("-------------");
				if(isNowPointNode)
				{
					BuildingBlueprint = null;
					Destroy(Building);
					Debug.Log("Reset"+Building.name);
					isNowPointNode = false;
				}
			}
		}
	
		
	//	Debug.Log("!!!");
	}

	public void SetFlip(int Flip)
	{
		flip = Flip;
	}

	public int GetFlip()
	{
		return flip;
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
		}
}
