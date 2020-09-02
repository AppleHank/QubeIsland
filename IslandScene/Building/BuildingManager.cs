using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading; 
using System.Threading.Tasks;
using System.Linq;

public class BuildingManager : MonoBehaviour {

	public BuildingBlueprint Cave;
	public BuildingBlueprint WareHouse;
	public BuildingBlueprint Canoe;
	public BuildingBlueprint FireCamp;
	public BuildingBlueprint IceHouse;
	public BuildingBlueprint HolyChurch;
	public BuildingBlueprint TutorCave;
	public BuildingBlueprint NormalTree;
	public BuildingBlueprint GreenTree;
	public BuildingBlueprint MintTree;
	public BuildingBlueprint PinkTree;
	public BuildingBlueprint PuddingTree;
	public BuildingBlueprint GrapeTree;
	public BuildingBlueprint LittleFroest;
	public BuildingBlueprint LittleFir;
	public BuildingBlueprint TrebleTree;
	public BuildingBlueprint Fir;
	public BuildingBlueprint FirFroest;
	public BuildingBlueprint DeadTree;
	public BuildingBlueprint DeadTreeFroest;
	public BuildingBlueprint ChristmasTree;
	public BuildingBlueprint DreamFroest;
	public BuildingBlueprint UFCBlueprint;
	public BuildingBlueprint UHCBlueprint;
	public BuildingBlueprint UIHBlueprint;

	private BuildingBlueprint BuildingBlueprintToBuild;
	private GameObject PreBuilding;
	private GameObject PreBuildingNow;
	private bool ForGiveEditNow;
	public GameObject FireCampUpgradePanel;
	public GameObject IceHouseUpgradePanel;
	public GameObject HolyChurchUpgradePanel;

	public PetMoveManager PMM;
	[Header("StoredBuildng")]
	public StoredBuilding StoredBuildingPrefab;
	public GameObject StoredList;
	private Vector3 LastStoredPosition;
	private List<GameObject> StoredBuildingButtonsThisEdit = new List<GameObject>();
	public List<StoredBuilding> StoredBuildingButtons = new List<StoredBuilding>();
	private StoredBuilding LastTouchedStoreButton;
	private List<string> StoredBuildingName = new List<string>();
	private bool PutStoredBuilding;
	private int StoredBuildingTempTreeNum;
	private int ContinuousCickButtonNum;

	public GameObject CaveCanvas;
	public GameObject CaveManager;
	public GameObject UI;
	public GameObject EditingBuilding;
	public GameObject EditingBuildingNow;
	public static bool isEditMode;//Decision MovingBuliding if enter press sensitive
	public static bool isEditNow;
	public static bool isBuildingMode;
	private BuildingBlueprint BuildingTobuild;
	private bool ClickedButton;
	public GameObject ReverseEditBuildingButton;
	public GameObject ReversePreBuildingButton;

	private BuildingMemory BuildingMemory;
	public Image AnimationNormalSprite;
	public Image AnimationUpgradeSprite;
	public Sprite FireCampNormal;
	public Sprite FireCampUpgrade;
	public Sprite IceHouseNormal;
	public Sprite IceHouseUpgrade;
	public Sprite HolyChurchNormal;
	public Sprite HolyChurchUpgrade;
	public GameObject EditCanvas;
	public GameObject BuildCanvas;
//	private DataSnapshot snapshot;
	private GameObject BuildNodeNow;
	private Vector3 touchPosWorld;
	private Vector3 LastPreBuildingPos;
	public GameObject BuildNode;
	private Vector3 RayCastPosition;
	private List<GameObject> EditBuildings = new List<GameObject>();
	private List<GameObject> BuildEditBuildings = new List<GameObject>();
	private bool PositionError;
	public GameObject EditFuck;
	private string LastTreeName;
	private GameObject LastEditBuilding;
	private GameObject ReversedBuilding;
	private DatabaseReference reference;
	public bool isFlip;
	private Vector3 EditingBuildingOriginPos;
	private BuildingBlueprint EditingBuildingOriginBlueprint;
	private BuildNode EditBuildingOriginNode;
	public bool GoBack; //if GoBack == true; EditingBuilding return to originpos;
	private bool WaitForTreeAsync;
	public AudioSource BuildAudio;
	public AudioSource Upgraded;
	public AudioSource Upgrading;
	public IllustrationManager IM;
	private SpawnUI UIspawn;
	public GameObject NodesPrefab;
	public List<GameObject> PopCanvas = new List<GameObject>();
	public List<StoredBuilding> StoredBuildingButonList = new List<StoredBuilding>();
	public Vector2 LastStroedButtonPosition = new Vector2(0,0);
	public DecoratingManager DM;
	private string NowPutDecoratingName;


	public GameObject Test;



	public GameObject GetNodes()
	{
		return BuildNode;
	}

	public void SetBM(BuildingMemory BM)
	{
		BuildingMemory = BM;
	}

	public void Start()
	{
	//	UIspawn = GameObject.Find("UISpawn").GetComponent<SpawnUI>();
		Debug.Log("BuildingManager Start");
	//	Meterial.Test();
		AssignText();
//		BuildNode = UIspawn.BuildNode;
//	EditCanvas = UIspawn.EditCanvas;
//		BuildCanvas = UIspawn.BuildCanvas;
	//	StoredList = GameObject.Find("CancelEditCanvas/BackUpBackGroundImage/StoredBuildings");
		BuildNode = Instantiate(NodesPrefab,new Vector3(-1.06f,-20,0),Quaternion.identity);
		BuildNode[] Nodes = GameObject.FindObjectsOfType<BuildNode>();
		foreach(BuildNode node in Nodes)
		{
			node.SetBM(this);
		}
//		BuildingMemory = GameObject.Find("Memory").GetComponent<BuildingMemory>();
		LastStoredPosition = new Vector3(-975,88,0);
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		Debug.Log("reference Complete");
/* 		reference.Child("users").GetValueAsync().ContinueWith(task => {
			Debug.Log("BuilddingManager FireBase Start");
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				Debug.Log("BuilddingManager FireBase Task Complete");
				snapshot = task.Result;
			}
			
			Debug.Log("BuilddingManager FireBase End");
		});*/
		Debug.Log("BuildingManager End");
	}

	public void AssignText()
	{
	//	Cave.SetMaterial();
	//	Canoe.SetMaterial();
	//	WareHouse.SetMaterial();
		FireCamp.SetMaterial();
	//	FireCamp.CanUpgrade();
		IceHouse.SetMaterial();
	//	IceHouse.CanUpgrade();
		HolyChurch.SetMaterial();
	//	HolyChurch.CanUpgrade();
		NormalTree.SetMaterial();
		MintTree.SetMaterial();
		PinkTree.SetMaterial();
		TrebleTree.SetMaterial();
		GreenTree.SetMaterial();
		PuddingTree.SetMaterial();
		GrapeTree.SetMaterial();
		DeadTree.SetMaterial();
		DeadTreeFroest.SetMaterial();
		Fir.SetMaterial();
		FirFroest.SetMaterial();
	//	LittleFir.SetMaterial();
		DreamFroest.SetMaterial();
		LittleFroest.SetMaterial();
		ChristmasTree.SetMaterial();
	}

