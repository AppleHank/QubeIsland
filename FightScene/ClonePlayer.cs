using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClonePlayer : MonoBehaviour {

	public Text liveText;
	public Image LivesBar;
	public GameObject Doll;

	public void SetLive(float Lives,float startLives)
	{
		liveText.text = Lives.ToString() + "/" + startLives.ToString();
	}

	public void UpdateLive (float Lives,float startLives) 
	{
		liveText.text = Lives.ToString() + "/" + startLives.ToString();
		LivesBar.fillAmount = Lives / startLives;
	}

	public void ReSetImg(Sprite WarriorImg)
	{
		Doll.GetComponent<SpriteRenderer>().sprite = WarriorImg;
	}
}
