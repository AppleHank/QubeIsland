using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialListButton : MonoBehaviour {

	public Text Life;
	public Text Level;
	public GameObject AfterPicked;
	public GameObject BeforePicked;
	public PetMoveManager PMM;
	public Image BG;
	public Sprite OriginalBG;

	public void Reset()
	{
		AfterPicked.SetActive(false);
		BeforePicked.SetActive(true);
		BG.sprite = OriginalBG;
	}

}
