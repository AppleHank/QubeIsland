using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetHealth : MonoBehaviour {

	private int life;
	private PetMoveManager PetManager;
	private string NameInDb;
	private Sprite PetImg;
	public Text Life;
	public Text Level;
	public GameObject PetButton; // PetImage
	public Image BackGround;
	public Sprite Warrior;
	public Sprite Upgrade;
	private GameObject PetPanel;
	private int ArrayNum;
	public GameObject UpgradeCheck;
	public Image GradeImg;

	
	public void SetImg(Sprite Img)
	{
		PetButton.GetComponent<Image>().sprite = Img;
	}

	public void SetLevel(int Level)
	{
		this.Level.text = Level.ToString();
	}
	
	public void SetPMM(PetMoveManager PMM)
	{
		PetManager = PMM;
	}

	public void SetLife(int Life)
	{
		life = Life;
		this.Life.text = Life.ToString();
	}

	public void SetName(string name)
	{
		NameInDb = name;
	}

	public void ChangeCube(int num,int isWarrior)
	{
		if(isWarrior == 1)//Warrior
			BackGround.sprite = Warrior;
		else if(num == 2)
			BackGround.sprite = Upgrade;
	}

	public void WhenClick()
	{
		PetManager.OpenHealEnsure(NameInDb,PetButton.GetComponent<Image>().sprite);
	}


}
