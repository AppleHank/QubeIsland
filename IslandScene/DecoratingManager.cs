using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoratingManager : MonoBehaviour {

	public GameObject ButtonPrefab;
	public Vector2 LastPosition;
	public GameObject InitailPosition;
	public Transform ButtonParent;
	public List<GameObject> Buttons = new List<GameObject>();
	public BuildingManager BM;
	public Slider slider;
	public Text NowNumText;
	public Text PriceText;
	private int NowSellPrice;
	
	[Header("Pop")]
	public GameObject PopPanel;
	public Image PopImg;
	public Text PopNameText;
	public string DecoratingReadyToPut;

	[Header("Sprtie")]
	public Sprite NormalTreeSprite;
	public Sprite MintTreeSprite;
	public Sprite PinkTreeSprite;
	public Sprite GreenTreeSprite;
	public Sprite GrapeTreeSprite;
	public Sprite PuddingTreeSprite;
	public Sprite TrebleTreeSprite;
	public Sprite RustTreeSprite;
	public Sprite RustForestSprite;
	public Sprite FirSprite;
	public Sprite LittleFirForestSprite;
	public Sprite FirForestSprite;
	public Sprite DreamForestSprite;
	public Sprite LittleForestSprite;
	public Sprite ChristmasTreeSprite;

	[Header("Building")]
	public GameObject NormalTree;
	public GameObject MintTree;
	public GameObject GreenTree;
	public GameObject PinkTree;
	public GameObject GrapeTree;
	public GameObject PuddingTree;
	public GameObject TrebleTree;
	public GameObject RustTree;
	public GameObject RustForest;
	public GameObject Fir;
	public GameObject LittleFirForest;
	public GameObject FirForest;
	public GameObject DreamForest;
	public GameObject LittleForest;
	public GameObject ChristmasTree;

	void Start()
	{
		LastPosition = InitailPosition.GetComponent<RectTransform>().anchoredPosition;
		Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!))))))))))))))))");
		Debug.Log(LastPosition);
	}

	public void VolumeChanged(float num)
	{
		NowNumText.text = ((int)num).ToString();
		Debug.Log(NowSellPrice);
		PriceText.text = (num*NowSellPrice).ToString();
	}

	public void ButtonClicked(GameObject Button,int num,int BasicPrice)
	{
		slider.maxValue = num;
		NowSellPrice = BasicPrice;
		NowNumText.text = "1";
		PriceText.text = NowSellPrice.ToString();
		Debug.Log(BasicPrice);
		Debug.Log(NowSellPrice);
		Debug.Log(PriceText.text);

		if(Button.name == "NormalTree")
		{
			PopImg.sprite = NormalTreeSprite;
			PopNameText.text = "樹";
			DecoratingReadyToPut = "NormalTree";
		}
		else if(Button.name == "MintTree")
		{
			PopImg.sprite = MintTreeSprite;
			PopNameText.text = "薄荷樹";
			DecoratingReadyToPut = "MintTree";
		}
		else if(Button.name == "GreenTree")
		{
			PopImg.sprite = GreenTreeSprite;
			PopNameText.text = "翠綠樹";
			DecoratingReadyToPut = "GreenTree";
		}
		else if(Button.name == "PinkTree")
		{
			PopImg.sprite = PinkTreeSprite;
			PopNameText.text = "粉粉樹";
			DecoratingReadyToPut = "PinkTree";
		}
		else if(Button.name == "TrebleTree")
		{
			PopImg.sprite = TrebleTreeSprite;
			PopNameText.text = "小樹三兄弟";
			DecoratingReadyToPut = "TrebleTree";
		}
		else if(Button.name == "GrapeTree")
		{
			PopImg.sprite = GrapeTreeSprite;
			PopNameText.text = "葡萄樹";
			DecoratingReadyToPut = "GrapeTree";
		}
		else if(Button.name == "PuddingTree")
		{
			PopImg.sprite = PuddingTreeSprite;
			PopNameText.text = "布丁樹";
			DecoratingReadyToPut = "PuddingTree";
		}
		else if(Button.name == "RustTree")
		{
			PopImg.sprite = RustTreeSprite;
			PopNameText.text = "枯木";
			DecoratingReadyToPut = "RustTree";
		}
		else if(Button.name == "RustForest")
		{
			PopImg.sprite = RustForestSprite;
			PopNameText.text = "枯木森林";
			DecoratingReadyToPut = "RustForest";
		}
		else if(Button.name == "Fir")
		{
			PopImg.sprite = FirSprite;
			PopNameText.text = "冷杉";
			DecoratingReadyToPut = "Fir";
		}
		else if(Button.name == "LittleFirForest")
		{
			PopImg.sprite = LittleFirForestSprite;
			PopNameText.text = "小杉林";
			DecoratingReadyToPut = "LittleFirForest";
		}
		else if(Button.name == "FirForest")
		{
			PopImg.sprite = FirForestSprite;
			PopNameText.text = "冷杉森林";
			DecoratingReadyToPut = "FirForest";
		}
		else if(Button.name == "DreamForest")
		{
			PopImg.sprite = DreamForestSprite;
			PopNameText.text = "夢幻森林";
			DecoratingReadyToPut = "DreamForest";
		}
		else if(Button.name == "LittleForest")
		{
			PopImg.sprite = LittleForestSprite;
			PopNameText.text = "小森林";
			DecoratingReadyToPut = "LittleForest";
		}
		else if(Button.name == "ChristmasTree")
		{
			PopImg.sprite = ChristmasTreeSprite;
			PopNameText.text = "聖誕樹";
			DecoratingReadyToPut = "ChristmasTree";
		}
		PopPanel.GetComponent<SetSellNum>().Name = Button.name;
		PopPanel.GetComponent<SetSellNum>().BasisPrice = NowSellPrice;
		PopPanel.SetActive(true);
	}

	public void PutDecorating()
	{
		PopPanel.SetActive(false);
		BM.PutDecorating(DecoratingReadyToPut);
		if(DecoratingReadyToPut == "NormalTree")
		{
			BM.AssignNormalTreeToBuild();
			BM.AssignPreBuilding(NormalTree);
		}
		else if(DecoratingReadyToPut == "MintTree")
		{
			BM.AssignMintTreeToBuild();
			BM.AssignPreBuilding(MintTree);
		}
		else if(DecoratingReadyToPut == "GreenTree")
		{
			BM.AssignGreenTreeToBuild();
			BM.AssignPreBuilding(GreenTree);
		}
		else if(DecoratingReadyToPut == "PinkTree")
		{
			BM.AssignPinkTreeToBuild();
			BM.AssignPreBuilding(PinkTree);
		}
		else if(DecoratingReadyToPut == "TrebleTree")
		{
			BM.AssignTrebleTreeToBuild();
			BM.AssignPreBuilding(TrebleTree);
		}
		else if(DecoratingReadyToPut == "PuddingTree")
		{
			BM.AssignPuddingTreeToBuild();
			BM.AssignPreBuilding(PuddingTree);
		}
		else if(DecoratingReadyToPut == "GrapeTree")
		{
			BM.AssignGrapeTreeToBuild();
			BM.AssignPreBuilding(GrapeTree);
		}
		else if(DecoratingReadyToPut == "RustTree")
		{
			BM.AssignDeadTreeToBuild();
			BM.AssignPreBuilding(RustTree);
		}
		else if(DecoratingReadyToPut == "RustForest")
		{
			BM.AssignDeadTreeFroestToBuild();
			BM.AssignPreBuilding(RustForest);
		}
		else if(DecoratingReadyToPut == "Fir")
		{
			BM.AssignFirToBuild();
			BM.AssignPreBuilding(Fir);
		}
		else if(DecoratingReadyToPut == "LittleFirForest")
		{
			BM.AssignLittleFirToBuild();
			BM.AssignPreBuilding(LittleFirForest);
		}
		else if(DecoratingReadyToPut == "FirForest")
		{
			BM.AssignFirFroestToBuild();
			BM.AssignPreBuilding(FirForest);
		}
		else if(DecoratingReadyToPut == "DreamForest")
		{
			BM.AssignDreamFroestToBuild();
			BM.AssignPreBuilding(DreamForest);
		}
		else if(DecoratingReadyToPut == "LittleForest")
		{
			BM.AssignLittleFroestToBuild();
			BM.AssignPreBuilding(LittleForest);
		}
		else if(DecoratingReadyToPut == "ChristmasTree")
		{
			BM.AssignChristmasTreeToBuild();
			BM.AssignPreBuilding(ChristmasTree);
		}
	}
		
	public void UpdateButton(string Type,int Num)
	{
		bool hasButton = false;
		int repeatButton = 0;
		foreach(GameObject Button in Buttons)
		{
			if(Button.name == Type)
			{
				Button.GetComponent<DecoratingStored>().DecoratingNum.text = Num.ToString();
				hasButton = true;
				if(Num == 0)
				{
					bool StartReset = false;
					for(int i=0;i<Buttons.Count;i++)
					{
						if(StartReset)
						{
							Buttons[i].GetComponent<RectTransform>().anchoredPosition = LastPosition;
							UpdateLastPosition();
						}
						if(Buttons[i] == Button)
						{
							repeatButton = i;
							StartReset = true;
							LastPosition = Button.GetComponent<RectTransform>().anchoredPosition;
						}
					}
					Destroy(Button);
					Buttons.RemoveAt(repeatButton);
				}
				break;
			}
		}

		if(!hasButton)
			InsButton(Type,Num);

	}

	void InsButton(string Type,int Num)
	{
		GameObject Button = Instantiate(ButtonPrefab);
		DecoratingStored ButtonDS = Button.GetComponent<DecoratingStored>();
		Button.name = Type;

		ButtonDS.AssignSellPrice(20);

		if(Type == "NormalTree")
			ButtonDS.DecoratingImg.sprite = NormalTreeSprite;
		else if(Type == "MintTree")
			ButtonDS.DecoratingImg.sprite = MintTreeSprite;
		else if(Type == "PinkTree")
			ButtonDS.DecoratingImg.sprite = PinkTreeSprite;		
		else if(Type == "GreenTree")
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = GreenTreeSprite;
		else if(Type == "TrebleTree")
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = TrebleTreeSprite;
		else if(Type == "PuddingTree")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = PuddingTreeSprite;
			ButtonDS.AssignSellPrice(30);
		}
		else if(Type == "GrapeTree")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = GrapeTreeSprite;
			ButtonDS.AssignSellPrice(30);
		}
		else if(Type == "RustTree")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = RustTreeSprite;
			ButtonDS.AssignSellPrice(30);
		}
		else if(Type == "RustForest")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = RustForestSprite;
			ButtonDS.AssignSellPrice(50);
		}
		else if(Type == "Fir")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = FirSprite;
			ButtonDS.AssignSellPrice(30);
		}
		else if(Type == "LittleFirForest")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = LittleFirForestSprite;
			ButtonDS.AssignSellPrice(50);
		}
		else if(Type == "FirForest")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = FirForestSprite;
			ButtonDS.AssignSellPrice(50);
		}
		else if(Type == "DreamForest")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = DreamForestSprite;
			ButtonDS.AssignSellPrice(50);
		}
		else if(Type == "LittleForest")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = LittleForestSprite;
			ButtonDS.AssignSellPrice(50);
		}
		else if(Type == "ChristmasTree")
		{
			Button.GetComponent<DecoratingStored>().DecoratingImg.sprite = ChristmasTreeSprite;
			ButtonDS.AssignSellPrice(30);
		}

		
		ButtonDS.DecoratingNum.text = Num.ToString();
		ButtonDS.AssignDM(this);

		Buttons.Add(Button);
		Button.transform.SetParent(ButtonParent);
		Debug.Log(LastPosition);
		Button.GetComponent<RectTransform>().anchoredPosition = LastPosition;
		Button.transform.localScale = new Vector3(.7f,.7f,.7f);
		
		UpdateLastPosition();
	}

	void UpdateLastPosition()
	{
		LastPosition += new Vector2(230,0);

		if(LastPosition.x > 400)
		{
			Vector2 temp = new Vector2(InitailPosition.transform.localPosition.x,InitailPosition.transform.localPosition.y);
			temp.y = LastPosition.y - 297;
			LastPosition= temp;
		}
	}

}
