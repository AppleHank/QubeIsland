using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exchange : MonoBehaviour {

	public Image GoodsToChangeImage;
	public Slider slider;
	public Text NowNumText;
	private int HaveNum;
	private int BasicNum;
	private int UpFood30 = 30;
	private int UpFood60 = 60;
	private int Food10 = 10;
	private int Food20 = 20;
	public Text ConsumeText;
	private string MaterialToReduce;
	public string Material;
	private GameObject Block;
	public GameObject FoodBlock;
	public GameObject UpFoodBlock;
	public GameObject BackChangeBlock;
	private int NowNum;
	private int DiamandChanged;
	private int DiamandChangeStatus;
	private int UpFoodChangeStatus;
	private int FoodChangeStatus;
	public Image ChangeImg;
	public Sprite Cotton;
	public Sprite Rock;
	public Sprite Earth;
	public Sprite Wood;
	public Sprite Back;


	public void AssignMaxValue(int num)
	{
		NowNum = num;
		if(num == 1)//Wood;
		{
			BasicNum = Food20;
			HaveNum = Meterial.wood;
			MaterialToReduce = "Wood";
			FoodChangeStatus = 1;
			ChangeImg.sprite = Wood;
		}
		else if(num == 2)//Earth;
		{
			BasicNum = Food10;
			HaveNum = Meterial.earth;
			MaterialToReduce = "Earth";
			FoodChangeStatus = 2;
			ChangeImg.sprite = Earth;
		}
		else if(num == 3)//Rock;
		{
			BasicNum = Food10;
			HaveNum = Meterial.rock;
			MaterialToReduce = "Rock";
			FoodChangeStatus = 3;
			ChangeImg.sprite = Rock;
		}
		else if(num == 4)//Cotton;
		{
			BasicNum = Food10;
			HaveNum = Meterial.cotton;
			MaterialToReduce = "Cotton";
			FoodChangeStatus = 4;
			ChangeImg.sprite = Cotton;
		}
		else if(num == 5)//wood;
		{
			BasicNum = UpFood60;
			HaveNum = Meterial.wood;
			MaterialToReduce = "Wood";
			UpFoodChangeStatus = 5;
			ChangeImg.sprite = Wood;
		}
		else if(num == 6)//earth;
		{
			BasicNum = UpFood30;
			HaveNum = Meterial.earth;
			MaterialToReduce = "Earth";
			UpFoodChangeStatus = 6;
			ChangeImg.sprite = Earth;
		}
		else if(num == 7)//rock;
		{
			BasicNum = UpFood30;
			HaveNum = Meterial.rock;
			MaterialToReduce = "Rock";
			UpFoodChangeStatus = 7;
			ChangeImg.sprite = Rock;
		}
		else if(num == 8)//cotton;
		{
			BasicNum = UpFood30;
			HaveNum = Meterial.cotton;
			MaterialToReduce = "Cotton";
			UpFoodChangeStatus = 8;
			ChangeImg.sprite = Cotton;
		}
		else if(num == 9)
		{
			BasicNum = 10;
			DiamandChanged = 180;
			HaveNum = Meterial.diamandbee;
			MaterialToReduce = "Diamand";
			DiamandChangeStatus = 9;
			ChangeImg.sprite = Back;
		}
		else if(num == 10)
		{
			BasicNum = 1;
			DiamandChanged = 15;
			HaveNum = Meterial.diamandbee;
			MaterialToReduce = "Diamand";
			DiamandChangeStatus = 10;
			ChangeImg.sprite = Back;
		}

		if(num <= 4)
		{
			Block = FoodBlock;
			Material = "Food";
		}
		else if(num <= 8)
		{
			Block = UpFoodBlock;
			Material = "UpFood";
		}
		else
		{
			Block = BackChangeBlock;
			Material = "Back";
		}
		

		if(HaveNum < BasicNum)
		{
			Debug.Log("!!!!!!!!!!!");
			Block.SetActive(true);
			return;
		}

		Block.SetActive(false);

		slider.maxValue = HaveNum/BasicNum;
		slider.value = 1;
		ConsumeText.text = BasicNum*1 + "/" + HaveNum;
		if(NowNum > 8)
			NowNumText.text = DiamandChanged.ToString();
		else
			NowNumText.text = "1";
	}	

	public void RefreshSliderPage(int Status)
	{
		if(Status == 1)
			AssignMaxValue(DiamandChangeStatus);
		else if(Status == 2)
			AssignMaxValue(UpFoodChangeStatus);
		else if(Status == 3)
			AssignMaxValue(FoodChangeStatus);
	}
	
	
	public void VolumeChanged(float num)
	{
		if(NowNum >8)
			NowNumText.text = ((int)num*DiamandChanged).ToString();
		else
			NowNumText.text = ((int)num).ToString();
		ConsumeText.text = (int)num*BasicNum + "/" + HaveNum;
	}

	public void AddValue()
	{
		if(slider.value >= slider.maxValue)
			return;
		slider.value += 1;
	}	
	
	public void DecreseValue()
	{
		if(slider.value <= 1)
		{
			slider.value = 1;
			return;
		}
		slider.value -= 1;
	}

	public void ExchangeFood()
	{
		Meterial.Reduce(MaterialToReduce,(int)slider.value*BasicNum);
		Meterial.AddMaterial(Material,int.Parse(NowNumText.text));
		AssignMaxValue(NowNum);
	}


}
