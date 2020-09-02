using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoredBuilding : MonoBehaviour {

	public Image BuildingImg;
	public Text NumberText;
	private GameObject Building;
	private BuildingManager buildingmanager;
	private BuildingBlueprint Blueprint;
	private int NumThisEdit;
	private int ClickTime;
	public string TreeType;

	void Start()
	{
		Debug.Log("----------------------Building.name");
		TreeType = Building.name;
	}

	public void SetTreeType()
	{
		TreeType = Building.name;
	}

	public void Stored(string Type)
	{
	//	if(TreeType == Type)
	//	{
	//		NumberText.text = (int.Parse(NumberText.text) + 1).ToString();
	//	}
		if(!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
			gameObject.GetComponent<RectTransform>().anchoredPosition = buildingmanager.LastStroedButtonPosition;
			bool RecoredLastPosition = false;
			foreach(StoredBuilding SB in buildingmanager.StoredBuildingButtons)
			{
				if(RecoredLastPosition)
					buildingmanager.LastStroedButtonPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
				if(SB.TreeType == TreeType)
					RecoredLastPosition = true;
			}
		}
	}

	public void SetNumThisEdit()
	{
		NumThisEdit = int.Parse(NumberText.text);
	}

	public void ResetNum()
	{
		NumberText.text = NumThisEdit.ToString();
		if(int.Parse(NumberText.text) > 0)
			gameObject.SetActive(true);
	}

	public void SetBlueprint(BuildingBlueprint B)
	{
		Blueprint = B;
	}

	public void AssignBuilding(GameObject Building)
	{
		this.Building = Building;
	}

	public string GetBuildingName()
	{
		return Building.name;
	}

	public void AssignBuildingImg(Sprite Img)
	{
		BuildingImg.sprite = Img;
	}

	public void SetNumberText(int num)
	{
		NumberText.text = num.ToString();
	}

	public void SetBM(BuildingManager BM)
	{
		buildingmanager = BM;
	}

	public void PutBuilding()
	{
		buildingmanager.AddContinuousCickButtonNum();
		if(buildingmanager.GetContinuousCickButtonNum() >= 2)
			buildingmanager.ReSetLastTouchedButton();


		Debug.Log("PUT!!!");
		buildingmanager.AssignEditBuilding(Blueprint.Building,Blueprint);
		if(buildingmanager.EditingBuilding != null)
		{
			buildingmanager.EditingBuilding.GetComponent<Building>().UI.SetActive(false);
			buildingmanager.EditingBuilding = null;
			//解決連點兩個的智障玩家
		}
		buildingmanager.AssignEditBuildingOriginInfo(new Vector3(0,0,0),Blueprint);
		Debug.Log("PUTEND!!!");
		int Num = int.Parse(NumberText.text);
		NumberText.text = (Num - 1).ToString();
		if(Num - 1 == 0)
		{
			bool StartReset = false;
			for(int i=0;i<buildingmanager.StoredBuildingButtons.Count;i++)
			{
				Debug.Log(i);
				if(!buildingmanager.StoredBuildingButtons[i].gameObject.activeSelf)
					buildingmanager.LastStroedButtonPosition = buildingmanager.StoredBuildingButtons[i-1].GetComponent<RectTransform>().anchoredPosition;

				if(StartReset)
				{
					buildingmanager.StoredBuildingButtons[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2 (170f,0);
					buildingmanager.StoredBuildingButtons[i-1] = buildingmanager.StoredBuildingButtons[i];
					if(i == buildingmanager.StoredBuildingButtons.Count-1)
					{
						gameObject.GetComponent<RectTransform>().anchoredPosition = buildingmanager.StoredBuildingButtons[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(170f,0);
						buildingmanager.StoredBuildingButtons[i] = this;
					}
				}
				if(buildingmanager.StoredBuildingButtons[i].TreeType == TreeType)
					StartReset = true;
			}
			gameObject.SetActive(false);
//			buildingmanager.StoredBuildingButtons;
		}
		buildingmanager.SetLastTouchedStoreButton(this);
		buildingmanager.SetPutStoredBuilding();
	}


}
