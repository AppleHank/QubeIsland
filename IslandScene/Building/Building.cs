using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public GameObject UI;
	public List<GameObject> Foundations = new List<GameObject>();
	private BuildingManager buildingmanager;
	private BuildNode Node;
	public GameObject CanoeFlipFoundationPosition;
	public GameObject CanoeNoFlipFoundationPosition;
	private string TreeType;
	public string TreeTypeToWrite;
	public bool isNewTree;
	public bool isForest;

	public void CloseUI ()
	{
		buildingmanager.CloseEditingBuildingUI();
	}

	public void SetNode(BuildNode Node)
	{
		this.Node = Node;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = Node.Order;
	}

	public BuildNode GetNode()
	{
		return Node;
	}

	public void SetTreeType(string name)
	{
		TreeType = name;
	}


	public void GoBack()
	{
		Debug.Log("Building GOBack");
		if(buildingmanager.GetPutStoredBuilding() == true)
		{
			Debug.Log("TRUEEEEEEE");
			buildingmanager.ResetLastTouchedStoredButtonNum();
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("False");
			buildingmanager.GoBack = true;
			buildingmanager.EditingBuildingGoBack();
		}

	}

	public void ReverseBuilding()
	{
//		buildingmanager.isFlip = !buildingmanager.isFlip;
		buildingmanager.ReversePreBuilding();
	}

	public void AssignBuildingManager(BuildingManager BM)
	{
		buildingmanager = BM;
	}

	void Start()
	{
		if(UI != null)
			UI.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	public void StoreBuilding()
	{
		Debug.Log("in B:"+gameObject.name);
		Debug.Log(buildingmanager);
		buildingmanager.StoreBuilding(gameObject,TreeTypeToWrite,Node.BuildingBlueprint);
		Debug.Log("Over");
	}
}
