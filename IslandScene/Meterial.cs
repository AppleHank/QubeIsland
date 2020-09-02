using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class Meterial : MonoBehaviour {

	public static int wood;
	public static int rock;
	public static int metal;
	public static int cotton;
	public static int earth;
	public static int backbee;
	public static int diamandbee;
	public static int Food;
	public static int RecoverPotion;
	public static int HealthPotion;
	public static int UpFood;
	public static int Cake;
	public static int ChangeDoll;
	public static bool CanCake;
	public static int MineProgress;
	public BuildingManager BM;
	public static float AdTime = 0;


	public static void addwood(int amount)
	{
		wood += amount;
	}

	public static void addrock(int amount)
	{
		rock += amount;
	}

	public static void addmetal(int amount)
	{
		metal += amount;
	}

	public static int GetNum(string Name)
	{
		if(Name == "Wood")
			return wood;
		else if(Name == "Rock")
			return rock;
		else if(Name == "Metal")
			return metal;
		else if(Name == "Cotton")
			return cotton;
		else if(Name == "Earth")
			return earth;
		else if(Name == "Cake")
			return Cake;
		else if(Name == "Food")
			return Food;
		else if(Name == "UpFood")
			return UpFood;
		else if(Name == "RecoverPotion")
			return RecoverPotion;
		else if(Name == "HealthPotion")
			return HealthPotion;
		else if(Name == "Back")
			return backbee;
		else if(Name == "ChangeDoll")
			return ChangeDoll;
		return 0;
	}

	public static void AddMaterial(string name,int num)
	{
		int NumToDB = 0;
		if(name == "Wood")
		{
			wood += num;
			NumToDB = wood;
		}
		else if(name == "Rock")
		{
			rock += num;
			NumToDB = rock;
		}
		else if(name == "Metal")
		{
			metal += num;
			NumToDB = metal;
		}
		else if(name == "Cotton")
		{
			cotton += num;
			NumToDB = cotton;
		}
		else if(name == "Earth")
		{
			earth += num;
			NumToDB = earth;
		}
		else if(name == "Cake")
		{
			Cake += num;
			NumToDB = Cake;
		}
		else if(name == "Food")
		{
			Food += num;
			NumToDB = Food;
		}
		else if(name == "UpFood")
		{
			UpFood += num;
			NumToDB = UpFood;
		}
		else if(name == "RecoverPotion")
		{
			Debug.Log(RecoverPotion);
			RecoverPotion += num;
			Debug.Log(RecoverPotion);
			NumToDB = RecoverPotion;
			Debug.Log(RecoverPotion);
		}
		else if(name == "HealthPotion")
		{
			HealthPotion += num;
			NumToDB = HealthPotion;
		}
		else if(name == "ChangeDoll")
		{
			ChangeDoll += num;
			NumToDB = ChangeDoll;
		}
		else if(name == "Back")
		{
			backbee += num;
			NumToDB = backbee;
		}
		else if(name == "Diamand")
		{
			diamandbee += num;
			NumToDB = diamandbee;
		}
		BuildingManager buildingmanager = GameObject.FindObjectOfType<BuildingManager>();
		if(buildingmanager != null)
			buildingmanager.UpdateBuildingBlock();
		
		DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("Material");
		mDatabaseRef.Child(name).SetValueAsync(NumToDB);
	}

	public static void Reduce(string name,int num)
	{
		int NumToDB = 0;
		if(name == "Wood")
		{
			wood -= num;
			NumToDB = wood;
		}
		else if(name == "Rock")
		{
			rock -= num;
			NumToDB = rock;
		}
		else if(name == "Metal")
		{
			metal -= num;
			NumToDB = metal;
		}
		else if(name == "Cotton")
		{
			cotton -= num;
			NumToDB = cotton;
		}
		else if(name == "Earth")
		{
			earth -= num;
			NumToDB = earth;
		}
		else if(name == "Back")
		{
			backbee -= num;
			NumToDB = backbee;
		}
		else if(name == "Diamand")
		{
			diamandbee -= num;
			NumToDB = diamandbee;
		}
		else if(name == "Cake")
		{
			Cake -= num;
			NumToDB = Cake;
		}
		else if(name == "Food")
		{
			Food -= num;
			NumToDB = Food;
		}
		else if(name == "UpFood")
		{
			UpFood -= num;
			NumToDB = UpFood;
		}
		else if(name == "ChangeDoll")
		{
			ChangeDoll -= num;
			NumToDB = ChangeDoll;
		}
		
		else if(name == "RecoverPotion")
		{
			RecoverPotion -= num;
			NumToDB = RecoverPotion;
		}
		else if(name == "HealthPotion")
		{
			HealthPotion -= num;
			NumToDB = HealthPotion;
		}
		
		BuildingManager buildingmanager = GameObject.FindObjectOfType<BuildingManager>();
		buildingmanager.UpdateBuildingBlock();
		
		DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("Material");
		mDatabaseRef.Child(name).SetValueAsync(NumToDB);
	}

}
