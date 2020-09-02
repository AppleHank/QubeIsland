using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FireBaseMaterial
{
	public int Wood;
	public int Rock;
	public int Metal;
	public int Earth;
	public int Cotton;
	public int Back;
	public int Diamand;
	public int Cake;
	public int Food;
	public int UpFood;
	public int RecoverPotion;
	public int HealthPotion;
	public int UseCake;
	public int CanMine;
	public int ResetMaterial;
	public int LastSetMaterialDay;
	public int LastSetMaterialHour;
	public int LastSetMaterialMinute;
	public int CanAdTime;
	public int LastSetAdDay;
	public int LastSetAdHour;
	public int LastSetAdMinute;
	public int MaterialTime;
}

public class SetMaterial : MonoBehaviour {

	public Text WoodText;
	public Text RockText;
	public Text MetalText;
	public Text DiamandText;
	public Text BackBeeText;
	public Text EarthText;
	public Text CottonText;
	public Text CakeText;
	public Text FoodText;
	public Text UpFoodText;
	public Text RecoverPotionText;
	public Text HealthPotionText;
	public Text FoodNumText;
	public Text UpFoodNumText;

	[Header("Exchange")]
	public Text UpFoodCotton;
	public Text UpFoodRock;
	public Text UpFoodEarth;
	public Text UpFoodWood;
	public Text FoodCotton;
	public Text FoodRock;
	public Text FoodEarth;
	public Text FoodWood;

	public GameObject woodBlock;
	public GameObject rockBlock;
	public GameObject metalBlock;
	public GameObject cottonBlock;
	public GameObject earthBlock;
	public GameObject FoodBlock;
	public GameObject UpFoodBlock;
	public GameObject CakeBlock;
	public GameObject RecoverPotionBlock;
	public GameObject HealthPotionBlock;
	public GameObject CakeIcon;
	public GameObject CakeUseBlock;
	public GameObject MinePanel;
	public GameObject RecoverPotionUseBlock;
	public GameObject HealthPotionUseBlock;
	public AudioSource Mining;
	public AudioSource BGM;
	private int FirstMine;
	public GameObject MineTutor;
	public Image MineProgress;
	public GameObject ProgressBar;
	public BuildingManager BM;
	public BuildingMemory BMemory;
	public GameObject AdBlock;
	public Text AdBlockTime;
	private int CanAdTime;
	private bool CanWatchAd;
	private bool StartCountAdTime;
	public GameObject AfterWatchAdPanel;
	public bool isFriendIsland;
	public AdTest AdManager;
	private int MaterialTime;
	public Text MaterialTimeText;
	public GameObject WarningPanel;

