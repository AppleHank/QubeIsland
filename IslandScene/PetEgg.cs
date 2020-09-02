using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PetEgg : MonoBehaviour {

	public PetMoveManager PMM;
	public CaveManager CM;
	public GameObject PetImg;
	public GameObject PetEggImg;
	public GameObject Goat;
	public GameObject Cat;
	public GameObject Pig;
	public GameObject BearCat;
	public GameObject Chicken;
	public GameObject Horse;
	public Sprite Loris;
	public Sprite Owl;
	public Sprite Chemeleon;
	public Sprite Turtle;
	private int Progress = 0;
	public GameObject EggProgressButton;
	public GameObject PetEggPanel;
	private AudioSource PetBeLotteredAudio;
	public AudioSource GoatAudio;
	public AudioSource CatAudio;
	public AudioSource PigAudio;
	public AudioSource BearCatAudio;
	public AudioSource ChickenAudio;
	public AudioSource HorseAudio;
	public AudioSource LorisAudio;
	public AudioSource OwlAudio;
	public AudioSource ChameleonAudio;
	public AudioSource TurtleAudio;
	public Text PetText;
	private bool isOver;
	private int PetId;
	public AudioSource NewPet;
	public bool isTutorEgg;
	public GameObject NewEgg;
	public GameObject OriginEgg;
	public Animator NewEggAnimator;
	private float Chance;
	public GameObject RareEffect;
	private int EggNum;
	public GameObject WaitPanel;

	IEnumerator Check()
	{
		yield return new WaitForSeconds(.1f);
		Lotter();
	}
	

	public void Lotter()
	{
		Chance = 0;
		if(!isTutorEgg)
			Chance = Random.Range(0f,100f);
		else
			Chance = Random.Range(76f,100f);

		if(0<=Chance && Chance<=15)//GOat
		{
			PetManager.PetNumList.Add(1);
			PetId = 1;
//			PMM.GoatUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Goat.GetComponent<SpriteRenderer>().sprite;
			PetBeLotteredAudio = GoatAudio;
			PetText.text = "恭喜獲得 <color=#FFFFFF>口水羊</color>";
		}
		else if(Chance<=30)//Cat
		{
			PetManager.PetNumList.Add(2);
			PetId = 2;
//			PMM.CatUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Cat.GetComponent<SpriteRenderer>().sprite;
			PetBeLotteredAudio = CatAudio;
			PetText.text = "恭喜獲得 <color=#FFFFFF>厭世貓</color>";
		}
		else if(Chance<=45)//Pig
		{
			PetManager.PetNumList.Add(3);
			PetId = 3;
//			PMM.PigUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Pig.GetComponent<SpriteRenderer>().sprite;
			PetBeLotteredAudio = PigAudio;
			PetText.text = "恭喜獲得 <color=#FFFFFF>暴走豬</color>";
		}
		else if(Chance<=60)//Chicken
		{
			PetManager.PetNumList.Add(5);
			PetId = 5;
//			PMM.ChickenUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Chicken.GetComponent<SpriteRenderer>().sprite;
			PetBeLotteredAudio = ChickenAudio;
			PetText.text = "恭喜獲得 <color=#FFFFFF>窩母雞</color>";
		}
		else if(Chance<=75)//BearCat
		{
			PetManager.PetNumList.Add(4);
			PetId = 4;
//			PMM.BearCatUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = BearCat.GetComponent<SpriteRenderer>().sprite;
			PetBeLotteredAudio = BearCatAudio;
			PetText.text = "恭喜獲得 <color=#FFFFFF>小熊貓</color>";
		}
		else if(Chance<=80)//Horse
		{
			PetManager.PetNumList.Add(6);
			PetId = 6;
//			PMM.HorseUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Horse.GetComponent<SpriteRenderer>().sprite;
			PetBeLotteredAudio = HorseAudio;
			PetText.text = "恭喜獲得 <color=#FFCC22>糖果馬</color>";
		}
		else if(Chance<=85)//Loris
		{
			PetManager.PetNumList.Add(6);
			PetId = 7;
//			PMM.HorseUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Loris;
			PetBeLotteredAudio = LorisAudio;
			PetText.text = "恭喜獲得 <color=#FFCC22>懶猴王</color>";
		}
		else if(Chance<=90)//Owl
		{
			PetManager.PetNumList.Add(6);
			PetId = 8;
//			PMM.HorseUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Owl;
			PetBeLotteredAudio = OwlAudio;
			PetText.text = "恭喜獲得 <color=#FFCC22>木頭鷹</color>";
		}
		else if(Chance<=95)//CHemeleon
		{
			PetManager.PetNumList.Add(6);
			PetId = 9;
//			PMM.HorseUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Chemeleon;
			PetBeLotteredAudio = ChameleonAudio;
			PetText.text = "恭喜獲得 <color=#FFCC22>黃綠龍</color>";
		}
		else if(Chance<=100)//Turtle
		{
			PetManager.PetNumList.Add(6);
			PetId = 10;
//			PMM.HorseUnlock();
			PetImg.GetComponent<SpriteRenderer>().sprite = Turtle;
			PetBeLotteredAudio = TurtleAudio;
			PetText.text = "恭喜獲得 <color=#FFCC22>大方龜</color>";
		}

		if(PMM.GetReadInfoStatus())
		{
			Debug.Log("PetId = "+PetId);
			WaitPanel.SetActive(true);
			StartCoroutine(Wait(PetId));
			return;
		}


		FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("Pet").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;

				float PetNumNow = snapshot.ChildrenCount;
				DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString());
				mDatabaseRef.Child("PetId").SetValueAsync(PetId);
				mDatabaseRef.Child("PetLife").SetValueAsync(5);
				mDatabaseRef.Child("PetLevel").SetValueAsync(1);
				mDatabaseRef.Child("PetGrade").SetValueAsync(1);
				mDatabaseRef.Child("IsWarrior").SetValueAsync(0);
				mDatabaseRef.Child("PetExp").SetValueAsync(0);
				mDatabaseRef.Child("IsOnIsland").SetValueAsync(0);
				mDatabaseRef.Child("Burial").SetValueAsync(0);
			}
		});