	public void UpdateBuildingBlock()
	{
		
		FireCamp.CanUpgrade();
		IceHouse.CanUpgrade();
		HolyChurch.CanUpgrade();
		GreenTree.CanBuild();
		NormalTree.CanBuild();
		MintTree.CanBuild();
		PinkTree.CanBuild();
		PuddingTree.CanBuild();
		GrapeTree.CanBuild();
		TrebleTree.CanBuild();
		DeadTree.CanBuild();
		Fir.CanBuild();
		ChristmasTree.CanBuild();
	//	LittleFir.CanBuild();
		FirFroest.CanBuild();
		DeadTreeFroest.CanBuild();
		LittleFroest.CanBuild();
		DreamFroest.CanBuild();
	}

	public void SetClickButton()
	{
		ClickedButton = true;
		StartCoroutine(ResetClickedButton());
	}

	IEnumerator ResetClickedButton()
	{
		yield return new WaitForSeconds(.2f);
		ClickedButton = false;
	}

	public bool GetClickedButton()
	{
		return ClickedButton;
	}

	public void AssignCaveToBuild()
	{
		BuildingBlueprintToBuild = Cave;
	}

	public void AssignCanoeToBuild()
	{
		BuildingBlueprintToBuild = Canoe;
	}

	public void AssignWareHouseToBuild()
	{
		BuildingBlueprintToBuild = WareHouse;
	}

	public void AssignFireCampToBuild()
	{
		BuildingBlueprintToBuild = FireCamp;
	}

	public void AssignIceHouseToBuild()
	{
		BuildingBlueprintToBuild = IceHouse;
	}

	public void AssignHolyChurchToBuild()
	{
		BuildingBlueprintToBuild = HolyChurch;
	}
	public void AssignGreenTreeToBuild()
	{
		BuildingBlueprintToBuild = GreenTree;
	}

	public void AssignTutorCaveToBuild()
	{
		BuildingBlueprintToBuild = TutorCave;
	}

	public void AssignNormalTreeToBuild()
	{
		BuildingBlueprintToBuild = NormalTree;
	}
	public void AssignMintTreeToBuild()
	{
		BuildingBlueprintToBuild = MintTree;
	}
	public void AssignPinkTreeToBuild()
	{
		BuildingBlueprintToBuild = PinkTree;
	}
	public void AssignPuddingTreeToBuild()
	{
		BuildingBlueprintToBuild = PuddingTree;
	}
	public void AssignGrapeTreeToBuild()
	{
		BuildingBlueprintToBuild = GrapeTree;
	}
	public void AssignTrebleTreeToBuild()
	{
		BuildingBlueprintToBuild = TrebleTree;
	}
	public void AssignLittleFroestToBuild()
	{
		BuildingBlueprintToBuild = LittleFroest;
	}
	public void AssignLittleFirToBuild()
	{
		BuildingBlueprintToBuild = LittleFir;
	}
	public void AssignFirToBuild()
	{
		BuildingBlueprintToBuild = Fir;
	}
	public void AssignFirFroestToBuild()
	{
		BuildingBlueprintToBuild = FirFroest;
	}
	public void AssignDeadTreeToBuild()
	{
		BuildingBlueprintToBuild = DeadTree;
	}
	public void AssignDeadTreeFroestToBuild()
	{
		BuildingBlueprintToBuild = DeadTreeFroest;
	}
	public void AssignChristmasTreeToBuild()
	{
		BuildingBlueprintToBuild = ChristmasTree;
	}
	public void AssignDreamFroestToBuild()
	{
		BuildingBlueprintToBuild = DreamFroest;
	}

	public void PutDecorating(string Name)
	{
		NowPutDecoratingName = Name;
	}

	public void ResetDecoratingName()
	{
		NowPutDecoratingName = null;
	}

	public void AssignPreBuilding(GameObject Pre)
	{
		PreBuilding = Pre;
		isBuildingMode = true;
		BuildNode.SetActive(true);
		BuildCanvas.SetActive(true);
		UI.SetActive(false);
		
		foreach(GameObject Pet in PMM.GetIslandPet())
		{
			Pet.GetComponent<BoxCollider2D>().enabled = false;
		}
		
		Doll DollObj = GameObject.FindObjectOfType<Doll>();
		DollObj.GetComponent<BoxCollider2D>().enabled = false;
		foreach(GameObject GO in PopCanvas)
		{
			GO.transform.parent = null;
			GO.SetActive(false);
		}

	}

	public void CloseEditingBuildingUI()
	{
		EditingBuilding.GetComponent<Building>().UI.SetActive(false);
	}