	public void ResetSetMine()
	{
		
			DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString());
			reference.Child("CanMine").SetValueAsync(0);
			MineProgress.fillAmount = 0f;
	}

	public void ResetFirstMine()
	{
		
				DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString());
				reference.Child("FirstMine").SetValueAsync(1);
	}

	public void OpenAdButton()
	{

		AdBlock.SetActive(false);
		AdBlockTime.text = "領取獎勵!";
		AdBlockTime.color = Color.green;
				
	}

	public void CheckWarning()
	{
		if(MaterialTime == 0)
			WarningPanel.SetActive(true);
	}

	public void StopC()
	{
		Debug.Log("StopCoroutine");
		StopCoroutine("CountAdTime");
	}

	IEnumerator CountAdTime()
	{
		while(CanWatchAd)
		{
			yield return new WaitForSeconds(1f);
			if(Time.time > Meterial.AdTime + 60)
			{
				if(CanAdTime == 1)
					CanWatchAd = false;

				if(!AdManager.rewardBasedVideo.IsLoaded())
				{
					AdBlockTime.text = "等待網路...";
					AdManager.StartCheck();
				}
				else
					OpenAdButton();
			}

			else
			{
				float AdCountTimeSecond =  ((int)Meterial.AdTime + 300 - (int)Time.time) % 60;
				string AdCountTimeSecondstring = AdCountTimeSecond.ToString();
				if(AdCountTimeSecond <10)
					AdCountTimeSecondstring = "0" + AdCountTimeSecond;

				AdBlockTime.text = ((int)Meterial.AdTime + 300 - (int)Time.time)/60 + ":" + AdCountTimeSecondstring;
				AdBlockTime.color = Color.red;
				AdBlock.SetActive(true);
			}
		}
	}

	public async void WatchAd()
	{
		if(CanAdTime == 1)
		{
			CanWatchAd = false;
			AdBlockTime.text = "到達今日上限!";
		}
		else
		{
			Meterial.AdTime = Time.time;
			AdBlockTime.text = (((int)Meterial.AdTime + 300 - (int)Time.time)/60 + ":" + ((int)Meterial.AdTime + 300 - (int)Time.time) % 60).ToString();
		}
		AdBlockTime.color = Color.red;
		Meterial.AddMaterial("Diamand",10);
		AdBlock.SetActive(true);
		AfterWatchAdPanel.SetActive(true);
		
		int TimeinDb = await FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("ResetInfo").Child("CanAdTime").GetValueAsync().ContinueWith(task =>{
			if (task.IsFaulted) {
				Debug.Log("error");
				return 0;
			}
			else if (task.IsCompleted) { 
				DataSnapshot snapshot = task.Result;
				return int.Parse(snapshot.GetRawJsonValue());
			}
			return 0;
		});
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("ResetInfo").Child("CanAdTime");
		reference.SetValueAsync(TimeinDb - 1);
		CanAdTime = TimeinDb - 1;
	}

	async void Start()
	{
		
		Mining = GameObject.Find("Audiances/MiningBGMAudio ").GetComponent<AudioSource>();
		BGM = GameObject.Find("Audiances/BackGroundMusic").GetComponent<AudioSource>();

		Meterial.CanCake = false;
		InvokeRepeating("UpdateMaterialNum",0f,.25f);
		Debug.Log(PlayerData.UserId);
		Debug.Log("READ METERAIL0");
		FireBaseMaterial MaterialInfo = await FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).GetValueAsync().ContinueWith( task =>{

			FireBaseMaterial Error = new FireBaseMaterial();
			if (task.IsFaulted) {
				Debug.Log("error");
				return null;
			}
			else if (task.IsCompleted) { 
				DataSnapshot snapshot = task.Result;
				FireBaseMaterial LocalMaterialInfo = new FireBaseMaterial();
				LocalMaterialInfo.Wood = int.Parse(snapshot.Child("Material").Child("Wood").GetRawJsonValue());
				LocalMaterialInfo.Rock = int.Parse(snapshot.Child("Material").Child("Rock").GetRawJsonValue());
				LocalMaterialInfo.Metal = int.Parse(snapshot.Child("Material").Child("Metal").GetRawJsonValue());
				LocalMaterialInfo.Earth = int.Parse(snapshot.Child("Material").Child("Earth").GetRawJsonValue());
				LocalMaterialInfo.Cotton = int.Parse(snapshot.Child("Material").Child("Cotton").GetRawJsonValue());
				LocalMaterialInfo.Diamand = int.Parse(snapshot.Child("Material").Child("Diamand").GetRawJsonValue());
				LocalMaterialInfo.Back = int.Parse(snapshot.Child("Material").Child("Back").GetRawJsonValue());
				LocalMaterialInfo.Cake = int.Parse(snapshot.Child("Material").Child("Cake").GetRawJsonValue());
				LocalMaterialInfo.Food = int.Parse(snapshot.Child("Material").Child("Food").GetRawJsonValue());
				LocalMaterialInfo.UpFood = int.Parse(snapshot.Child("Material").Child("UpFood").GetRawJsonValue());
//				LocalMaterialInfo.ResetMaterial = int.Parse(snapshot.Child("ResetMaterial").GetRawJsonValue());
				LocalMaterialInfo.RecoverPotion = int.Parse(snapshot.Child("Material").Child("RecoverPotion").GetRawJsonValue());
				LocalMaterialInfo.HealthPotion = int.Parse(snapshot.Child("Material").Child("HealthPotion").GetRawJsonValue());
				LocalMaterialInfo.UseCake = int.Parse(snapshot.Child("UseCake").GetRawJsonValue());
				LocalMaterialInfo.CanMine = int.Parse(snapshot.Child("CanMine").GetRawJsonValue());

				Debug.Log("------------------========================");

				LocalMaterialInfo.LastSetMaterialDay = int.Parse(snapshot.Child("ResetInfo").Child("LastSetMaterialDay").GetRawJsonValue());
				
				Debug.Log("------------------================kko;k;lkl;========");
				
				LocalMaterialInfo.LastSetMaterialHour = int.Parse(snapshot.Child("ResetInfo").Child("LastSetMaterialHour").GetRawJsonValue());
				LocalMaterialInfo.LastSetMaterialMinute = int.Parse(snapshot.Child("ResetInfo").Child("LastSetMaterialMinute").GetRawJsonValue());

				Debug.Log("------------------============jhhhhhhhhhhhhhhhhhhh============");
				LocalMaterialInfo.CanAdTime = int.Parse(snapshot.Child("ResetInfo").Child("CanAdTime").GetRawJsonValue());
				LocalMaterialInfo.LastSetAdDay = int.Parse(snapshot.Child("ResetInfo").Child("LastSetAdDay").GetRawJsonValue());
				LocalMaterialInfo.LastSetAdHour = int.Parse(snapshot.Child("ResetInfo").Child("LastSetAdHour").GetRawJsonValue());
				LocalMaterialInfo.LastSetAdMinute = int.Parse(snapshot.Child("ResetInfo").Child("LastSetAdMinute").GetRawJsonValue());
				LocalMaterialInfo.MaterialTime = int.Parse(snapshot.Child("MaterialTime").GetRawJsonValue());

				FirstMine = int.Parse(snapshot.Child("FirstMine").GetRawJsonValue());
				return LocalMaterialInfo;
			}
			return Error;
		}); 

		