//		CM.UpdateNum();
//		PlayerPrefs.SetInt("PetListNum",PetManager.PetNumList.Count);
//		PlayerPrefs.SetInt("Pet" + PetManager.PetNumList.Count , PetId);

		EggProgressButton.SetActive(true);
		PetEggPanel.SetActive(true);


	}

	IEnumerator Wait(int PetId)
	{
		yield return new WaitForSeconds(.5f);
		if(PMM.GetReadInfoStatus())
		{
			Debug.Log("WaitPMMRead...");
			StartCoroutine(Wait(PetId));
		}
		else
		{
			Debug.Log("PMM Read Over.");
			Debug.Log("PetId = "+PetId);
			EggProgressButton.SetActive(true);
			PetEggPanel.SetActive(true);	
			WaitPanel.SetActive(false);	
			
			FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("Pet").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;

				float PetNumNow = snapshot.ChildrenCount;
				DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("Pet").Child(PetNumNow.ToString());
				mDatabaseRef.Child("PetId").SetValueAsync(PetId);
				mDatabaseRef.Child("PetLife").SetValueAsync(5);
				mDatabaseRef.Child("PetLevel").SetValueAsync(1);
				mDatabaseRef.Child("PetGrade").SetValueAsync(1);
				mDatabaseRef.Child("IsWarrior").SetValueAsync(0);
				mDatabaseRef.Child("PetExp").SetValueAsync(0);
				mDatabaseRef.Child("IsOnIsland").SetValueAsync(0);
				mDatabaseRef.Child("Burial").SetValueAsync(0);
			}
			});
		}
	}

	public void EggProGress()
	{
		Progress += 1;
		EggNum += 1;
		if(Progress == 1)
		{
			PetEggPanel.GetComponent<Animator>().Play("PetEggOpen");
			NewEgg.SetActive(true);
			isOver = false;
			if(Chance >= 75)
				StartCoroutine(OpenRareEffect(EggNum));
			StartCoroutine(WaitLight(EggNum));
		}
		else if(Progress == 2)
		{
			isOver = true;
//			Light.SetActive(false);
			SetPetImg();
			StartCoroutine(ShowTextAndPlayeSound());
			NewEggAnimator.Play("Egg",0,.75f);
		}
		else if(Progress == 3)
		{
			if(isTutorEgg)
			{
				Debug.Log("!!!!!!!!!!!!!!!!");
				TutorIsland TI = GameObject.FindObjectOfType<TutorIsland>();
				TI.Next();
				return;
			}
			RareEffect.SetActive(false);
			EggProgressButton.SetActive(false);
			PetEggPanel.SetActive(false);
			Progress = 0;
			PetImg.SetActive(false);
			PetEggImg.SetActive(true);
			PetText.gameObject.SetActive(false);
			NewEgg.SetActive(false);
			OriginEgg.SetActive(true);
		}
	}

	public void TutorReset()
	{
			RareEffect.SetActive(false);
			NewEgg.SetActive(false);
			EggProgressButton.SetActive(false);
			PetEggPanel.SetActive(false);
			Progress = 0;
			PetImg.SetActive(false);
			PetEggImg.SetActive(true);
			PetText.gameObject.SetActive(false);
	}

	IEnumerator OpenRareEffect(int num)
	{
//		yield return new WaitForSeconds(1.85f);
		if(isOver)
			yield break;
		if(num != EggNum)
			yield break;
		RareEffect.SetActive(true);
	}

	IEnumerator WaitLight(int num)
	{
		SetPetImg();
		OriginEgg.SetActive(false);
		yield return new WaitForSeconds(3.5f);		
		if(isOver)
			yield break;
		if(num != EggNum)
		{
			Debug.Log("Next Egg!");
			yield break;
		}
		EggProGress();
		StartCoroutine(ShowTextAndPlayeSound());
	}

	void SetPetImg()
	{	
		PetImg.SetActive(true);
		PetEggImg.SetActive(false);
	}

	IEnumerator ShowTextAndPlayeSound()
	{
		NewPet.Play();
		PetText.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.4f);
		PetBeLotteredAudio.Play();
	}

}