	public void DisBuild()
	{
		SetPositionError(false);
		Debug.Log("DIsBuild");
		DestroyPreBuilding();
		BuildingBlueprintToBuild = null;
		BuildingTobuild = null;
		if(EditingBuilding != null)
			EditingBuilding.GetComponent<Building>().UI.SetActive(false);
		EditingBuilding = null;
		PreBuilding = null;
		PreBuildingNow = null;
		UI.SetActive(true);
		ForGiveEditNow = false;
	//	ReverseEditBuildingButton.SetActive(false);
	//	ReversePreBuildingButton.SetActive(false);
		EditCanvas.SetActive(false);
		BuildCanvas.SetActive(false);
		BuildNode.SetActive(false);
		isBuildingMode = false;
		if(isEditMode)
		{
			isEditMode = false;		
			StoreAllStoreBuildingButtonNum();
		}
		
		foreach(GameObject Pet in PMM.GetIslandPet())
		{
			Pet.GetComponent<BoxCollider2D>().enabled = true;
		}
		Doll DollObj = GameObject.FindObjectOfType<Doll>();
		DollObj.GetComponent<BoxCollider2D>().enabled = true;

		foreach(GameObject GO in PopCanvas)
		{			
			if(GO.name == "CanoePopCanvas")
			{
				GameObject Canoe = GameObject.Find("Canoe(Clone)");
				GO.transform.position = Canoe.transform.position + new Vector3(0,5,0);
				GO.transform.parent = Canoe.transform;
			}
			else if(GO.name == "CavePopCanvas")
			{
				GameObject Cave = GameObject.Find("Cave(Clone)");
				GO.transform.position = Cave.transform.position + new Vector3(0,5,0);
				GO.transform.parent = Cave.transform;
			}
			else if(GO.name == "WarehousePopCanvas")
			{
				GameObject Warehouse = GameObject.Find("WareHouse(Clone)");
				GO.transform.position = Warehouse.transform.position + new Vector3(0,5,0);
				GO.transform.parent = Warehouse.transform;
			}
			GO.SetActive(true);
		}


		GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
		foreach(GameObject Building in Buildings)
		{
			if(Building.GetComponent<BoxCollider2D>() != null)
				Building.GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	public void SetEditingBuilding(GameObject Building)
	{
		EditingBuilding = Building;
	}

	public void DisEdit()
	{
		isEditMode = false;

		foreach(GameObject GO in PopCanvas)
		{
			if(GO.name == "CanoePopCanvas")
			{
				GameObject Canoe = GameObject.Find("Canoe(Clone)");
				GO.transform.position = Canoe.transform.position + new Vector3(0,5,0);
				GO.transform.parent = Canoe.transform;
			}
			else if(GO.name == "CavePopCanvas")
			{
				GameObject Cave = GameObject.Find("Cave(Clone)");
				GO.transform.position = Cave.transform.position + new Vector3(0,5,0);
				GO.transform.parent = Cave.transform;
			}
			else if(GO.name == "WarehousePopCanvas")
			{
				GameObject Warehouse = GameObject.Find("WareHouse(Clone)");
				GO.transform.position = Warehouse.transform.position + new Vector3(0,5,0);
				GO.transform.parent = Warehouse.transform;
			}
			GO.SetActive(true);
		}

		StoreAllStoreBuildingButtonNum();
	}


	public void AssignBuildPreBuilding(GameObject PreB)
	{
		PreBuildingNow = PreB;
		isBuildingMode = true;
	}



	public BuildingBlueprint GetBuilding()
	{
		return BuildingBlueprintToBuild;
	}

	public void DestroyPreBuilding()
	{
		Destroy(PreBuildingNow);
	}


	public GameObject GetPreBuilding()
	{
		return PreBuilding;
	}

	public GameObject GetPreBuildingNow()
	{
		return PreBuildingNow;
	}

	public void SetCaveOn()
	{
		CaveManager.SetActive(true);
	}

	public void SetRayCastPos(Vector3 pos)
	{
		
		Vector3 temp = pos;
		if(BuildingBlueprintToBuild.Building.tag == "Tree")
			temp.y -= 5;
		else
			temp.x += 5;
		RayCastPosition = temp;

	}

	public bool IfCanBuild()
	{
		Building BuildingScript = PreBuildingNow.GetComponent<Building>();
		foreach(GameObject Foundation in BuildingScript.Foundations)
		{
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(Foundation.transform.position,1);
			foreach (Collider2D collider in colliders2)
			{
				if (collider.tag == "BuildNode")
				{
					if(collider.gameObject.GetComponent<BuildNode>().Building != null)
					{
						Debug.Log("HAS!!!");
						Debug.Log(collider.name);
						return false;
					}
					if(PreBuilding.name == "Canoe" | PreBuilding.name == "PreCanoe")
					{
						if(!collider.gameObject.GetComponent<BuildNode>().CanBuildCanoe)
							return false;
					}
				}
			}
		}
		return true;
		
	}

	public void Build()
	{
		if(GoBack)
			return;
		BuildingTobuild = GetBuilding();

		if(!isEditMode)
		{
			if(GetBuilding() == null)
				return;
			

			Debug.Log("WWWWWWWWWWWWWWWWWWWWWWW");
			if(!IfCanBuild())
				return;
			if(Meterial.wood <  BuildingTobuild.WoodNum)
				return;
			if(Meterial.rock <  BuildingTobuild.RockNum)
				return;
			if(Meterial.metal <  BuildingTobuild.MetalNum)
				return;
			if(Meterial.cotton < BuildingTobuild.CottonNum)
				return;
			if(Meterial.earth < BuildingTobuild.EarthNum)
				return;
			if(Meterial.backbee < BuildingTobuild.Back)
				return;
			Debug.Log("QQQQQQQQQQQQQQQQQQQQQ");
		}

		if(PreBuildingNow == null)
			return;
		if(PositionError)
		{
			Debug.Log("PositionError!");
			return;
		}

		Debug.Log("!!!!!!!!!!!!!!!!!!!!!!");
		
		GameObject BuildingInfo = Instantiate(BuildingTobuild.Building,PreBuildingNow.transform.position,Quaternion.identity);
		Vector3 Btemp = BuildingInfo.transform.position;
		Btemp.z = 0;
		BuildingInfo.transform.position = Btemp;
		
		Building BuildingInfoScript = BuildingInfo.GetComponent<Building>();

		int flip = 0;
		if(PreBuildingNow.GetComponent<SpriteRenderer>().flipX)
		{
			BuildingInfo.GetComponent<SpriteRenderer>().flipX = true;
			flip = 1;
		}
		else
		{
			BuildingInfo.GetComponent<SpriteRenderer>().flipX = false;
			if(PreBuilding.name == "Canoe" | PreBuilding.name == "PreCanoe")
				BuildingInfoScript.Foundations[0].transform.position = BuildingInfoScript.CanoeNoFlipFoundationPosition.transform.position;
		}

		BuildingInfoScript.AssignBuildingManager(this);
		Debug.Log(BuildingInfo);

		List<BuildNode> BroNodes = new List<BuildNode>();
		foreach(GameObject Foundation in BuildingInfoScript.Foundations)
		{
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(Foundation.transform.position,1);
			foreach (Collider2D collider in colliders2)
			{
				if (collider.tag == "BuildNode")
				{
					BuildNode Node = collider.GetComponent<BuildNode>();
					BuildingInfoScript.SetNode(Node);
					BroNodes.Add(Node);
					Node.SetFlip(flip);
					Node.Building = BuildingInfo;
					Node.BuildingBlueprint = BuildingBlueprintToBuild;
					if(!isEditMode)
						RecordPositoin(Node,Node.BuildingBlueprint,BuildingInfo,flip);
				}
			}
		}
		for(int SelfNum = 0;SelfNum < BroNodes.Count;SelfNum++)
		{
			for(int BroNum = 0; BroNum < BroNodes.Count; BroNum++)
			{
				if(SelfNum != BroNum)
					BroNodes[SelfNum].SetBroNode(BroNodes[BroNum]);
			}
		}





		DestroyPreBuilding();
		if(BuildingInfo.tag == "Tree")
			DecorateIndex.TreeList.Add(1);

		


		if(!isEditMode)
		{
			if(NowPutDecoratingName == null)
			{
				Meterial.Reduce("Wood",BuildingTobuild.WoodNum);
				Meterial.Reduce("Rock",BuildingTobuild.RockNum);
				Meterial.Reduce("Metal",BuildingTobuild.MetalNum);
				Meterial.Reduce("Earth",BuildingTobuild.EarthNum);
				Meterial.Reduce("Cotton",BuildingTobuild.CottonNum);
				Meterial.Reduce("Back",BuildingTobuild.Back);
			}
			else
				UpdateStoredButton(1);
			
			BuildAudio.Play();

			if(BuildingInfo.tag == "Tree")
				SetBuildTreeName(BuildingInfo,flip);
			else
			{				
				SavePosition(BuildingInfo,flip);
				DisBuild();
			}

		}
		else
		{
			BuildingInfoScript.UI.SetActive(true);
			EditingBuilding = BuildingInfo;
			LastEditBuilding = BuildingInfo;
			EditResetPreBuilding();
			BuildEditBuildings.Add(BuildingInfo);
			if(BuildingInfo.tag == "Tree")
			{	
				BuildingBlueprint TreeBlueprint = BuildingTobuild;
				if(TreeBlueprint.Building == null)
					TreeBlueprint = EditingBuildingOriginBlueprint;
				BuildingInfo.GetComponent<Building>().SetTreeType(TreeBlueprint.Building.name);

				if(PutStoredBuilding)
				{
					BuildingInfo.name = "Tree" + StoredBuildingTempTreeNum.ToString();//false decide is write in Buildnode, when buildnode is touched	
					StoredBuildingTempTreeNum += 1;	
				}
				else
					BuildingInfo.name = LastTreeName;
			}
		}
	}

	public void UpdateStoredButton(int ReduceNum)
	{
		bool StartReset = false;
				GameObject RepeatButtonToDestory = new GameObject();
				for(int i=0;i<StoredBuildingButtons.Count;i++)
				{
					Debug.Log("i:"+i);
					StoredBuilding Button = StoredBuildingButtons[i];					
					if(StartReset)
					{
						Debug.Log("ResetPosition:"+Button);
						StoredBuildingButtons[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2 (170f,0);
						StoredBuildingButtons[i-1] = StoredBuildingButtons[i];
				//		if(i == StoredBuildingButtons.Count-1)
				//			StoredBuildingButtons.RemoveAt(i);
					}
					if(Button.TreeType == NowPutDecoratingName)
					{
						Button.NumberText.text = (int.Parse(Button.NumberText.text) - ReduceNum).ToString();

						int Num = int.Parse(Button.NumberText.text);
						reference.Child("users").Child(PlayerData.UserId.ToString()).Child("StoredBuilding").Child(Button.GetBuildingName()).SetValueAsync(Num);
						
						if(Num == 0)
						{
							RepeatButtonToDestory = Button.gameObject;
							Debug.Log("STARTRESET!!!!!!!!!!!!!!:::"+i);
							StartReset = true;
				//			StoredBuildingButtons.Remove(Button);

						}
						DM.UpdateButton(Button.TreeType,Num);


						NowPutDecoratingName = null;
					}

				}
				Debug.Log("NUM:"+StoredBuildingButtons.Count);
				LastStroedButtonPosition = StoredBuildingButtons[StoredBuildingButtons.Count-1].GetComponent<RectTransform>().anchoredPosition + new Vector2(170f,0);
				if(StartReset)
					StoredBuildingButtons.RemoveAt(StoredBuildingButtons.Count-1);
				Destroy(RepeatButtonToDestory);
				Debug.Log("NUM"+StoredBuildingButtons.Count);
	}

	async void SetBuildTreeName(GameObject BuildingInfo,int flip)
	{
		DataSnapshot snapshot = await reference.Child("users").Child(PlayerData.UserId.ToString()).GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
			}
			return task.Result;
		});
		BuildingInfo.name = "Tree"+snapshot.Child("Tree").ChildrenCount.ToString();
		BuildingInfo.GetComponent<Building>().GetNode().SetOriginTreeName(BuildingInfo.name);
		SavePosition(BuildingInfo,flip);
		DisBuild();
	}


