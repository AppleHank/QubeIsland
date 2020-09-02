using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class IllustrationManager : MonoBehaviour {

	[Header("TurretIllustration")]
	private Illustration_Turret NowSeeTurret;
	public GameObject ChangeToUpgradeButton;
	public Text TurretNameText;
	public Image TurretImg;
	public List<Image> DamageStar = new List<Image>();
	public List<Image> RangeStar = new List<Image>();
	public Sprite halfStarSprite;
	public Sprite StarSprite;
	public Text TurretInfoText;
	public GameObject TurretIllustration;
	private int UpgradeDamageStarNum;
	private bool UpgradeDamagehalfStar;
	private int UpgradeRangeStarNum;
	private bool UpgradeRangehalfStar;
	private Sprite UpgradeTurretSprite;
	public Illustration_Turret BloodTurret;
	public Illustration_Turret StandardTurret;
	public Illustration_Turret FireTurret;
	public GameObject IFireBlock;
	public Illustration_Turret LavaTurret;
	public GameObject ILavaBlock;
	public Illustration_Turret IceTurret;
	public GameObject IIceBlock;
	public Illustration_Turret SnowTurret;
	public GameObject ISnowBlock;
	public Illustration_Turret MoneyTurret;
	public GameObject IMoneyBlock;
	public Illustration_Turret HealthTurret;
	public GameObject IHealthBlock;
	public List<Illustration_Turret> TurretList = new List<Illustration_Turret>();
	private List<int> TurretTempList = new List<int>();

	[Header("MonsterIllustration")]
	public GameObject MonsterIllustration;
	public Text MonsterNameText;
	public Image MonsterIcon1;
	public Image MonsterIcon2;
	public Image MonsterImage;
	public Text MonsterHealthText;
	public Text MonsterInfoText;
	public Text MonsterSpeedText;
	public List<Illustrations> MonsterId = new List<Illustrations>();
	private Illustrations NowSeeMonster;
	public Illustrations Goat;
	public Illustrations Cat;
	public Illustrations Pig;
	public Illustrations BearCat;
	public Illustrations Chicken;
	public Illustrations Horse;
	public GameObject HorseBlock;
	public Illustrations Loris;
	public GameObject LorisBlock;
	public Illustrations Owl;
	public GameObject OwlBlock;
	public Illustrations Chameleon;
	public GameObject ChameleonBlock;
	public Illustrations Turtle;
	public GameObject TurtleBlock;
	public Illustrations UGoat;
	public GameObject UGoatBlock;
	public Illustrations UCat;
	public GameObject UCatBlock;
	public Illustrations UPig;
	public GameObject UPigBlock;
	public Illustrations UBearCat;
	public GameObject UBearCatBlock;
	public Illustrations UChicken;
	public GameObject UChickenBlock;
	public Illustrations UHorse;
	public GameObject UHorseBlock;
	public Illustrations ULoris;
	public GameObject ULorisBlock;
	public Illustrations UOwl;
	public GameObject UOwlBlock;
	public Illustrations UChameleon;
	public GameObject UChameleonBlock;
	public Illustrations UTurtle;
	public GameObject UTurtleBlock;
	public bool isFriendIsland;
	public GameObject DamageNull;
	public GameObject RangeNull;

	public void UnlockFireIllustration()
	{
	//	AddUnlockTurretList(1);
		TurretTempList.Add(1);
	}
	public void UnlockLavaIllustration()
	{
	//	AddUnlockTurretList(1);
	//	AddUnlockTurretList(2);
		TurretTempList.Add(1);
		TurretTempList.Add(2);
	}
	public void UnlockIceIllustration()
	{
	//	AddUnlockTurretList(3);
		TurretTempList.Add(3);
	}
	public void UnlockSnowIllustration()
	{
	//	AddUnlockTurretList(3);
	//	AddUnlockTurretList(4);
		TurretTempList.Add(3);
		TurretTempList.Add(4);
	}
	public void UnlockHolyIllustration()
	{
	//	AddUnlockTurretList(5);
		TurretTempList.Add(5);
	}
	public void UnlockMoneyIllustration()
	{
	//	AddUnlockTurretList(5);
	//	AddUnlockTurretList(6);
		TurretTempList.Add(5);
		TurretTempList.Add(6);
	}

	public void RefreshTurretList()
	{
		TurretTempList.Sort();
		TurretTempList = TurretTempList.Distinct().ToList();
		Debug.Log(TurretTempList.Count);
		Debug.Log(TurretList.Count);
		if(TurretTempList.Count != TurretList.Count - 2)
		{
			TurretList.Clear();
				TurretList.Add(BloodTurret);
				TurretList.Add(StandardTurret);
			foreach(int i in TurretTempList)
			{
				AddUnlockTurretList(i);
			}
		}
	}

	public void ResetStar()
	{	
		foreach(Image T in DamageStar)
		{
			T.gameObject.SetActive(false);
			if(T.sprite == halfStarSprite)
			{
				T.rectTransform.sizeDelta = new Vector2(35,35);
				T.sprite = StarSprite;
				T.rectTransform.anchoredPosition = new Vector2(T.rectTransform.anchoredPosition.x+8,T.rectTransform.anchoredPosition.y);
			}
		}
		foreach(Image T in RangeStar)
		{
			T.gameObject.SetActive(false);
			if(T.sprite == halfStarSprite)
			{
				T.rectTransform.sizeDelta = new Vector2(35,35);
				T.sprite = StarSprite;
				T.rectTransform.anchoredPosition = new Vector2(T.rectTransform.anchoredPosition.x+8,T.rectTransform.anchoredPosition.y);
			}
		}
	}

	public void SeeTurretInfo (Illustration_Turret I,Sprite Img,int DamageStarNum,bool DamagehalfStar,string Info,string Name,int RangeStarNum,bool RangehalfStar)
	{
		ResetStar();
		if(I == MoneyTurret || I == HealthTurret)
			ChangeToUpgradeButton.SetActive(false);
		else
			ChangeToUpgradeButton.SetActive(true);

		NowSeeTurret = I;
		TurretImg.sprite = Img;
		if(I == BloodTurret)
			TurretImg.rectTransform.sizeDelta = new Vector2(Img.rect.width*.125f,Img.rect.height*.125f);
		else
			TurretImg.rectTransform.sizeDelta = new Vector2(Img.rect.width*.15f,Img.rect.height*.15f);
		Debug.Log(Img.rect.width);

		if(I == MoneyTurret | I == HealthTurret)
		{
			RangeNull.SetActive(true);
			DamageNull.SetActive(true);
		}
		else
		{
			RangeNull.SetActive(false);
			DamageNull.SetActive(false);
		}

		for(int i=0;i<DamageStarNum;i++)//Damage
		{
			DamageStar[i].gameObject.SetActive(true);
			if(i == (DamageStarNum - 1))
			{
				if(DamagehalfStar)
				{
					DamageStar[i].sprite = halfStarSprite;
					DamageStar[i].rectTransform.sizeDelta = new Vector2(35*.54f,35);
					DamageStar[i].rectTransform.anchoredPosition = new Vector2(DamageStar[i].rectTransform.anchoredPosition.x-8,DamageStar[i].rectTransform.anchoredPosition.y);
				}
			}
		}
		
		for(int i=0;i<RangeStarNum;i++)
		{
			RangeStar[i].gameObject.SetActive(true);
			if(i == (RangeStarNum - 1))
			{
				if(RangehalfStar)
				{
					RangeStar[i].sprite = halfStarSprite;
					RangeStar[i].rectTransform.sizeDelta = new Vector2(35*.54f,35);
					RangeStar[i].rectTransform.anchoredPosition = new Vector2(RangeStar[i].rectTransform.anchoredPosition.x-8,RangeStar[i].rectTransform.anchoredPosition.y);
				}
			}
		}
		TurretInfoText.text = "說明：" + Info;
		TurretNameText.text = Name;	
		TurretIllustration.SetActive(true);	
	}

	public void NextTurret()
	{
		int NowIndex = TurretList.FindIndex(x => x.Equals(NowSeeTurret));
		if(NowIndex == TurretList.Count-1)
			TurretList[0].WhenClick();
		else
			TurretList[NowIndex+1].WhenClick();
	}
	public void PreviousTurret()
	{
		int NowIndex = TurretList.FindIndex(x => x.Equals(NowSeeTurret));
		if(NowIndex == 0)
			TurretList[TurretList.Count-1].WhenClick();
		else
			TurretList[NowIndex-1].WhenClick();
	}

	public void SetUpgradeTurretInfo(int DamageStarNum,bool DamagehalfStar,int RangeStarNum,bool RangehalfStar,Sprite Img)
	{
		UpgradeDamageStarNum = DamageStarNum;
		UpgradeDamagehalfStar = DamagehalfStar;
		UpgradeRangeStarNum = RangeStarNum;
		UpgradeRangehalfStar = RangehalfStar;
		UpgradeTurretSprite = Img;
	}

	public void SeeUpgradeVersion()
	{
		ResetStar();
		if(TurretImg.sprite == UpgradeTurretSprite)
			NowSeeTurret.WhenClick();

		else
		{

			TurretImg.sprite = UpgradeTurretSprite;
			
			if(NowSeeTurret == BloodTurret)
				TurretImg.rectTransform.sizeDelta = new Vector2(UpgradeTurretSprite.rect.width*.125f,UpgradeTurretSprite.rect.height*.125f);
			else
				TurretImg.rectTransform.sizeDelta = new Vector2(UpgradeTurretSprite.rect.width*.15f,UpgradeTurretSprite.rect.height*.15f);

			for(int i=0;i<UpgradeDamageStarNum;i++)//Damage
			{
				DamageStar[i].gameObject.SetActive(true);
				if(i == (UpgradeDamageStarNum - 1))
				{
					if(UpgradeDamagehalfStar)
					{
						DamageStar[i].sprite = halfStarSprite;
						DamageStar[i].rectTransform.sizeDelta = new Vector2(35*.54f,35);
						DamageStar[i].rectTransform.anchoredPosition = new Vector2(DamageStar[i].rectTransform.anchoredPosition.x-8,DamageStar[i].rectTransform.anchoredPosition.y);
					
					}
				}
			}
			
			for(int i=0;i<UpgradeRangeStarNum;i++)
			{
				RangeStar[i].gameObject.SetActive(true);
				if(i == (UpgradeRangeStarNum - 1))
				{
					if(UpgradeRangehalfStar)
					{
						RangeStar[i].sprite = halfStarSprite;
						RangeStar[i].rectTransform.sizeDelta = new Vector2(35*.54f,35);
						RangeStar[i].rectTransform.anchoredPosition = new Vector2(RangeStar[i].rectTransform.anchoredPosition.x-8,RangeStar[i].rectTransform.anchoredPosition.y);
					
					}
				}
			}
		}
	}

	public void SeeMonsterIllustation(Illustrations I,Sprite Img,string name,Sprite Icon1,Sprite Icon2,int Health,string Speed,string Info)
	{
		NowSeeMonster = I;
		MonsterImage.sprite = Img;
		MonsterNameText.text = name;
		MonsterIcon1.sprite = Icon1;
		MonsterSpeedText.text = "速度：" + Speed;
		if(Icon2 == null)
			MonsterIcon2.gameObject.SetActive(false);
		else
			MonsterIcon2.sprite = Icon2;
		MonsterIllustration.SetActive(true);
		MonsterHealthText.text = "血量：" + Health.ToString();
		MonsterInfoText.text = "說明：" + Info;
	}

	public void Next()
	{
		int NowIndex = MonsterId.FindIndex(x => x.Equals(NowSeeMonster));
		if(NowIndex == MonsterId.Count-1)
			MonsterId[0].SeeMonsterIllustation();
		else
			MonsterId[NowIndex+1].SeeMonsterIllustation();
	}

	public void Previous()
	{
		int NowIndex = MonsterId.FindIndex(x => x.Equals(NowSeeMonster));
		if(NowIndex == 0)
			MonsterId[MonsterId.Count-1].SeeMonsterIllustation();
		else
			MonsterId[NowIndex-1].SeeMonsterIllustation();
	}

	public void RefreshList()
	{
		MonsterId.Clear();
		MonsterId.Add(Goat);
		MonsterId.Add(Cat);
		MonsterId.Add(Pig);
		MonsterId.Add(BearCat);
		MonsterId.Add(Chicken);
	}

	public void AddUnlockTurretList(int id)
	{
		if(isFriendIsland)
			return;
		
		if(id == 1)//Fire
		{
			TurretList.Add(FireTurret);
			IFireBlock.SetActive(false);
		}
		else if(id == 2)
		{
			TurretList.Add(LavaTurret);
			ILavaBlock.SetActive(false);
		}
		else if(id == 3)
		{
			TurretList.Add(IceTurret);
			IIceBlock.SetActive(false);
		}
		else if(id == 4)
		{
			TurretList.Add(SnowTurret);
			ISnowBlock.SetActive(false);
		}
		else if(id == 5)
		{
			TurretList.Add(HealthTurret);
			IHealthBlock.SetActive(false);
		}
		else if(id == 6)
		{
			TurretList.Add(MoneyTurret);
			IMoneyBlock.SetActive(false);
		}
	}

	public void AddUnlockList(int Id)
	{
		if(isFriendIsland)
			return;
		if(Id <= 5)
			return;
		else if(Id == 6)
		{
			MonsterId.Add(Horse);
			HorseBlock.SetActive(false);
		}
		else if(Id == 7)
		{
			MonsterId.Add(Loris);
			LorisBlock.SetActive(false);
		}
		else if(Id == 8)
		{
			MonsterId.Add(Owl);
			OwlBlock.SetActive(false);
		}
		else if(Id == 9)
		{
			MonsterId.Add(Chameleon);
			ChameleonBlock.SetActive(false);
		}
		else if(Id == 10)
		{
			MonsterId.Add(Turtle);
			TurtleBlock.SetActive(false);
		}
		else if(Id == 11)
		{
			MonsterId.Add(UGoat);
			UGoatBlock.SetActive(false);
		}
		else if(Id == 12)
		{
			MonsterId.Add(UCat);
			UCatBlock.SetActive(false);
		}
		else if(Id == 13)
		{
			MonsterId.Add(UPig);
			UPigBlock.SetActive(false);
		}
		else if(Id == 14)
		{
			MonsterId.Add(UBearCat);
			UBearCatBlock.SetActive(false);
		}
		else if(Id == 15)
		{
			MonsterId.Add(UChicken);
			UChickenBlock.SetActive(false);
		}
		else if(Id == 16)
		{
			MonsterId.Add(UHorse);
			UHorseBlock.SetActive(false);
		}
		else if(Id == 17)
		{
			MonsterId.Add(ULoris);
			ULorisBlock.SetActive(false);
		}
		else if(Id == 18)
		{
			MonsterId.Add(UOwl);
			UOwlBlock.SetActive(false);
		}
		else if(Id == 19)
		{
			MonsterId.Add(UChameleon);
			UChameleonBlock.SetActive(false);
		}
		else if(Id == 20)
		{
			MonsterId.Add(UTurtle);
			UTurtleBlock.SetActive(false);
		}
	}


}
