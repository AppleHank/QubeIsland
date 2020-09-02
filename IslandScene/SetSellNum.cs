using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSellNum : MonoBehaviour {

	public Text NowNum;
	public Slider slider;
	public string Name;
	public int BasisPrice;
	public GameObject CompleteSell;
	public Text PriceText;
	public bool isDecorating;

	public void VolumeChanged(float num)
	{
		NowNum.text = ((int)num).ToString();
		PriceText.text = (((int)num)*BasisPrice).ToString();
	}

	public void DecreseValue()
	{
		if(slider.value <= 0)
		{
			slider.value = 1;
			return;
		}
		slider.value -= 1;
	}

	public void IncreseValue()
	{
		if(slider.value >= slider.maxValue)
			return;
		slider.value += 1;
	}

	public void Sell()
	{
		if(isDecorating)
		{
			BuildingManager BM = GameObject.FindObjectOfType<BuildingManager>();
			BM.PutDecorating(Name);
			BM.UpdateStoredButton(int.Parse(NowNum.text));

		}
		else
			Meterial.Reduce(Name,int.Parse(NowNum.text));
		Meterial.AddMaterial("Back",int.Parse(PriceText.text));
		CompleteSell.SetActive(true);
		gameObject.SetActive(false);
	}

	public void SetMax()
	{
		slider.maxValue = (int)Meterial.GetNum(Name);
		slider.value = 1;
	}

}
