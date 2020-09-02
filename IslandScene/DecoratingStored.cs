using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoratingStored : MonoBehaviour {

	public Image DecoratingImg;
	public Text DecoratingNum;
	private DecoratingManager DM;
	private int BasicSellPrice;

	public void AssignSellPrice(int Price)
	{
		BasicSellPrice = Price;
	}

	public void AssignDM(DecoratingManager DM)
	{
		this.DM = DM;
	}

	public void WhenClick()
	{
		Debug.Log(BasicSellPrice);
		DM.ButtonClicked(gameObject,int.Parse(DecoratingNum.text),BasicSellPrice);
	}

}
