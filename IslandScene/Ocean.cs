using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BurialNormalOceanInfo
{
	public Sprite Sprite1;
	public Sprite Sprite2;
	public string Material1;
	public string Material2;
}
[System.Serializable]
public class BurialUpgradeOceanInfo
{
	public Sprite Sprite1;
	public Sprite Sprite2;
	public Sprite Sprite3;
	public string Material1;
	public string Material2;
	public string Material3;
}

public class Ocean : MonoBehaviour {


	private int Id;
	private string Name;
	private int Grade;
	private PetMoveManager PMM;
	public Image OceanNormalImg1;
	public Text OceanNormalText1;
	public Image OceanNormalImg2;
	public Text OceanNormalText2;
	public Image OceanUpImg1;
	public Text OceanUpText1;
	public Image OceanUpImg2;
	public Text OceanUpText2;
	public Image OceanUpImg3;
	public Text OceanUpText3;
	public GameObject OceanButton;
	private BurialNormalOceanInfo TempNormal = new BurialNormalOceanInfo();
	private	BurialUpgradeOceanInfo TempUpgrade = new BurialUpgradeOceanInfo();
	public BurialNormalOceanInfo OceanGoat;
	public BurialNormalOceanInfo OceanCat;
	public BurialNormalOceanInfo OceanPig;
	public BurialNormalOceanInfo OceanBearCat;
	public BurialNormalOceanInfo OceanChicken;
	public BurialUpgradeOceanInfo OceanHorse;
	public BurialUpgradeOceanInfo OceanLoris;
	public BurialUpgradeOceanInfo OceanOwl;
	public BurialUpgradeOceanInfo OceanChameleon;
	public BurialUpgradeOceanInfo OceanTurtle;
	private GameObject DeadPanel;
	public GameObject BackGround;
	public GameObject ShadPanel;

	public void SetDeadPanel(GameObject B)
	{
		DeadPanel = B;
	}

	public void SetPMM(PetMoveManager PMM)
	{
		this.PMM = PMM;
	}

	public void PlayCancel()
	{
		PMM.PlayCancel();
	}

	public void PlayOpen()
	{
		PMM.PlayOpen();
	}

	public void Set(int Pid,int Grade,string Name)
	{
		Id = Pid;
		this.Name = Name;
		this.Grade = Grade;

		if(Pid == 1)
			TempNormal = OceanGoat;
		else if(Pid == 2)
			TempNormal = OceanCat;
		else if(Pid == 3)
			TempNormal = OceanPig;
		else if(Pid == 4)
			TempNormal = OceanBearCat;
		else if(Pid == 5)
			TempNormal = OceanChicken;
		else if(Pid == 6)
			TempUpgrade = OceanHorse;
		else if(Pid == 7)
			TempUpgrade = OceanLoris;
		else if(Pid == 8)
			TempUpgrade = OceanOwl;
		else if(Pid == 9)
			TempUpgrade = OceanChameleon;
		else if(Pid == 10)
			TempUpgrade = OceanTurtle;

		Debug.Log(TempNormal.Sprite1);
		if(TempNormal.Sprite1 != null)
		{
			OceanNormalImg1.sprite = TempNormal.Sprite1;
			OceanNormalImg2.sprite = TempNormal.Sprite2;
			OceanNormalImg1.gameObject.SetActive(true);
			OceanNormalImg2.gameObject.SetActive(true);
			int Num1 = GetNum(Grade);
			StartCoroutine(Wait(Grade,Pid,0));
			OceanNormalText1.text = Num1.ToString();
		}
		else
		{
			OceanUpImg1.sprite = TempUpgrade.Sprite1;
			OceanUpImg2.sprite = TempUpgrade.Sprite2;
			OceanUpImg3.sprite = TempUpgrade.Sprite3;
			int Num1 = GetNum(Grade);
			gameObject.SetActive(true);
			StartCoroutine(Wait(Grade,Pid,2));
			OceanUpText1.text = Num1.ToString();
			OceanUpImg1.gameObject.SetActive(true);
			OceanUpImg2.gameObject.SetActive(true);
			OceanUpImg3.gameObject.SetActive(true);
		}
	}

	IEnumerator GetThird(int Grade,int Id)
	{
		yield return new WaitForSeconds(1f);
		int Num2 = GetNum(Grade);
		OceanUpText3.text = Num2.ToString();
	}


	IEnumerator Wait(int Grade,int Id,int UpSetNum)
	{
		yield return new WaitForEndOfFrame();
		Debug.Log("Over");
		int Num2 = GetNum(Grade);
		if(Id < 6)
			OceanNormalText2.text = Num2.ToString();
		else if(UpSetNum == 2)
		{
			OceanUpText2.text = Num2.ToString();
			OceanUpText3.text = (Num2 - 1).ToString();
		}
		if(UpSetNum == 3)
		{
			yield return new WaitForEndOfFrame();	
			int Num3 = GetNum(Grade);
			OceanUpText3.text = Num3.ToString();
			Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!=="+Num3);
		}
		gameObject.transform.parent = DeadPanel.transform;
	}

	public int GetNum(int Grade)
	{
		float Rate = Random.Range(0f,100f);
		int Num = 0;
		if(Grade == 1)
		{
			if(Rate < 34)
				Num = 3;
			else if(Rate <67)
				Num = 4;
			else
				Num = 5;
		}
		else
		{
			if(Rate <= 20)
				Num = 11;
			else if(Rate <= 40)
				Num = 12;
			else if(Rate <= 60)
				Num = 13;
			else if(Rate <= 80)
				Num = 14;
			else if(Rate <= 100)
				Num = 15;
		}
		return Num;
	}

	public void WhenClickButton()
	{
		if(Id < 6)
		{
			Meterial.AddMaterial(TempNormal.Material1,int.Parse(OceanNormalText1.text));
			Meterial.AddMaterial(TempNormal.Material2,int.Parse(OceanNormalText2.text));
		}
		else
		{
			Meterial.AddMaterial(TempUpgrade.Material1,int.Parse(OceanUpText1.text));
			Meterial.AddMaterial(TempUpgrade.Material2,int.Parse(OceanUpText2.text));
			Meterial.AddMaterial(TempUpgrade.Material3,int.Parse(OceanUpText3.text));
		}

		DeadPetPanel DP = DeadPanel.GetComponent<DeadPetPanel>();
		DP.DestroyBurialButton();
		DP.ResetBurialButton();
		PMM.UpDateOcean(Id,Grade,Name,gameObject);
//		PMM.RefreshPetList();
	}


}
