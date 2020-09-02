using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottonUIControl : MonoBehaviour {

	private bool BuildingOpened;
	private bool ShoppingMallOpend;
	private bool IllustrationOpend;
	public GameObject ShoppingMall;
	public GameObject Building;
	public GameObject Illustration;
	public RectTransform ShoppingUI;
	public RectTransform BuildingUI;
	public RectTransform IllustrationUI;
	private Vector3 BuildUIOriginPos;
	private Vector3 ShoppingMallOriginPos;
	private Vector3 IllustrationMallOriginPos;

	void Start()
	{
		BuildUIOriginPos = BuildingUI.anchoredPosition;
		ShoppingMallOriginPos = ShoppingUI.anchoredPosition;
		IllustrationMallOriginPos = IllustrationUI.anchoredPosition;
	}

	public void ResetUI(int num)
	{
		if(num == 1)
		{
			BuildingUI.anchoredPosition = BuildUIOriginPos;
			BuildingOpened = false;
		}
		else if(num == 2)
		{
			ShoppingUI.anchoredPosition = ShoppingMallOriginPos;
			ShoppingMallOpend = false;
		}
		else
		{
			IllustrationUI.anchoredPosition = IllustrationMallOriginPos;
			IllustrationOpend = false;
		}
	}

	public void ClickBuilding(RectTransform UI)
	{
		if(BuildingOpened)
		{
			ResetUI(1);
			Building.SetActive(false);
			BuildingOpened = false;
		}
		else
		{
			Building.SetActive(true);
			BuildingOpened = true;
			UI.anchoredPosition += new Vector2(0,15);
		}
		
		if(ShoppingMallOpend)
		{
			ResetUI(2);
			ShoppingMallOpend = false;
			ShoppingMall.SetActive(false);
		}	
		if(IllustrationOpend)
		{
			ResetUI(3);
			IllustrationOpend = false;
			Illustration.SetActive(false);
		}
		
	}

	public void ClickShoppingMall(RectTransform UI)
	{
		if(ShoppingMallOpend)
		{
			ResetUI(2);
			ShoppingMall.SetActive(false);
			ShoppingMallOpend = false;
		}
		else
		{
			ShoppingMall.SetActive(true);
			UI.anchoredPosition += new Vector2(0,15);
			ShoppingMallOpend = true;
		}

		if(BuildingOpened)
		{
			ResetUI(1);
			BuildingOpened = false;
			Building.SetActive(false);
		}
		if(IllustrationOpend)
		{
			ResetUI(3);
			IllustrationOpend = false;
			Illustration.SetActive(false);
		}
	}
	public void ClickIllustration(RectTransform UI)
	{
		if(IllustrationOpend)
		{
			ResetUI(3);
			Illustration.SetActive(false);
			IllustrationOpend = false;
		}
		else
		{
			Illustration.SetActive(true);
			UI.anchoredPosition += new Vector2(0,15);
			IllustrationOpend = true;
		}

		if(BuildingOpened)
		{
			ResetUI(1);
			BuildingOpened = false;
			Building.SetActive(false);
		}
		if(ShoppingMallOpend)
		{
			ResetUI(2);
			ShoppingMallOpend = false;
			ShoppingMall.SetActive(false);
		}	
	}

	public void ChangePositionTutor(RectTransform UI)
	{
		UI.anchoredPosition += new Vector2(0,15);
	}




}