	async void SavePosition(GameObject BuildingInfo,int flip)
	{
		Debug.Log("EnterSAVE");
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		float PosX = BuildingInfo.transform.position.x;
		float PosY = BuildingInfo.transform.position.y;
		float PosZ = BuildingInfo.transform.position.z;
		if(BuildingInfo.name == "Cave(Clone)")
		{
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("Unlock").SetValueAsync(1);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("PosZ").SetValueAsync(PosZ);
		}
		else if(BuildingInfo.name == "WareHouse(Clone)")
		{
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("WareHouse").Child("Unlock").SetValueAsync(1);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("WareHouse").Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("WareHouse").Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("WareHouse").Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("WareHouse").Child("PosZ").SetValueAsync(PosZ);
		}
		else if(BuildingInfo.name == "Canoe(Clone)")
		{
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Canoe").Child("Unlock").SetValueAsync(1);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Canoe").Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Canoe").Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Canoe").Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Canoe").Child("PosZ").SetValueAsync(PosZ);
		}
		else if(BuildingInfo.name == "FireCamp(Clone)" | BuildingInfo.name == "UpgradeFireCamp(Clone)")
		{
			IM.UnlockFireIllustration();
			Unlock.FireElement = true;
			if(BuildingInfo.name == "UpgradeFireCamp(Clone)")
			{	
				IM.UnlockLavaIllustration();
				Unlock.LavaElement = true;
			}
			BuildingMemory.FireCampUI.ChangeToUpgrade();
			if(BuildingInfo.name == "FireCamp(Clone)" )
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("Unlock").SetValueAsync(1);
			else
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("Unlock").SetValueAsync(2);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("PosZ").SetValueAsync(PosZ);
		}
		else if(BuildingInfo.name == "IceHouse(Clone)" | BuildingInfo.name == "UpgradeIceHouse(Clone)")
		{
			IM.UnlockIceIllustration();
			Unlock.IceElement = true;
			Debug.Log(BuildingInfo.name);
			if(BuildingInfo.name == "UpgradeIceHouse(Clone)")
			{
				IM.UnlockSnowIllustration();
				Unlock.FrozeElement = true;
			}
			BuildingMemory.IceHouseUI.ChangeToUpgrade();
			if(BuildingInfo.name == "IceHouse(Clone)")
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Unlock").SetValueAsync(1);
			else
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Unlock").SetValueAsync(2);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("PosZ").SetValueAsync(PosZ);
		}
		else if(BuildingInfo.name == "HolyChurch(Clone)" | BuildingInfo.name == "UpgradeHolyChurch(Clone)")
		{
			IM.UnlockHolyIllustration();
			Unlock.HolyElement = true;
			Debug.Log(BuildingInfo.name);
			if(BuildingInfo.name == "UpgradeHolyChurch(Clone)")
			{
				IM.UnlockMoneyIllustration();
				Debug.Log("UPGRADEDEDEDE");
				Unlock.MoneyElement = true;
			}
			BuildingMemory.HolyChurchUI.ChangeToUpgrade();
			if(BuildingInfo.name == "HolyChurch(Clone)")
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Unlock").SetValueAsync(1);
			else
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Unlock").SetValueAsync(2);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("PosZ").SetValueAsync(PosZ);
		}

		else if(BuildingInfo.tag == "Tree")
		{
			if(ForGiveEditNow)
				WaitForTreeAsync = true;

			BuildingBlueprint TreeBlueprint = new BuildingBlueprint();
			if(isBuildingMode)
				TreeBlueprint = BuildingBlueprintToBuild;
			else
				TreeBlueprint = BuildingTobuild;
			if(TreeBlueprint.Building == null)
				TreeBlueprint = EditingBuildingOriginBlueprint;
			
			BuildingInfo.GetComponent<Building>().SetTreeType(TreeBlueprint.Building.name);

			reference = FirebaseDatabase.DefaultInstance.RootReference;
			Debug.Log("AWAIT");
			int TreeNum = await reference.Child("users").Child(PlayerData.UserId.ToString()).GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					Debug.Log("error");
				}
				else if (task.IsCompleted) {
		//			reference = FirebaseDatabase.DefaultInstance.GetReference("users");
				}
				DataSnapshot snapshot = task.Result;
			Debug.Log((int)snapshot.Child("Tree").ChildrenCount);
				return (int)snapshot.Child("Tree").ChildrenCount;
			});
			
			Debug.Log("------------------------------");
			Debug.Log(isEditMode);
			Debug.Log(BuildingTobuild);
			Debug.Log("TreeNum = " + TreeNum);
