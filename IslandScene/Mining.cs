using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mining : MonoBehaviour {

	public static int MiningTime;
	public GameObject MiningPanel;
	public Mine FirstMine;
	public Mine SecondMine;
	public Mine ThirdMine;
	public Mine FourMine;
	public Mine FiveMine;
	public Mine SixMine;
	public Mine SevenMine;
	public Mine EightMine;
	public Mine NineMine;
	private List<int> usedValues = new List<int>();
	private int MineOpened;
	public GameObject ClosePanel;
	private List<int> AddNumTODB = new List<int>();
	public SetMaterial MaterialManager;
	public GameObject NormalProgress;
	private int TotalMetalNum = 0;
	public Text OverText;

	public void Mine(GameObject MineButton)
	{
		MineButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0,15);
		NormalProgress.SetActive(false);
	//	if(MiningTime 5)
	//		return;
		MineOpened = 0;
		FirstMine.NumText.text = "挖礦";
		SecondMine.NumText.text = "挖礦";
		ThirdMine.NumText.text = "挖礦";
		FourMine.NumText.text = "挖礦";
		FiveMine.NumText.text = "挖礦";
		SixMine.NumText.text = "挖礦";
		SevenMine.NumText.text = "挖礦";
		EightMine.NumText.text = "挖礦";
		NineMine.NumText.text = "挖礦";
		MiningPanel.SetActive(true);
		usedValues.Clear();
	}


	public void AssignNum(Mine mine)
	{
		int num = GetMineNum(1,10);
		mine.gameObject.SetActive(false);
		mine.NumText.text = num.ToString();
		AddNumTODB.Add(num);
		TotalMetalNum += num;
		if(MineOpened >= 3)
		{
			OverText.text = "恭喜獲得 " + TotalMetalNum + " 金屬!";
			ClosePanel.SetActive(true);
			MaterialManager.ResetSetMine();
			MaterialManager.ResetFirstMine();
			foreach(int num2 in AddNumTODB)
			{			
				Meterial.AddMaterial("Metal",num2);
			}
			Meterial.MineProgress = 0;
		}
	}

	public void CloseMinePanel()
	{
		NormalProgress.SetActive(true);
		MiningPanel.SetActive(false);
		ClosePanel.SetActive(false);

	}

	 public int GetMineNum(int min, int max)
	{
		MineOpened++;
		int val = Random.Range(min, max);
		Debug.Log(val);
		int MineNum = 0;
		while(usedValues.Contains(val))
		{
			val = Random.Range(min, max);
		}
		usedValues.Add(val);
		if(val <=3)
			MineNum = 1;
		else if(val <=7)
			MineNum = 5;
		else
			MineNum = 10;

		return MineNum;
	}


}
