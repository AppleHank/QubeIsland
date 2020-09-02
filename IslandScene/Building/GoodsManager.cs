using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GoodsManager : MonoBehaviour {

	public GameObject NormalTreeBlock;
	public GameObject PinkTreeBlock;
	public GameObject MintTreeBlock;
	public GameObject PuddingTreeBlock;
	public GameObject GrapeTreeBlock;
	public GameObject TrebleTreeBlock;
	public GameObject GreenTreeBlock;
	public GameObject RustTreeBlock;
	public GameObject RustForestBlock;
	public GameObject FirBlock;
	public GameObject LittleFirForestBlock;
	public GameObject FirForestBlock;
	public GameObject DreamForestBlock;
	public GameObject LittleForestBlock;
	public GameObject ChristmasTreeBlock;
	public GameObject HaveNormalTree;
	public GameObject HaveMintTree;
	public GameObject HavePinkTree;
	public GameObject HavePuddingTree;
	public GameObject HaveGrapeTree;
	public GameObject HaveGreenTree;
	public GameObject HaveTrebleTree;
	public GameObject HaveRustTree;
	public GameObject HaveRustForest;
	public GameObject HaveFir;
	public GameObject HaveLittleFirForest;
	public GameObject HaveFirForest;
	public GameObject HaveDreamForest;
	public GameObject HaveLittleForest;
	public GameObject HaveChristmasTree;

	public GameObject MintTreeBuild;
	public GameObject PinkTreeBuild;
	public GameObject GreenTreeBuild;
	public GameObject PuddingTreeBuild;
	public GameObject GrapeTreeBuild;
	public GameObject TrebleTreeBuild;
	public GameObject RustTreeBuild;
	public GameObject RustForestBuild;
	public GameObject FirBuild;
	public GameObject LittleFirForestBuild;
	public GameObject FirForestBuild;
	public GameObject DreamForestBuild;
	public GameObject LittleForestBuild;
	public GameObject ChristmasTreeBuild;

	private DatabaseReference reference;

	class FireBaseBlockStatus
	{
		public int PinkBlock;
		public int MintBlock;
		public int PuddingBlock;
		public int GrapeBlock;
		public int TrebleBlock;
		public int GreenBlock;
		public int RustTreeBlock;
		public int FirBlock;
		public int ChristmasTreeBlock;
		public int PuddingTreeBlock;
		public int GrapeTreeBlock;
		public int LittleFirForestBlock;
		public int FirForestBlock;
		public int RustForestBlock;
		public int LittleForestBlock;
		public int DreamForestBlock;

	}

	async void Start()
	{
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		Debug.Log(PlayerData.UserId);
		FireBaseBlockStatus BlockStatus = await reference.Child("users").Child(PlayerData.UserId.ToString()).GetValueAsync().ContinueWith(task => {
			FireBaseBlockStatus error = new FireBaseBlockStatus();
			if (task.IsFaulted) {
					Debug.Log("error");
				}
			else if (task.IsCompleted) {
				FireBaseBlockStatus LocalBlockStatus = new FireBaseBlockStatus();
				DataSnapshot snapshot = task.Result;
				LocalBlockStatus.PinkBlock = int.Parse(snapshot.Child("Unlock").Child("PinkTree").GetRawJsonValue());
				LocalBlockStatus.MintBlock = int.Parse(snapshot.Child("Unlock").Child("MintTree").GetRawJsonValue());
				LocalBlockStatus.PuddingBlock = int.Parse(snapshot.Child("Unlock").Child("PuddingTree").GetRawJsonValue());
				LocalBlockStatus.GrapeBlock = int.Parse(snapshot.Child("Unlock").Child("GrapeTree").GetRawJsonValue());
				LocalBlockStatus.TrebleBlock = int.Parse(snapshot.Child("Unlock").Child("TrebleTree").GetRawJsonValue());
				LocalBlockStatus.GreenBlock = int.Parse(snapshot.Child("Unlock").Child("GreenTree").GetRawJsonValue());
				LocalBlockStatus.RustTreeBlock = int.Parse(snapshot.Child("Unlock").Child("RustTree").GetRawJsonValue());
				LocalBlockStatus.RustForestBlock = int.Parse(snapshot.Child("Unlock").Child("RustForest").GetRawJsonValue());
				LocalBlockStatus.FirBlock = int.Parse(snapshot.Child("Unlock").Child("Fir").GetRawJsonValue());
				LocalBlockStatus.LittleFirForestBlock = int.Parse(snapshot.Child("Unlock").Child("LittleFirForest").GetRawJsonValue());
				LocalBlockStatus.FirForestBlock = int.Parse(snapshot.Child("Unlock").Child("FirForest").GetRawJsonValue());
				LocalBlockStatus.LittleForestBlock = int.Parse(snapshot.Child("Unlock").Child("LittleForest").GetRawJsonValue());
				LocalBlockStatus.DreamForestBlock = int.Parse(snapshot.Child("Unlock").Child("DreamForest").GetRawJsonValue());
				LocalBlockStatus.ChristmasTreeBlock = int.Parse(snapshot.Child("Unlock").Child("ChristmasTree").GetRawJsonValue());
				return LocalBlockStatus;
			}
			return error;	
		});

		Debug.Log(BlockStatus.MintBlock);

		if(BlockStatus.MintBlock == 1)
			UnlockMintTree();
		if(BlockStatus.PinkBlock == 1)
			UnlockPinkTree();
		if(BlockStatus.PuddingBlock == 1)
			UnlockPuddingTree();
		if(BlockStatus.GrapeBlock == 1)
			UnlockGrapeTree();
		if(BlockStatus.TrebleBlock == 1)
			UnlockTrebleTree();
		if(BlockStatus.GreenBlock == 1)
			UnlockGreenTree();
		if(BlockStatus.RustTreeBlock == 1)
			UnlockRustTree();
		if(BlockStatus.RustForestBlock == 1)
			UnlockRustForest();
		if(BlockStatus.FirBlock == 1)
			UnlockFir();
		if(BlockStatus.FirForestBlock == 1)
			UnlockFirForest();
		if(BlockStatus.LittleFirForestBlock == 1)
			UnlockLittleFirForest();
		if(BlockStatus.LittleForestBlock == 1)
			UnlockLittleForest();
		if(BlockStatus.DreamForestBlock == 1)
			UnlockDreamForest();
		if(BlockStatus.ChristmasTreeBlock == 1)
			UnlockChristmasTree();


/* 		if(PlayerPrefs.GetInt("NormalTree",0) == 1)
			UnlockNormalTree();
		if(PlayerPrefs.GetInt("MintTree",0) == 1)
			UnlockMintTree();
		if(PlayerPrefs.GetInt("PinkTree",0) == 1)
			UnlockPinkTree();
		if(PlayerPrefs.GetInt("PuddingTree",0) == 1)
			UnlockPuddingTree();
		if(PlayerPrefs.GetInt("GrapeTree",0) == 1)
			UnlockGrapeTree();*/
	}

	public void UnlockMintTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("MintTree").SetValueAsync(1);
		MintTreeBlock.SetActive(false);
		Unlock.MintTree = true;
		MintTreeBuild.SetActive(true);
		HaveMintTree.SetActive(true);
	}
	public void UnlockPinkTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("PinkTree").SetValueAsync(1);
		PinkTreeBlock.SetActive(false);
		Unlock.PinkTree = true;
		PinkTreeBuild.SetActive(true);
		HavePinkTree.SetActive(true);
	}
	public void UnlockPuddingTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("PuddingTree").SetValueAsync(1);
		PuddingTreeBlock.SetActive(false);
		Unlock.PuddingTree = true;
		PuddingTreeBuild.SetActive(true);
		HavePuddingTree.SetActive(true);
	}	
	public void UnlockGrapeTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("GrapeTree").SetValueAsync(1);
		GrapeTreeBlock.SetActive(false);
		Unlock.GrapeTree = true;
		GrapeTreeBuild.SetActive(true);
		HaveGrapeTree.SetActive(true);
	}
	public void UnlockTrebleTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("TrebleTree").SetValueAsync(1);
		TrebleTreeBlock.SetActive(false);
		HaveTrebleTree.SetActive(true);
		TrebleTreeBuild.SetActive(true);
	}
	public void UnlockGreenTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("GreenTree").SetValueAsync(1);
		GreenTreeBlock.SetActive(false);
		HaveGreenTree.SetActive(true);
		GreenTreeBuild.SetActive(true);
	}
	public void UnlockRustTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("RustTree").SetValueAsync(1);
		RustTreeBlock.SetActive(false);
		HaveRustTree.SetActive(true);
		RustTreeBuild.SetActive(true);
	}
	public void UnlockRustForest()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("RustForest").SetValueAsync(1);
		RustForestBlock.SetActive(false);
		HaveRustForest.SetActive(true);
		RustForestBuild.SetActive(true);
	}
	public void UnlockFir()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("Fir").SetValueAsync(1);
		FirBlock.SetActive(false);
		HaveFir.SetActive(true);
		FirBuild.SetActive(true);
	}
	public void UnlockFirForest()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("FirForest").SetValueAsync(1);
		FirForestBlock.SetActive(false);
		HaveFirForest.SetActive(true);
		FirForestBuild.SetActive(true);
	}
	public void UnlockLittleFirForest()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("LittleFirForest").SetValueAsync(1);
		LittleFirForestBlock.SetActive(false);
		HaveLittleFirForest.SetActive(true);
		LittleFirForestBuild.SetActive(true);
	}
	public void UnlockDreamForest()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("DreamForest").SetValueAsync(1);
		DreamForestBlock.SetActive(false);
		HaveDreamForest.SetActive(true);
		DreamForestBuild.SetActive(true);
	}
	public void UnlockLittleForest()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("LittleForest").SetValueAsync(1);
		LittleForestBlock.SetActive(false);
		HaveLittleForest.SetActive(true);
		LittleForestBuild.SetActive(true);
	}
	public void UnlockChristmasTree()
	{
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Unlock").Child("ChristmasTree").SetValueAsync(1);
		ChristmasTreeBlock.SetActive(false);
		HaveChristmasTree.SetActive(true);
		ChristmasTreeBuild.SetActive(true);
	}

}
