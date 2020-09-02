using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaveManager : MonoBehaviour {

	public PetIllustrationBlueprint GoatPetBlueprint;
	public PetIllustrationBlueprint CatPetBlueprint;
	public PetIllustrationBlueprint PigPetBlueprint;
	public PetIllustrationBlueprint BearCatBlueprint;
	public PetIllustrationBlueprint ChickenBlueprint;
	public PetIllustrationBlueprint HorseBlueprint;

	public GameObject IslandPosition;
	public Sprite InIslandImg;
	private PetIllustrationBlueprint PetBlueprintToUse;
	private GameObject PetToSpawn;
	private PetMoveManager PetMoveManager;


	private Color32 CutColor = new Color (255,255,255,255);

	public void AssignNumber()
	{
		Debug.Log(PetManager.PetNumList.Count);
		foreach(int Petid in PetManager.PetNumList)
		{
			if(Petid == 1)
				GoatPetBlueprint.SetButtonActive();
			else if(Petid == 2)
				CatPetBlueprint.SetButtonActive();
			else if(Petid == 3)
				PigPetBlueprint.SetButtonActive();
			else if(Petid == 4)
				BearCatBlueprint.SetButtonActive();
			else if(Petid == 5)
				ChickenBlueprint.SetButtonActive();
			else if(Petid == 6)
				HorseBlueprint.SetButtonActive();
		}
		
		SetNumText();
		UpdateNum();
	}

	void SetNumText()
	{
		GoatPetBlueprint.SetTextandNumNow();
		CatPetBlueprint.SetTextandNumNow();
		PigPetBlueprint.SetTextandNumNow();
		BearCatBlueprint.SetTextandNumNow();
		ChickenBlueprint.SetTextandNumNow();
		HorseBlueprint.SetTextandNumNow();
	}

	public void PetOnIsland(int Petnum)//this is id
	{
		if(!PetManager.PetNumList.Contains(Petnum))
			return;
		int PetN = 0;
		foreach(int P in PetManager.PetNumList)
		{
			if(P == Petnum)
				PetN += 1;
		}
		if(Petnum == 1)
			PetBlueprintToUse = GoatPetBlueprint;
		else if(Petnum == 2)
			PetBlueprintToUse = CatPetBlueprint;
		else if(Petnum == 3)
			PetBlueprintToUse = PigPetBlueprint;
		else if(Petnum == 4)
			PetBlueprintToUse = BearCatBlueprint;
		else if(Petnum == 5)
			PetBlueprintToUse = ChickenBlueprint;
		else if(Petnum == 6)
			PetBlueprintToUse = HorseBlueprint;

		PetManager.PetNumList.Remove(Petnum);
		PetBlueprintToUse.PetNumNow -= 1;
		PetBlueprintToUse.ResetText();
		PetBlueprintToUse.InIsland.SetActive(true);
		PetBlueprintToUse.PetBackGround.GetComponent<Image>().sprite = InIslandImg;

		float PositionAddX = Random.Range(0f, 10.0f);
		float PositionAddY = Random.Range(0f, 10.0f);
		GameObject PetInfo = Instantiate(PetBlueprintToUse.Prefab,IslandPosition.transform.position + new Vector3(PositionAddX,PositionAddY),Quaternion.identity);
	}

	public void UpdateNum()
	{
		GoatPetBlueprint.ResetText();
		CatPetBlueprint.ResetText();
		PigPetBlueprint.ResetText();
		BearCatBlueprint.ResetText();
		ChickenBlueprint.ResetText();
		HorseBlueprint.ResetText();
	}
	public void WaitUISetWarrior(int num)
	{
		if(num == 1)
			PetWarrior.LorisWarrior = true;
		else if(num == 2)
			PetWarrior.ChameleonWarrior = true;
		else if(num == 3)
			PetWarrior.OwlWarrior = true;
		else if(num == 4)
			PetWarrior.TurtleWarrior = true;
	}
}