/* 			if(!isEditMode)
			{
				BuildingInfo.name = "Tree"+snapshot.Child("Tree").ChildrenCount.ToString();
	//			BuildingInfo.name = reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Unlock").SetValueAsync(2);
			}
			else
			{
				if(PutStoredBuilding)
					BuildingInfo.name = "Tree"+snapshot.Child("Tree").ChildrenCount.ToString();//false decide is write in Buildnode, when buildnode is touched
				else
					BuildingInfo.name = LastTreeName;
			}*/
			
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("TreeNum").SetValueAsync(TreeNum+1);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(BuildingInfo.name).Child("Flip").SetValueAsync(flip);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(BuildingInfo.name).Child("PosX").SetValueAsync(PosX);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(BuildingInfo.name).Child("PosY").SetValueAsync(PosY);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(BuildingInfo.name).Child("PosZ").SetValueAsync(PosZ);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(BuildingInfo.name).Child("TreeType").SetValueAsync(BuildingInfo.GetComponent<Building>().TreeTypeToWrite);
			
			if(WaitForTreeAsync)
			{
				WaitForTreeAsync = false;
				DisBuild();
				Debug.Log("OVer");
			}
		}
	}

	public void ResetBoxCollider2d()
	{
		GameObject[] Buildings = GameObject.FindGameObjectsWithTag("Building");
		foreach(GameObject Building in Buildings)
		{
			Building.GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	public void ReversePreBuilding()
	{
		isFlip = !isFlip;
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		int flip = 0;
		if(!isEditMode)
		{
			PreBuildingNow.GetComponent<SpriteRenderer>().flipX = (PreBuildingNow.GetComponent<SpriteRenderer>().flipX == true) ? false : true;
			if(PreBuildingNow.GetComponent<SpriteRenderer>().flipX)
				flip = 1;
			if(PreBuildingNow.tag == "Tree")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(PreBuildingNow.name).Child("Flip").SetValueAsync(flip);
				else if(PreBuildingNow.name == "FireCamp(Clone)" | PreBuildingNow.name == "UpgradeFireCamp(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("Flip").SetValueAsync(flip);
				else if(PreBuildingNow.name == "IceHouse(Clone)" | PreBuildingNow.name == "UpgradeIceHouse(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Flip").SetValueAsync(flip);
				else if(PreBuildingNow.name == "HolyChurch(Clone)" | PreBuildingNow.name == "UpgradeHolyChurch(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Flip").SetValueAsync(flip);
				else if(PreBuildingNow.name == "Cave(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("Flip").SetValueAsync(flip);

		}
		else
		{
			Debug.Log("!!!!!");
			if(EditingBuilding != null)
			{		
				Debug.Log("LLLLLLLL");
				EditingBuilding.GetComponent<SpriteRenderer>().flipX = (EditingBuilding.GetComponent<SpriteRenderer>().flipX == true) ? false : true;
				if(EditingBuilding.GetComponent<SpriteRenderer>().flipX)
					flip = 1;
				if(EditingBuilding.tag == "Tree")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(EditingBuilding.name).Child("Flip").SetValueAsync(flip);
				else if(EditingBuilding.name == "FireCamp(Clone)" | EditingBuilding.name == "UpgradeFireCamp(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("Flip").SetValueAsync(flip);
				else if(EditingBuilding.name == "IceHouse(Clone)" | EditingBuilding.name == "UpgradeIceHouse(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Flip").SetValueAsync(flip);
				else if(EditingBuilding.name == "HolyChurch(Clone)" | EditingBuilding.name == "UpgradeHolyChurch(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Flip").SetValueAsync(flip);
				else if(EditingBuilding.name == "Cave(Clone)")
					reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Cave").Child("Flip").SetValueAsync(flip);

			}
		}

	
	}


	public void ResetBuildingOrder()
	{
		BuildingOrder[] Buildings = FindObjectsOfType<BuildingOrder>();
		foreach(BuildingOrder building in Buildings)
			building.GetComponent<BuildingOrder>().enabled = true;
	}

	public void UpgradeFireCamp()
	{
		BuildNode.SetActive(true);
				IM.UnlockLavaIllustration();
		GameObject FireCampClone = GameObject.Find("FireCamp(Clone)");
		int CloneFlip = 0;
		if(FireCampClone.GetComponent<SpriteRenderer>())
			CloneFlip = 1;
		Vector3 PosTemp = FireCampClone.transform.position;

		GameObject UpgradedFireCamp = Instantiate(FireCamp.UpgradeBuilding,PosTemp,Quaternion.identity);
		UpgradedFireCamp.GetComponent<Building>().AssignBuildingManager(this);
		Debug.Log(FireCampClone.transform.position);
		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(FireCampClone.transform.position, 4);
		foreach (Collider2D collider in colliders2)
		{
			Debug.Log(collider);
			if (collider.tag == "BuildNode")
			{
				collider.GetComponent<BuildNode>().SetOriginInfo(UpgradedFireCamp.transform.position,UFCBlueprint,CloneFlip);
				collider.GetComponent<BuildNode>().Building = UpgradedFireCamp;
				collider.GetComponent<BuildNode>().BuildingBlueprint = UFCBlueprint;
			}
		}
		Destroy(FireCampClone);

		Unlock.LavaElement = true;

		ReduceMaterial(FireCamp);
		BuildingMemory.FireCampUI.BuildingNotUpgraded = false;
		BuildingMemory.FireCampUI.ChangeToMax();
		Upgrading.Play();
		FireCampUpgradePanel.SetActive(true);
	//	OpenAnimation(FireCampNormal,FireCampUpgrade);
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("Unlock").SetValueAsync(2);
		BuildNode.SetActive(false);

	}
	public void UpgradeIceHouse()
	{
		BuildNode.SetActive(true);
		IM.UnlockSnowIllustration();
		GameObject IceHouseClone = GameObject.Find("IceHouse(Clone)");		
		int CloneFlip = 0;
		if(IceHouseClone.GetComponent<SpriteRenderer>())
			CloneFlip = 1;
		Vector3 PosTemp = IceHouseClone.transform.position;
		GameObject UpgradedIceHouse = Instantiate(IceHouse.UpgradeBuilding,PosTemp,Quaternion.identity);
		UpgradedIceHouse.GetComponent<Building>().AssignBuildingManager(this);
		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(IceHouseClone.transform.position, 4);
		foreach (Collider2D collider in colliders2)
		{
			if (collider.tag == "BuildNode")
			{
				collider.GetComponent<BuildNode>().SetOriginInfo(UpgradedIceHouse.transform.position,UIHBlueprint,CloneFlip);
				collider.GetComponent<BuildNode>().Building = UpgradedIceHouse;
				collider.GetComponent<BuildNode>().BuildingBlueprint = UIHBlueprint;
			}
		}

		Unlock.FrozeElement = true;

		Destroy(IceHouseClone);
		ReduceMaterial(IceHouse);
		BuildingMemory.IceHouseUI.BuildingNotUpgraded = false;
		BuildingMemory.IceHouseUI.ChangeToMax();
		Upgrading.Play();
		IceHouseUpgradePanel.SetActive(true);
//		OpenAnimation(IceHouseNormal,IceHouseUpgrade);
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Unlock").SetValueAsync(2);
		BuildNode.SetActive(false);
	}
	public void UpgradeHolyChurch()
	{
		BuildNode.SetActive(true);
		IM.UnlockMoneyIllustration();
		GameObject HolyChurchClone = GameObject.Find("HolyChurch(Clone)");
		int CloneFlip = 0;
		if(HolyChurchClone.GetComponent<SpriteRenderer>())
			CloneFlip = 1;
		Vector3 PosTemp = HolyChurchClone.transform.position;
		PosTemp.y -= 1.5f;
		PosTemp.x -= 1;
		GameObject UpgradedHolyChurch = Instantiate(HolyChurch.UpgradeBuilding,PosTemp,Quaternion.identity);
		UpgradedHolyChurch.GetComponent<Building>().AssignBuildingManager(this);

		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(HolyChurchClone.transform.position, 4);
		foreach (Collider2D collider in colliders2)
		{
			if (collider.tag == "BuildNode")
			{
				collider.GetComponent<BuildNode>().SetOriginInfo(UpgradedHolyChurch.transform.position,UHCBlueprint,CloneFlip);
				collider.GetComponent<BuildNode>().Building = UpgradedHolyChurch;
				collider.GetComponent<BuildNode>().BuildingBlueprint = UHCBlueprint;
			}
		}

		Unlock.MoneyElement = true;

		Destroy(HolyChurchClone);
		ReduceMaterial(HolyChurch);
		BuildingMemory.HolyChurchUI.BuildingNotUpgraded = false;
		BuildingMemory.HolyChurchUI.ChangeToMax();
		Upgrading.Play();
		HolyChurchUpgradePanel.SetActive(true);
//		OpenAnimation(HolyChurchNormal,HolyChurchUpgrade);
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		reference.Child("users").Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Unlock").SetValueAsync(2);
		BuildNode.SetActive(false);
	}

	private void OpenAnimation(Sprite Normal,Sprite Upgrade)
	{
		AnimationNormalSprite.gameObject.SetActive(true);
		AnimationUpgradeSprite.gameObject.SetActive(false);
		AnimationNormalSprite.sprite = Normal;
//		UpgradeAnimation.SetActive(true);
		StartCoroutine(WaitForUpgradeAnimation(Upgrade));

	}

	IEnumerator WaitForUpgradeAnimation(Sprite Upgrade)
	{
		yield return new WaitForSeconds(4.5f);
		Upgraded.Play();
		AnimationNormalSprite.gameObject.SetActive(false);
		AnimationUpgradeSprite.gameObject.SetActive(true);
		AnimationUpgradeSprite.sprite = Upgrade;
	}

	private void ReduceMaterial(BuildingBlueprint Blueprint)
	{
		Meterial.Reduce("Wood",Blueprint.U_WoodNum);
		Meterial.Reduce("Rock",Blueprint.U_RockNum);
		Meterial.Reduce("Metal",Blueprint.U_MetalNum);
		Meterial.Reduce("Earth",Blueprint.U_EarthNum);
		Meterial.Reduce("Cotton",Blueprint.U_CottonNum);
		Meterial.Reduce("Back",Blueprint.U_Back);
	}

	public void SetLastPreBuildingPos(Vector3 B)
	{
		LastPreBuildingPos = B;
	}

	public Vector3 GetLastPreBuildingPos()
	{
		return LastPreBuildingPos;
	}

	public async void Edit()
	{
		isEditMode = true;
		BuildNode.SetActive(true);
		StoredBuildingTempTreeNum = await reference.Child("users").Child(PlayerData.UserId.ToString()).GetValueAsync().ContinueWith(task => {
			return (int)task.Result.Child("Tree").ChildrenCount;
		});
		Debug.Log("EDIT");

		foreach(GameObject Pet in PMM.GetIslandPet())
		{
			Pet.GetComponent<BoxCollider2D>().enabled = false;
		}
		Doll DollObj = GameObject.FindObjectOfType<Doll>();
		DollObj.GetComponent<BoxCollider2D>().enabled = false;
		foreach(GameObject GO in PopCanvas)
		{
			GO.transform.parent = null;
			GO.SetActive(false);
		}


		EditCanvas.SetActive(true);
	}
	public void AssignEditBuilding(GameObject Prefab,BuildingBlueprint BuildingBlueprint)
	{
		PreBuilding = Prefab;
		BuildingBlueprintToBuild = BuildingBlueprint;
	}
	
	public void AssignEditBuildingNow(GameObject B)
	{
		PreBuildingNow = B;
	}

	public void EditResetPreBuilding()
	{
		PreBuilding = null;
		BuildingBlueprintToBuild = null;
	}

	public void RecordPositoin(BuildNode Node,BuildingBlueprint B,GameObject Building,int flip)
	{
		Debug.Log(Node);
		Node.SetOriginInfo(Building.transform.position,B,flip);
	}

	public void CheckPosition()
	{
		GameObject.FindObjectOfType<TouchControl>().enabled = true;
		if(PositionError)
		{
			Debug.Log("POsitionError");
			return;
		}
		if(PreBuilding != null && PreBuildingNow == null)
		{
			Debug.Log("NowBuildingNow");
			return;
		}
		if(isEditMode)
		{
			EditEndSetNewOriginInfo();
			DisBuild();
		}
		else
			Build();

		
	}

	public void SetPositionError(bool TF)
	{
		PositionError = TF;
	}

	public bool GetPositionError()
	{
		return PositionError;
	}

	public void AddEditingBuilding(GameObject B)
	{
		EditBuildings.Add(B);
	}

	public void ForGiveEdit()
	{
		foreach(GameObject B in BuildEditBuildings)
		{
			Debug.Log("DESTROY"+B);
			Destroy(B);//Bug
			Debug.Log(B);
		}
		foreach(StoredBuilding Button in StoredBuildingButtons)
		{
			Button.ResetNum();
		}
		foreach(GameObject StoredBuildingButton in StoredBuildingButtonsThisEdit)
		{
			if(StoredBuildingButton != null)
			{
				StoredBuilding StoredBScript = StoredBuildingButton.GetComponent<StoredBuilding>();
				string BuildingName = StoredBScript.GetBuildingName();
				int BuildingStoredNumInDB = int.Parse(StoredBScript.NumberText.text);
				reference.Child("users").Child(PlayerData.UserId.ToString()).Child("StoredBuilding").Child(BuildingName).SetValueAsync(BuildingStoredNumInDB);
				if(BuildingStoredNumInDB == 0)
				{
					StoredBuildingButtons.Remove(StoredBuildingButton.GetComponent<StoredBuilding>());
					Destroy(StoredBuildingButton);
				}
				else
					StoredBScript.NumberText.text = (BuildingStoredNumInDB).ToString();
			}
		}
		next();
	}

	public void DestroyEditBuildingList(BuildingBlueprint Blueprint)
	{
		foreach(GameObject GO in EditBuildings)
		{
			Destroy(GO);
		}

	}




	void next()
	{

		GameObject[] Nodes = GameObject.FindGameObjectsWithTag("BuildNode");
		ForGiveEditNow = true;
			Debug.Log("FORGIVEEDIT");
		foreach(GameObject N in Nodes)
		{
			BuildNode Node = N.GetComponent<BuildNode>();
			if(Node.GetOriginBlueprint() != null)
			{Debug.Log("1");
			//	if(Node.GetOriginBlueprint() != Node.BuildingBlueprint)
			//	{
					if(Node.GetOriginBlueprint().Building != null)
					{
						GameObject B = null;
						if(Node.Building == null)
						{
							Debug.Log(Node);
							Debug.Log(Node.GetOriginBlueprint());
							Debug.Log(Node.GetOriginBlueprint().Building);
							B = Instantiate(Node.GetOriginBlueprint().Building,Node.GetOriginPos(),Quaternion.identity);	
							BuildingTobuild = Node.GetOriginBlueprint();
							SavePosition(B,Node.GetFlip());
							Building BuildingScript = B.GetComponent<Building>();
							BuildingScript.SetNode(Node);
							if(B.tag == "Tree")
								B.name = Node.GetOriginTreeName();

							if(Node.GetFlip() == 1)
								B.GetComponent<SpriteRenderer>().flipX = true;
							else
							{
								if(B.name == "Canoe(Clone)")
									BuildingScript.Foundations[0].transform.position = BuildingScript.CanoeNoFlipFoundationPosition.transform.position;

								B.GetComponent<SpriteRenderer>().flipX = false;
							}
							BuildingScript.AssignBuildingManager(this);
							foreach(GameObject Foundation in BuildingScript.Foundations)
							{
								Collider2D[] colliders4 = Physics2D.OverlapCircleAll(Foundation.transform.position,1);
								foreach (Collider2D collider in colliders4)
								{
									if(collider.tag == "BuildNode")
									{
										collider.GetComponent<BuildNode>().Building = B;
										collider.GetComponent<BuildNode>().BuildingBlueprint = Node.GetOriginBlueprint();
									}
								}
							}
							
						}
	 				else
						{
							foreach(GameObject Bu in BuildEditBuildings)
							{
								if(Bu == Node.Building)
								{
									Debug.Log("GOTYA");
									B = Instantiate(Node.GetOriginBlueprint().Building,Node.GetOriginPos(),Quaternion.identity);
									BuildingTobuild = Node.GetOriginBlueprint();
									Debug.Log(BuildingTobuild.Building);
									Debug.Log(Node.GetOriginBlueprint().Building);		
									SavePosition(B,Node.GetFlip());
									Building BuildingScript = B.GetComponent<Building>();							
						//			if(B.name == "Canoe(Clone)")
									BuildingScript.SetNode(Node);	
									if(B.tag == "Tree")
										B.name = Node.GetOriginTreeName();

									if(Node.GetFlip() == 1)
										B.GetComponent<SpriteRenderer>().flipX = true;
									else
									{		
										if(B.name == "Canoe(Clone)")
											BuildingScript.Foundations[0].transform.position = BuildingScript.CanoeNoFlipFoundationPosition.transform.position;

										B.GetComponent<SpriteRenderer>().flipX = false;	
									}						
									BuildingScript.AssignBuildingManager(this);
									foreach(GameObject Foundation in BuildingScript.Foundations)
									{
										Collider2D[] colliders4 = Physics2D.OverlapCircleAll(Foundation.transform.position,1);
										foreach (Collider2D collider in colliders4)
										{
											if(collider.tag == "BuildNode")
											{
												collider.GetComponent<BuildNode>().Building = B;
												collider.GetComponent<BuildNode>().BuildingBlueprint = Node.GetOriginBlueprint();
											}
										}
									}
								}
							}
						}
					}
			//	}
			}
		}
		BuildEditBuildings.Clear();
		UI.SetActive(true);
		if(!WaitForTreeAsync)
			DisBuild();
		else
			Debug.Log("WAIT!!!!!!!!!!!!!!!");
	}

	public void EditEndSetNewOriginInfo()
	{
		BuildNode[] Nodes = GameObject.FindObjectsOfType<BuildNode>();
		Debug.Log("--------------------------------");
		foreach(BuildNode Node in Nodes)
		{
			if(Node.GetOriginBlueprint() != null)
			{
				if(Node.GetOriginBlueprint().Building == null)
				{
					if(Node.Building != null)
					{
						Debug.Log("GET U MOTHER FUCKER");
						Debug.Log(Node.Building);
						SavePosition(Node.Building,Node.GetFlip());
						RecordPositoin(Node,Node.BuildingBlueprint,Node.Building,Node.GetFlip());
						Node.SetOriginTreeName(Node.Building.name);
					}
				}
				if(Node.Building == null)
				{
					Node.SetOriginInfo(new Vector3(0,0,0),null,0);
				}
			}
			else
			{
				if(Node.Building != null)
				{
					Debug.Log(Node);
					Debug.Log(Node.Building);
					SavePosition(Node.Building,Node.GetFlip());
					RecordPositoin(Node,Node.BuildingBlueprint,Node.Building,Node.GetFlip());
					Node.SetOriginTreeName(Node.Building.name);
				}
			}
		}
		StoredBuildingButtonsThisEdit.Clear();
		SaveStoredButtonData();
		SaveStoredBuildingData();
		Debug.Log("--------------------------------");
		Debug.Log("End");
	}

	public void SetLastTreeName(string name)
	{
		LastTreeName = name;
	}

	public void EditingBuildingGoBack()
	{
		GameObject BuildingInfo = Instantiate(EditingBuildingOriginBlueprint.Building,EditingBuildingOriginPos,Quaternion.identity);
		Building BuildingInfoScript = BuildingInfo.GetComponent<Building>();

		int flip = 0;
		if(EditingBuilding.GetComponent<SpriteRenderer>().flipX)
		{
			BuildingInfo.GetComponent<SpriteRenderer>().flipX = true;
			flip = 1;
		}

		if(BuildingInfo.name == "Canoe(Clone)")
		{
			BuildingInfoScript.SetNode(EditBuildingOriginNode);
			Debug.Log("111111111111111111111111111111111111111111111111");
			Debug.Log(EditBuildingOriginNode);
			if(!EditBuildingOriginNode.Canoeflip)
			{
				BuildingInfoScript.Foundations[0].transform.position = BuildingInfoScript.CanoeNoFlipFoundationPosition.transform.position;
				BuildingInfo.GetComponent<SpriteRenderer>().flipX = false;
				flip = 0;
			}
			else
			{
				BuildingInfo.GetComponent<SpriteRenderer>().flipX = true;
				flip = 1;
			}
		}

		else if(BuildingInfo.tag == "Tree")
		{
			BuildingInfo.name = LastTreeName;
			BuildingInfo.GetComponent<Building>().SetTreeType(BuildingInfo.GetComponent<Building>().TreeTypeToWrite);
		}

		BuildingInfoScript.AssignBuildingManager(this);

		foreach(GameObject Foundation in BuildingInfoScript.Foundations)
		{
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(Foundation.transform.position,1);
			foreach (Collider2D collider in colliders2)
			{
				if (collider.tag == "BuildNode")
				{
					BuildNode Node = collider.GetComponent<BuildNode>();
					BuildingInfoScript.SetNode(Node);
					Node.SetFlip(flip);
					Node.Building = BuildingInfo;
					Node.BuildingBlueprint = EditingBuildingOriginBlueprint;
					if(!isEditMode)
					{
						RecordPositoin(Node,Node.BuildingBlueprint,BuildingInfo,flip);
						Node.SetOriginTreeName(BuildingInfo.name);
					}
				}
			}
		}

		

	//	SavePosition(BuildingInfo,flip);

		if(BuildingInfo.tag == "Tree")
			DecorateIndex.TreeList.Add(1);

		Destroy(EditingBuilding);

		BuildEditBuildings.Add(BuildingInfo);
		GoBack = false;
	}

	public void AssignEditBuildingOriginInfo(Vector3 OriginPos,BuildingBlueprint OriginBlueprint)
	{
		EditingBuildingOriginPos = OriginPos;
		EditingBuildingOriginBlueprint = OriginBlueprint;
	}

	public BuildingBlueprint GetEditingBuildingOriginPos()
	{
		return EditingBuildingOriginBlueprint;
	}

	public void AssignCanoeOriginNode (BuildNode Node)
	{
		EditBuildingOriginNode = Node;
	}

	public async void StoreBuilding(GameObject Obj,string TreeType,BuildingBlueprint Blueprint)
	{
		Debug.Log("enter");
		Debug.Log(PlayerData.UserId);
		Debug.Log(TreeType);
		int ChildNum = await FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerData.UserId.ToString()).Child("StoredBuilding").Child(TreeType).GetValueAsync().ContinueWith(task => {
			
				Debug.Log("=======================");
				if (task.IsFaulted) {
				Debug.Log("error");
			}

			if (task.IsCompleted) {
				if(task.Result.GetRawJsonValue() != null)
					return int.Parse(task.Result.GetRawJsonValue());
				else 
					return 0;
			}
			return -1;
		});
		Debug.Log("CHildNUM" + ChildNum);
		Debug.Log("Before Each");
		foreach(StoredBuilding SB in StoredBuildingButtons)
		{
			if(SB != null)
			{
				Debug.Log("Enter Decision");
				if(SB.GetBuildingName() == Obj.GetComponent<Building>().TreeTypeToWrite)
				{
					SB.gameObject.SetActive(true);
					SB.NumberText.text = (int.Parse(SB.NumberText.text) + 1).ToString();
					StoredBuildingName.Add(Obj.name);
					Destroy(Obj);
					return;
				}
			}
		}
		Debug.Log("in BM:"+Obj.name);
		StoredBuilding StoredObj = Instantiate(StoredBuildingPrefab,LastStoredPosition,Quaternion.identity);
		StoredObj.SetBM(this);
		StoredBuildingButtonsThisEdit.Add(StoredObj.gameObject);

		StoredBuildingName.Add(Obj.name);
		StoredObj.GetComponent<Transform>().parent = StoredList.transform;
		StoredObj.gameObject.transform.localScale = new Vector3(1,1,1);
//		StoredObj.gameObject.transform.localPosition  = LastStoredPosition;
		
		StoredObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-975 + (StoredBuildingButtons.Count-1)*170 ,-12);
		StoredBuildingButtons.Add(StoredObj);
		LastStoredPosition = StoredObj.GetComponent<RectTransform>().anchoredPosition;

		Vector3 temp = LastStoredPosition;
		temp.x += 170;
		LastStoredPosition = temp;
		StoredObj.AssignBuilding(EditingBuildingOriginBlueprint.Building);

		StoredObj.SetBlueprint(Blueprint);
		StoredObj.NumberText.text = (++ChildNum).ToString();
		StoredObj.BuildingImg.GetComponent<Image>().sprite = EditingBuildingOriginBlueprint.Building.GetComponent<SpriteRenderer>().sprite;
		

		EditingBuildingOriginBlueprint = null;
		EditingBuildingOriginPos = new Vector3(0,0,0);
		Destroy(Obj);
		Debug.Log(StoredObj.transform.localPosition);

	}

	public void StoreAllStoreBuildingButtonNum()
	{
		foreach(StoredBuilding Button in StoredBuildingButtons)
		{
			Button.SetNumThisEdit();
		}
	}

	public void SaveStoredButtonData()
	{
		foreach(StoredBuilding Button in StoredBuildingButtons.ToList())
		{
			int Num = int.Parse(Button.NumberText.text);
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("StoredBuilding").Child(Button.GetBuildingName()).SetValueAsync(Num);
			
			Debug.Log("111111111111111111111111111111111111");
			if(Num == 0)
			{
				StoredBuildingButtons.Remove(Button);
				Debug.Log(Button);
//				Debug.Log(StoredBuildingButtons[0]);
				Destroy(Button.gameObject);
//				Debug.Log(StoredBuildingButtons[0]);
			}
			DM.UpdateButton(Button.TreeType,Num);
		}
	}

	public void SetLastTouchedStoreButton(StoredBuilding SB)
	{
		LastTouchedStoreButton = SB;
	}

	public void ResetLastTouchedStoredButtonNum()
	{
		LastTouchedStoreButton.gameObject.SetActive(true);//if num == 0, it was set to false
		LastTouchedStoreButton.NumberText.text = (int.Parse(LastTouchedStoreButton.NumberText.text) + 1).ToString();
	}

	public void SaveStoredBuildingData()
	{
		foreach(string name in StoredBuildingName)
		{
			reference.Child("users").Child(PlayerData.UserId.ToString()).Child("Tree").Child(name).SetValueAsync("");
		}
		StoredBuildingName.Clear();
	}

	public void AddStoredBuildingButtons(StoredBuilding SB)
	{
		StoredBuildingButtons.Add(SB);
	}

	public List<StoredBuilding> GetStoredBuildingButtonsList()
	{
		return StoredBuildingButtons;
	}

	public void SetLastStoredPosition(Vector3 V)
	{
		LastStoredPosition = V;
	}

	public void SetPutStoredBuilding()
	{
		PutStoredBuilding = true;
	}

	public void ReleasePutStoredBuilding()
	{
		PutStoredBuilding = false;
	}

	public bool GetPutStoredBuilding()
	{
		return PutStoredBuilding;
	}

	public void ReSetLastTouchedButton()
	{
		LastTouchedStoreButton.NumberText.text = (int.Parse(LastTouchedStoreButton.NumberText.text) + 1).ToString();
		LastTouchedStoreButton.gameObject.SetActive(true);
	}

	public void AddContinuousCickButtonNum()
	{
		ContinuousCickButtonNum++;
	}

	public int GetContinuousCickButtonNum()
	{
		return ContinuousCickButtonNum;
	}

	public void ResetContinuousCickButtonNum()
	{
		ContinuousCickButtonNum = 0;
	}
}
