using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToWood : MonoBehaviour {

	public List<Sprite> LoadingSprite = new List<Sprite>();
	public List<string> Tips = new List<string>();
	public Image LoadingImg;
	public Text LoadingTipsText;

	// Use this for initialization
	void Start () {
		LoadingSprite.Add(Resources.Load<Sprite>("Loading/厭世貓連玩逗貓棒也很厭世-01"));
		LoadingSprite.Add(Resources.Load<Sprite>("Loading/媽媽的黑色藥水不夠拉-01"));
		LoadingSprite.Add(Resources.Load<Sprite>("Loading/懶猴王帶領著一批小而精幹的部隊-01"));
		LoadingSprite.Add(Resources.Load<Sprite>("Loading/據說兩隻糖果馬接吻時會形成一道神奇馬拱門-01"));
		LoadingSprite.Add(Resources.Load<Sprite>("Loading/紅綠燈龍是嚴格的交警-01"));
		Tips.Add("PVE對戰不會扣除代表生命值，也不會加挖礦次數");
		Tips.Add("你知道嗎？進化懶猴王的守衛有兩種造型喔！");
		Tips.Add("蜜蜂龜的殼是蜂蜜蛋糕口味");
		Debug.Log("Meterial1");
		Debug.Log(LoadingSprite.Count);
		int SpriteIndex = Random.Range(0,LoadingSprite.Count-1);
		int TipsIndex = Random.Range(0,Tips.Count-1);
		Debug.Log(SpriteIndex);
		Debug.Log(TipsIndex);
		Debug.Log(LoadingSprite[SpriteIndex]);
		LoadingImg.sprite = LoadingSprite[SpriteIndex];
		LoadingTipsText.text = Tips[TipsIndex];

		SceneManager.LoadScene("Meterial1");
	//	StartCoroutine(ChangeScene());
	}

//	IEnumerator ChangeScene()
//	{
//		yield return new WaitForSeconds(2f);
//	}
}