/*		DateTime now = DateTime.Now;
		if(now.ToString().Substring(300,2) == "上午" & now.ToString().Substring(13,2) == "05") 
		{
			if(MaterialInfo.ResetMaterial != 1)
			{	
				DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString());
				reference.Child("ResetMaterial").SetValueAsync(1);
				reference.Child("MagterialTime").SetValueAsync(3);
			}
		}
*/ 
		MaterialTime = MaterialInfo.MaterialTime;
		MaterialTimeText.text = "今日剩餘次數："+ MaterialTime +"/3";


		int Day = int.Parse(System.DateTime.Now.ToString("dd"));
		int Hour = int.Parse(System.DateTime.Now.ToString("HH"));
		int Minute = int.Parse(System.DateTime.Now.ToString("mm"));
		Debug.Log("-----------------------"+Day);
		Debug.Log(Hour);
		Debug.Log(Minute);

		Debug.Log(MaterialInfo.LastSetMaterialDay);
		Debug.Log(MaterialInfo.LastSetMaterialHour);
		Debug.Log(MaterialInfo.LastSetMaterialMinute);
		Debug.Log("--------------------------");
		

		if(Day != MaterialInfo.LastSetMaterialDay)//Math.abs != 0
		{
		/* 	if(Mathf.Abs(Day-MaterialInfo.LastSetMaterialDay) >= 2)
			{
				Debug.Log("Over 1 Day");
				ResetMaterialTime();
			}
			else//1Day
			{
				int IntervalMinute = (60-MaterialInfo.LastSetMaterialMinute) + Minute;
				int IntervalHour = (24-MaterialInfo.LastSetMaterialHour-1) + Hour;
				Debug.Log("IntervalHour:"+IntervalHour);
				Debug.Log("IntervalMinute:"+IntervalMinute);
				int TotalIntervalMinute = IntervalHour * 60 + IntervalMinute;
				Debug.Log("TotalIntevalMinute:"+TotalIntervalMinute);
				Debug.Log("ResetTime needed Interval:"+24*60);*/
		//		if(TotalIntervalMinute >= 24*60)
		//		{
					Debug.Log("ResetMaterail!!!!!!");
					ResetMaterialTime();
		//		}
		//	}
		}		

		if(MaterialInfo.CanAdTime > 0 )
		{
			CanAdTime = MaterialInfo.CanAdTime;
			Debug.Log("CanAdTime:"+CanAdTime);
			StartCountAdTime = true;
			CanWatchAd = true;
			StartCoroutine(CountAdTime());
		}
		else
		{
			AdBlockTime.text = "到達今日上限!";
		}
		if(Day != MaterialInfo.LastSetAdDay)//Math.abs != 0
		{
		/* 	if(Mathf.Abs(Day-MaterialInfo.LastSetAdDay) >= 2)
			{
				Debug.Log("Ad Over 1 Day");
				ResetAdTime();
			}
			else//1Day
			{
				int IntervalMinute = (60-MaterialInfo.LastSetAdMinute) + Minute;
				int IntervalHour = (24-MaterialInfo.LastSetAdHour-1) + Hour;
				Debug.Log("AD IntervalHour:"+IntervalHour);
				Debug.Log("AD IntervalMinute:"+IntervalMinute);
				int TotalIntervalMinute = IntervalHour * 60 + IntervalMinute;
				Debug.Log("AD TotalIntevalMinute:"+TotalIntervalMinute);
				Debug.Log("AD ResetTime needed Interval:"+24*60);
				if(TotalIntervalMinute >= 24*60)*/
		//		{
					Debug.Log("ResetAD!!!!!!");
					ResetAdTime();
		//		}
		//	}
		}


		Meterial.wood = MaterialInfo.Wood;
		Meterial.rock = MaterialInfo.Rock;
		Meterial.metal = MaterialInfo.Metal;
		Meterial.earth = MaterialInfo.Earth;
		Meterial.cotton = MaterialInfo.Cotton;
		Meterial.diamandbee = MaterialInfo.Diamand;
		Meterial.backbee = MaterialInfo.Back;
		Meterial.Cake = MaterialInfo.Cake;
		Meterial.Food = MaterialInfo.Food;
		Meterial.UpFood = MaterialInfo.UpFood;
		Meterial.RecoverPotion = MaterialInfo.RecoverPotion;
		Meterial.HealthPotion = MaterialInfo.HealthPotion;
			Debug.Log("READ METERAIL9");
		Debug.Log(PlayerData.UserId.ToString());
		Debug.Log(MaterialInfo.UseCake);
		if(MaterialInfo.UseCake == 1)
		{			
			CakeUseBlock.SetActive(true);
			CakeIcon.SetActive(true);
			Meterial.CanCake = true;
			Debug.Log("CCCCCCCCCCCAAAAAAAAAAAKKKKKK");
		}
		if(MaterialInfo.CanMine == 2)
		{
			
			Debug.Log("MINININININININININIIN");
			if(FirstMine == 0)
			{
				ProgressBar.SetActive(false);
				MinePanel.SetActive(true);
				MineTutor.SetActive(true);
			}
		}
		if(MaterialInfo.CanMine >= 5)
		{
				MinePanel.SetActive(true);
				ProgressBar.SetActive(false);
		}
		else
		{
			MineProgress.fillAmount = (float)MaterialInfo.CanMine / 5f;
			Meterial.MineProgress = MaterialInfo.CanMine;
		}
		BuildingManager BM = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
		BM.UpdateBuildingBlock();
		Debug.Log("Start BM");
		BMemory.StartAttatchDB();

	}

	public void ResetMaterialTime()
	{
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString());
		reference.Child("ResetInfo").Child("LastSetMaterialDay").SetValueAsync(int.Parse(System.DateTime.Now.ToString("dd")));
		reference.Child("ResetInfo").Child("LastSetMaterialHour").SetValueAsync(int.Parse(System.DateTime.Now.ToString("HH")));
		reference.Child("ResetInfo").Child("LastSetMaterialMinute").SetValueAsync(int.Parse(System.DateTime.Now.ToString("mm")));
		reference.Child("MaterialTime").SetValueAsync(3);
		MaterialTimeText.text = "今日剩餘次數：3/3";
	}

	public void ResetAdTime()
	{
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("ResetInfo");
		reference.Child("LastSetAdDay").SetValueAsync(int.Parse(System.DateTime.Now.ToString("dd")));
		reference.Child("LastSetAdHour").SetValueAsync(int.Parse(System.DateTime.Now.ToString("HH")));
		reference.Child("LastSetAdMinute").SetValueAsync(int.Parse(System.DateTime.Now.ToString("mm")));
		reference.Child("CanAdTime").SetValueAsync(5);
		CanAdTime = 5;
		if(!StartCountAdTime)
		{
			Debug.Log("Reset Ad Over,Start Count!");
			CanWatchAd = true;
			StartCoroutine(CountAdTime());
		}
	}

	public void EndMining()
	{
		Mining.Stop();
		BGM.Play();
	}

	public void StartMining()
	{
		Mining.Play();
		BGM.Stop();
	}

	void UpdateMaterialNum () {
	
		if(isFriendIsland)
			return;

		WoodText.text = Meterial.wood.ToString();
		RockText.text = Meterial.rock.ToString();
		MetalText.text = Meterial.metal.ToString();
		EarthText.text = Meterial.earth.ToString();
		CottonText.text = Meterial.cotton.ToString();
		DiamandText.text = Meterial.diamandbee.ToString();
		BackBeeText.text = Meterial.backbee.ToString();
		CakeText.text = Meterial.Cake.ToString();
		FoodText.text = Meterial.Food.ToString();
		UpFoodText.text = Meterial.UpFood.ToString();
		RecoverPotionText.text = Meterial.RecoverPotion.ToString();
		HealthPotionText.text = Meterial.HealthPotion.ToString();
		FoodNumText.text = Meterial.Food.ToString();
		UpFoodNumText.text = Meterial.UpFood.ToString();

		UpFoodCotton.text = Meterial.cotton.ToString();
		UpFoodRock.text = Meterial.rock.ToString();
		UpFoodEarth.text = Meterial.earth.ToString();
		UpFoodWood.text = Meterial.wood.ToString();
		FoodCotton.text = Meterial.cotton.ToString();
		FoodRock.text = Meterial.rock.ToString();
		FoodEarth.text = Meterial.earth.ToString();
		FoodWood.text = Meterial.wood.ToString();

		if(Meterial.wood <= 0)
			woodBlock.SetActive(true);
		else 
			woodBlock.SetActive(false);
		if(Meterial.rock <= 0)
			rockBlock.SetActive(true);
		else 
			rockBlock.SetActive(false);
		if(Meterial.metal <= 0)
			metalBlock.SetActive(true);
		else 
			metalBlock.SetActive(false);
		if(Meterial.earth <= 0)
			earthBlock.SetActive(true);
		else 
			earthBlock.SetActive(false);
		if(Meterial.cotton <= 0)
			cottonBlock.SetActive(true);
		else 
			cottonBlock.SetActive(false);
		if(Meterial.Cake <= 0)
		{
			CakeBlock.SetActive(true);
			CakeUseBlock.SetActive(true);
		}
		else 
		{
			CakeBlock.SetActive(false);
			if(!Meterial.CanCake)
				CakeUseBlock.SetActive(false);
		}
		if(Meterial.Food <= 0)
			FoodBlock.SetActive(true);
		else 
			FoodBlock.SetActive(false);
		if(Meterial.UpFood <= 0)
			UpFoodBlock.SetActive(true);
		else 
			UpFoodBlock.SetActive(false);
		if(Meterial.RecoverPotion <= 0)
		{
			RecoverPotionBlock.SetActive(true);
			RecoverPotionUseBlock.SetActive(true);
		}
		else 
		{
			RecoverPotionBlock.SetActive(false);
			RecoverPotionUseBlock.SetActive(false);
		}
		if(Meterial.HealthPotion <= 0)
		{
			HealthPotionBlock.SetActive(true);
			HealthPotionUseBlock.SetActive(true);
		}
		else 
		{
			HealthPotionBlock.SetActive(false);
			HealthPotionUseBlock.SetActive(false);
		}
	}	
	
	public void Cake()
	{
		CakeUseBlock.SetActive(true);
		Meterial.Reduce("Cake",1);
		Meterial.CanCake = true;
		CakeIcon.SetActive(true);
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("UseCake");
		reference.SetValueAsync(1);

/* 		NetPlayer[] NPs = FindObjectsOfType<NetPlayer>();
		foreach(NetPlayer NP in NPs)
		{
			if(NP.isLocalPlayer)
			{
				NP.CmdCake();
			}
		}*/
	}
	
}
