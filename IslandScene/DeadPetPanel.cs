using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPetPanel : MonoBehaviour {

	public int PetId;
	public int PetExp;
	public int PetLevel;
	public int PetLife;
	public Image PanelPetImg;
	public string PetDBName;
	public Text PetName;
	public Text PetHp;
	public Text PetSp;
	public Text PetColdDown;
	public Text PetBuff;
	public Text PetSpecialBuffText;
	public Text LevelText;
	public Text ExpText;
	public Image ExpProgressBar;
	public Text FoodNumText;
	public GameObject FoodButton;
	private PetMoveManager PMM;
	public GameObject OceanButton;
	public GameObject ReCoverButton;
	public GameObject BurialButton;
	private GameObject OceanPanelObj;
	private GameObject BurialButtonInDeadPanel;
	public Image StarImg;
	public Image Icon1;
	public Image Icon2;
	public Image BuffImg;
	public GameObject ChangeButton;
	private GameObject OceanPanel;

	public void DestroyBurialButton()
	{
		Destroy(BurialButton);
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(.1f);
		PMM.ResetBurialButtonPosition();
	}

	public void SetButtonInDP(GameObject Obj)
	{
		BurialButtonInDeadPanel = Obj;
	}

	public void SetPetInfo(PetBlueprint PB)
	{
		PetName.text = PB.PetName;
		PetHp.text = PB.PetHp.ToString();
		PetSp.text = PB.PetSp;
		PetBuff.text = PB.PetBuff;
		PetSpecialBuffText.text = PB.PetSpecialBuff;
		Icon1.sprite = PB.Type[0];
		Icon2.sprite = PB.Type[1];
	}

	public void Set(Sprite Img,int Level,string Name,int Exp,int LevelToUpExp)
	{
		PetLevel = Level;
		PetDBName = Name;
		PetExp = Exp;
		PanelPetImg.sprite = Img;

		if(LevelToUpExp == 0)
			ExpText.text = "Max";
			
		ExpText.text = Exp + " / " + LevelToUpExp;
		LevelText.text = "Level：" + Level.ToString();
		ExpProgressBar.fillAmount = (float)Exp / (float)LevelToUpExp;
	}

	public void SetOcean(GameObject Obj,GameObject OceanPanel)
	{
		OceanPanelObj = Obj;
		this.OceanPanel = OceanPanel;
	}


	public void SetPMM(PetMoveManager PMM)
	{
		this.PMM = PMM;
	}

	public void SetDeadNameInPMM()
	{
		PMM.SetDeadPetName(PetDBName);
	}

	public void Recover()
	{
		Debug.Log("Open");
		PMM.SetDeadPetName(PetDBName);
		PMM.OpenRecoverPanel(PanelPetImg);
	}

	public void Ocean()
	{
		PMM.SetDeadPetName(PetDBName);
		OceanPanelObj.SetActive(true);
		OceanPanelObj.GetComponent<Ocean>().BackGround.SetActive(true);
		OceanPanel.SetActive(true);

	}

	public void ResetBurialButton()
	{
		if(BurialButtonInDeadPanel != null)
		{
			BurialButtonInDeadPanel.GetComponent<Image>().sprite = PMM.Shade;
			BurialButtonInDeadPanel.GetComponent<BurialButtonInfo>().isSeted = false;
		}
		gameObject.SetActive(false);
	}

	public void PlayCancel()
	{
		PMM.PlayCancel();
	}

	public void PlayOpen()
	{
		PMM.PlayOpen();
	}


}
