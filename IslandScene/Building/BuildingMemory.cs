using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
using System.Globalization;

public class BuildingMemory : MonoBehaviour {

	public BuildingManager buildingmanager;
	public GameObject Node;
	public GameObject Cave;
	public GameObject NormalTree;
	public GameObject MintTree;
	public GameObject PinkTree;
	public GameObject PuddingTree;
	public GameObject GrapeTree;
	public GameObject TrebleTree;
	public GameObject GreenTree;
	public GameObject RustTree;
	public GameObject RustForest;
	public GameObject Fir;
	public GameObject LittleFirForest;
	public GameObject FirForest;
	public GameObject DreamForest;
	public GameObject LittleForest;
	public GameObject ChristmasTree;
	private GameObject Tree;
	public GameObject CaveBuildingBlockCanvas;
	public GameObject FireBuildingBlockCanvas;
	public GameObject IceBuildingBlockCanvas;
	public GameObject HolyBuildingBlockCanvas;

	public BuildingUIhandler FireCampUI;
	public BuildingUIhandler IceHouseUI;
	public BuildingUIhandler HolyChurchUI;
	public BuildingBlueprint CaveBlueprint;
	public BuildingBlueprint CanoeBlueprint;
	public BuildingBlueprint WareHouseBlueprint;
	public BuildingBlueprint NormalTreeBlueprint;
	public BuildingBlueprint MintTreeBlueprint;
	public BuildingBlueprint PinkTreebBlueprint;
	public BuildingBlueprint PuddingTreeBlueprint;
	public BuildingBlueprint GrapeTreeBlueprint;
	public BuildingBlueprint TrebleTreeBlueprint;
	public BuildingBlueprint GreenTreeBlueprint;
	public BuildingBlueprint RustTreeBlueprint;
	public BuildingBlueprint RustForestBlueprint;
	public BuildingBlueprint FirBlueprint;
	public BuildingBlueprint LittleFirForestBlueprint;
	public BuildingBlueprint FirForestBlueprint;
	public BuildingBlueprint DreamForestBlueprint;
	public BuildingBlueprint LittleForestBlueprint;
	public BuildingBlueprint ChristmasTreeBlueprint;

	[Header("StroedBuilding")]
	public GameObject Parent;
	public GameObject StoredBuildingButtonPrefab;
	private Vector3 LastButtonPosition;
	private bool CanInsStoredBuildingButton;
	private bool StoredBuildingButtonInsed;
	private List<string> StroedBuildingName = new List<string>();
	private List<int> StoredBuildingNum = new List<int>();

	private DataSnapshot snapshot;
	private bool FireCampLevel1;
	private bool FireCampLevel2;
	private GameObject FireCampToInstantiate;
	private Vector3 Fposition;
	private bool FFlip;
	private bool FireCampInsed;
	
	private bool IceHouseLevel1;
	private bool IceHouseLevel2;
	private GameObject IceHouseToInstantiate;
	private Vector3 Iposition;
	private bool IFlip;
	private bool IceHouseInsed;
	
	private bool HolyChurchLevel1;
	private bool HolyChurchLevel2;
	private GameObject HolyChurchToInstantiate;
	private Vector3 Hposition;
	private bool HFlip;
	private bool HolyChurchInsed;

	private Vector3 Cposition;
	private bool CaveUnlock;
	private bool CFlip;
	private bool CaveInsed;

	private Vector3 WareHouseposition;
	private bool WareHouseUnlock;
	private bool WareHouseFlip;
	private bool WareHouseInsed;

	private Vector3 Canoeposition;
	private bool CanoeUnlock;
	private bool CanoeFlip;
	private bool CanoeInsed;

	private GameObject TreeToIns;
	private bool HasTree;
	private bool TreeInsed;
	private BuildingBlueprint TreeBlueprint;

	private	List<string> TreeType = new List<string>();
	private	List<string> TreeName = new List<string>();
	private	List<string> TreeFlip = new List<string>();
	private	List<float> TreePosX = new List<float>();
	private	List<float> TreePosY = new List<float>();
	private	List<float> TreePosZ = new List<float>();
	
	public Canvas CavePop;
	public Canvas CanoePop;
	public Canvas WareHousePop;
	public Camera MainCamera;
	private int isFinishEgg;

private bool isReadyToUpdate;

private BuildingData buildingData;
	public TutorIsland Tutor;
	private int isFinishTutor;
	public IllustrationManager IM;
	public PetMoveManager PMM;
	public DecoratingManager DM;

 	void BuildMemory()
	{
			if(!isReadyToUpdate)
				return;

/* 			if(buildingData.CanInsFireCamp == 1)
			{
				FireCampUI.BuildingCanvas.SetActive(false);
				FireCampUI.BuildingUpgradedCanvas.SetActive(true);
				FireIllustrationBlock.SetActive(false);
				if(buildingData.FireCampUnlock == 1)
					FireCampToInstantiate = FireCampUI.BuildingPrefab;
				else
				{
					FireCampUI.BuildingBlock.SetActive(true);
					FireCampToInstantiate = FireCampUI.BuildingUpgradedPrefab;
				}
				Vector3 Fposition = new Vector3(buildingData.FirecampPosX,buildingData.FirecampPosY,buildingData.FirecampPosZ);
				GameObject B = Instantiate(FireCampToInstantiate,Fposition,Quaternion.identity);
				if(buildingData.FireCampFlip == 1)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,Fposition,FireCampUI.Blueprint);
				FireCampInsed = true;
			}
			buildingData.CanInsFireCamp = 0;*/

			
 			if(FireCampLevel1 & !FireCampInsed)
			{
				IM.UnlockFireIllustration();
				FireCampUI.BuildingCanvas.SetActive(false);
				FireCampUI.BuildingUpgradedCanvas.SetActive(true);
				GameObject B = Instantiate(FireCampToInstantiate,Fposition,Quaternion.identity);
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(FFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,FireCampUI.Blueprint);
				FireCampInsed = true;
			}
			else if(FireCampLevel2 & !FireCampInsed)
			{
				IM.UnlockLavaIllustration();
				FireCampUI.BuildingCanvas.SetActive(false);
				FireCampUI.BuildingUpgradedCanvas.SetActive(false);
				FireCampUI.BuildingBlock.SetActive(true);
				GameObject B = Instantiate(FireCampToInstantiate,Fposition,Quaternion.identity);
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(FFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,FireCampUI.UpBlueprint);
				FireCampInsed = true;
			}
 			if(IceHouseLevel1 & !IceHouseInsed)
			{
				IM.UnlockIceIllustration();
				IceHouseUI.BuildingCanvas.SetActive(false);
				IceHouseUI.BuildingUpgradedCanvas.SetActive(true);
				GameObject B = Instantiate(IceHouseToInstantiate,Iposition,Quaternion.identity);
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(IFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,IceHouseUI.Blueprint);
				IceHouseInsed = true;
			}
			else if(IceHouseLevel2 & !IceHouseInsed)
			{
				IM.UnlockSnowIllustration();
				IceHouseUI.BuildingCanvas.SetActive(false);
				IceHouseUI.BuildingUpgradedCanvas.SetActive(false);
				IceHouseUI.BuildingBlock.SetActive(true);
				GameObject B = Instantiate(IceHouseToInstantiate,Iposition,Quaternion.identity);
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(IFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,IceHouseUI.UpBlueprint);
				IceHouseInsed = true;
			}
			if(HolyChurchLevel1 & !HolyChurchInsed)
			{
				IM.UnlockHolyIllustration();
				HolyChurchUI.BuildingCanvas.SetActive(false);
				HolyChurchUI.BuildingUpgradedCanvas.SetActive(true);
				GameObject B = Instantiate(HolyChurchToInstantiate,Hposition,Quaternion.identity);
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(HFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,HolyChurchUI.Blueprint);
				HolyChurchInsed = true;
			}
			else if(HolyChurchLevel2 & !HolyChurchInsed)
			{
				IM.UnlockMoneyIllustration();
				HolyChurchUI.BuildingCanvas.SetActive(false);
				HolyChurchUI.BuildingUpgradedCanvas.SetActive(false);
				HolyChurchUI.BuildingBlock.SetActive(true);
				GameObject B = Instantiate(HolyChurchToInstantiate,Hposition,Quaternion.identity);
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(IFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,HolyChurchUI.UpBlueprint);
				HolyChurchInsed = true;
			}
			if(CaveUnlock & !CaveInsed)
			{
				GameObject B = Instantiate(Cave,Cposition,Quaternion.identity);
				CavePop.transform.position = B.transform.position + new Vector3(0,5,0);
				CavePop.transform.parent = B.transform;
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(CFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,CaveBlueprint);
				CaveInsed = true;
			}
			if(WareHouseUnlock & !WareHouseInsed)
			{
				GameObject B = Instantiate(WareHouseBlueprint.Building,WareHouseposition,Quaternion.identity);
				WareHousePop.transform.position = B.transform.position + new Vector3(0,5,0);
				WareHousePop.transform.parent = B.transform;
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(WareHouseFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				SetBuildNodeInfo(B,WareHouseBlueprint);
				WareHouseInsed = true;
			}
			if(CanoeUnlock & !CanoeInsed)
			{
				GameObject B = Instantiate(CanoeBlueprint.Building,Canoeposition,Quaternion.identity);
				CanoePop.transform.position = B.transform.position + new Vector3(0,5,0);
				CanoePop.transform.parent = B.transform;
				B.GetComponent<Building>().AssignBuildingManager(buildingmanager);
				if(CanoeFlip)
					B.GetComponent<SpriteRenderer>().flipX = true;
				else
					B.GetComponent<SpriteRenderer>().flipX = false;
				SetBuildNodeInfo(B,CanoeBlueprint);
				CanoeInsed = true;
			}
			if(HasTree & !TreeInsed)
			{
				for(int i=0;i<TreeType.Count;i++)
				{
					if(TreeType[i] == "\"NormalTree\"")
					{
						TreeToIns = NormalTree;
						TreeBlueprint = NormalTreeBlueprint;
					}
					else if(TreeType[i] == "\"MintTree\"")
					{
						TreeToIns = MintTree;
						TreeBlueprint = MintTreeBlueprint;
					}
					else if(TreeType[i] == "\"PinkTree\"")
					{
						TreeToIns = PinkTree;
						TreeBlueprint = PinkTreebBlueprint;
					}
					else if(TreeType[i] == "\"GrapeTree\"")
					{
						TreeToIns = GrapeTree;
						TreeBlueprint = GrapeTreeBlueprint;
					}
					else if(TreeType[i] == "\"PuddingTree\"")
					{
						TreeToIns = PuddingTree;
						TreeBlueprint = PuddingTreeBlueprint;
					}
					else if(TreeType[i] == "\"TrebleTree\"")
					{
						TreeToIns = TrebleTree;
						TreeBlueprint = TrebleTreeBlueprint;
					}
					else if(TreeType[i] == "\"GreenTree\"")
					{
						TreeToIns = GreenTree;
						TreeBlueprint = GreenTreeBlueprint;
					}
					else if(TreeType[i] == "\"RustTree\"")
					{
						TreeToIns = RustTree;
						TreeBlueprint = RustTreeBlueprint;
					}
					else if(TreeType[i] == "\"RustForest\"")
					{
						TreeToIns = RustForest;
						TreeBlueprint = RustForestBlueprint;
					}
					else if(TreeType[i] == "\"Fir\"")
					{
						TreeToIns = Fir;
						TreeBlueprint = FirBlueprint;
					}
					else if(TreeType[i] == "\"LittleFirForest\"")
					{
						TreeToIns = LittleFirForest;
						TreeBlueprint = LittleFirForestBlueprint;
					}
					else if(TreeType[i] == "\"FirForest\"")
					{
						TreeToIns = FirForest;
						TreeBlueprint = FirForestBlueprint;
					}
					else if(TreeType[i] == "\"DreamForest\"")
					{
						TreeToIns = DreamForest;
						TreeBlueprint = DreamForestBlueprint;
					}
					else if(TreeType[i] == "\"LittleForest\"")
					{
						TreeToIns = LittleForest;
						TreeBlueprint = LittleForestBlueprint;
					}
					else if(TreeType[i] == "\"ChristmasTree\"")
					{
						TreeToIns = ChristmasTree;
						TreeBlueprint = ChristmasTreeBlueprint;
					}
				
					Vector3 TreePos = new Vector3(TreePosX[i],TreePosY[i],TreePosZ[i]);
					GameObject B = Instantiate(TreeToIns,TreePos,Quaternion.identity);
					Building BScript = B.GetComponent<Building>();
					BScript.AssignBuildingManager(buildingmanager);
					BScript.SetTreeType(TreeBlueprint.Building.name);
					if(TreeFlip[i] == "1")
						B.GetComponent<SpriteRenderer>().flipX = true;
					B.name = TreeName[i];
					Vector3 RayCastPos = TreePos;
					RayCastPos.y -= 5;
					SetBuildNodeInfo(B,TreeBlueprint);
					BScript.GetNode().SetOriginTreeName(TreeName[i]);
				}
				TreeInsed = true;
			}
			if(CanInsStoredBuildingButton & !StoredBuildingButtonInsed)
			{
				int SBuildingNum = 0;
				foreach(string name in StroedBuildingName)
				{
					GameObject Button = Instantiate(StoredBuildingButtonPrefab,LastButtonPosition,Quaternion.identity);
					Button.transform.parent = Parent.transform;
					Button.transform.localScale = new Vector3(1,1,1);



					Button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-975 + (buildingmanager.GetStoredBuildingButtonsList().Count-1)*170 ,-12);
					
					buildingmanager.SetLastStoredPosition(LastButtonPosition);
				
					LastButtonPosition = Button.GetComponent<RectTransform>().anchoredPosition;


		//		Button.transform.localPosition = LastButtonPosition;
					Vector3 temp = LastButtonPosition;
					temp.x += 170;
					LastButtonPosition = temp;
					StoredBuilding SB = Button.GetComponent<StoredBuilding>();
					DM.UpdateButton(name,StoredBuildingNum[SBuildingNum]);
					SB.NumberText.text = StoredBuildingNum[SBuildingNum++].ToString();
					SB.SetBM(buildingmanager);
					buildingmanager.AddStoredBuildingButtons(SB);
					SB.SetNumThisEdit();


					if(name == "NormalTree")
					{
						SB.BuildingImg.sprite = NormalTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(NormalTree);
						SB.SetBlueprint(buildingmanager.NormalTree);
					}
					else if(name == "MintTree")
					{
						SB.BuildingImg.sprite = MintTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(MintTree);
						SB.SetBlueprint(buildingmanager.MintTree);
					}
					else if(name == "PinkTree")
					{
						SB.BuildingImg.sprite = PinkTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(PinkTree);
						SB.SetBlueprint(buildingmanager.PinkTree);
					}
					else if(name == "GrapeTree")
					{
						SB.BuildingImg.sprite = GrapeTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(GrapeTree);
						SB.SetBlueprint(buildingmanager.GrapeTree);
					}
					else if(name == "PuddingTree")
					{
						SB.BuildingImg.sprite = PuddingTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(PuddingTree);
						SB.SetBlueprint(buildingmanager.PuddingTree);
					}
					else if(name == "TrebleTree")
					{
						SB.BuildingImg.sprite = TrebleTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(TrebleTree);
						SB.SetBlueprint(buildingmanager.TrebleTree);
					}
					else if(name == "GreenTree")
					{
						SB.BuildingImg.sprite = GreenTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(GreenTree);
						SB.SetBlueprint(buildingmanager.GreenTree);
					}
					else if(name == "RustTree")
					{
						SB.BuildingImg.sprite = RustTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(RustTree);
						SB.SetBlueprint(buildingmanager.DeadTree);
					}
					else if(name == "RustForest")
					{
						SB.BuildingImg.sprite = RustForest.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(RustForest);
						SB.SetBlueprint(buildingmanager.DeadTreeFroest);
					}
					else if(name == "Fir")
					{
						SB.BuildingImg.sprite = Fir.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(Fir);
						SB.SetBlueprint(buildingmanager.Fir);
					}
					else if(name == "LittleFirForest")
					{
						SB.BuildingImg.sprite = LittleFirForest.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(LittleFirForest);
						SB.SetBlueprint(buildingmanager.LittleFir);
					}
					else if(name == "FirForest")
					{
						SB.BuildingImg.sprite = FirForest.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(FirForest);
						SB.SetBlueprint(buildingmanager.FirFroest);
					}
					else if(name == "DreamForest")
					{
						SB.BuildingImg.sprite = DreamForest.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(DreamForest);
						SB.SetBlueprint(buildingmanager.DreamFroest);
					}
					else if(name == "LittleForest")
					{
						SB.BuildingImg.sprite = LittleForest.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(LittleForest);
						SB.SetBlueprint(buildingmanager.LittleFroest);
					}
					else if(name == "ChristmasTree")
					{
						SB.BuildingImg.sprite = ChristmasTree.GetComponent<SpriteRenderer>().sprite;
						SB.AssignBuilding(ChristmasTree);
						SB.SetBlueprint(buildingmanager.ChristmasTree);
					}
					
					SB.SetTreeType();
				}
				StoredBuildingButtonInsed = true;
				
			}
				
			if(isFinishTutor == 0)
			{

				Tutor.gameObject.SetActive(true);
				if(isFinishEgg == 1)
					Tutor.EggStart();
				else
					Tutor.GoTutor();
				isFinishTutor = 2;
			}
			isReadyToUpdate = false;
			CancelInvoke();
			Debug.Log("Start BM");
			PMM.StartAttatchDB();
	}

	void SetBuildNodeInfo(GameObject B,BuildingBlueprint Bluepirnt)
	{
		Building BuildScript = B.GetComponent<Building>();
		int flip = 0;
		if(B.GetComponent<SpriteRenderer>().flipX)
			flip = 1;
		else if(B.name == "Canoe(Clone)")	
			BuildScript.Foundations[0].transform.position = BuildScript.CanoeNoFlipFoundationPosition.transform.position;


		Node.SetActive(true);
		foreach(GameObject Foundation in BuildScript.Foundations)
		{
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(Foundation.transform.position, 1);
			
			foreach (Collider2D collider in colliders2)
			{
				if (collider.tag == "BuildNode")
				{
					BuildNode Node = collider.GetComponent<BuildNode>();
					BuildScript.SetNode(Node);

					Node.Building = B;
					Node.BuildingBlueprint = Bluepirnt;
					Node.SetOriginInfo(B.transform.position,Bluepirnt,flip);
				}
			}
		}
		
		Node.SetActive(false);
	}


	class BuildingData
	{
		public int CanInsFireCamp;
		public float FirecampPosX;
		public float FirecampPosY;
		public float FirecampPosZ;
		public int FireCampFlip;
		public int FireCampUnlock;
	}


	async void FB()
	{
		Debug.Log("start Coroutine BM");
					CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
					ci.NumberFormat.CurrencyDecimalSeparator = ".";

		int Complete = await FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).GetValueAsync().ContinueWith(task => {

			Debug.Log("start BM DB");
			if (task.IsFaulted) {
				Debug.Log("error");
			}

			BuildingData localBuildingData  = new BuildingData();
			if (task.IsCompleted) {
				snapshot = task.Result;
				if((string)snapshot.Child("FireCamp").Child("Unlock").GetRawJsonValue() != "0")
				{
	//				localBuildingData.CanInsFireCamp = 1;
					Fposition.x = float.Parse((string)snapshot.Child("FireCamp").Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci);
					Fposition.y = float.Parse((string)snapshot.Child("FireCamp").Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci);
					Fposition.z = float.Parse(snapshot.Child("FireCamp").Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci);
	//				localBuildingData.FirecampPosX = float.Parse(snapshot.Child(PlayerData.UserId + "/FireCamp/PosX").GetRawJsonValue());
	//				localBuildingData.FirecampPosY = float.Parse(snapshot.Child(PlayerData.UserId + "/FireCamp/PosY").GetRawJsonValue());
	//				localBuildingData.FirecampPosZ = float.Parse(snapshot.Child(PlayerData.UserId + "/FireCamp/PosZ").GetRawJsonValue());
	//				localBuildingData.FireCampFlip = int.Parse(snapshot.Child(PlayerData.UserId + "/FireCamp/Flip").GetRawJsonValue());
	//				localBuildingData.FireCampUnlock = int.Parse(snapshot.Child(PlayerData.UserId + "/FireCamp/Flip").GetRawJsonValue());
					if(snapshot.Child("FireCamp").Child("Flip").GetRawJsonValue() == "1")
							FFlip = true;

 					if(snapshot.Child("FireCamp").Child("Unlock").GetRawJsonValue() == "1")
					{
						FireCampToInstantiate = FireCampUI.BuildingPrefab;
						Unlock.FireElement = true;
						FireCampLevel1 = true;
					}
					else if(snapshot.Child("FireCamp").Child("Unlock").GetRawJsonValue() == "2")
					{
						FireCampToInstantiate = FireCampUI.BuildingUpgradedPrefab;
						Unlock.FireElement = true;
						Unlock.LavaElement = true;
						FireCampLevel2 = true;
					}
				}
				
 				if(snapshot.Child("IceHouse").Child("Unlock").GetRawJsonValue() != "0")
				{
					Iposition.x = float.Parse(snapshot.Child("IceHouse").Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci);
					Iposition.y = float.Parse(snapshot.Child("IceHouse").Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci);
					Iposition.z = float.Parse(snapshot.Child("IceHouse").Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci);

					if(snapshot.Child("IceHouse").Child("Flip").GetRawJsonValue() == "1")
							IFlip = true;
					
					if(snapshot.Child("IceHouse").Child("Unlock").GetRawJsonValue() == "1")
					{
						IceHouseToInstantiate = IceHouseUI.BuildingPrefab;
						Unlock.IceElement = true;
						IceHouseLevel1 = true;
					}
					else if(snapshot.Child("IceHouse").Child("Unlock").GetRawJsonValue() == "2")
					{
						IceHouseToInstantiate = IceHouseUI.BuildingUpgradedPrefab;
						Unlock.IceElement = true;
						Unlock.FrozeElement = true;
						IceHouseLevel2 = true;
					}
				}
				if((string)snapshot.Child("HolyChurch").Child("Unlock").GetRawJsonValue() != "0")
				{
					Hposition.x = float.Parse(snapshot.Child("HolyChurch").Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci);
					Hposition.y = float.Parse(snapshot.Child("HolyChurch").Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci);
					Hposition.z = float.Parse(snapshot.Child("HolyChurch").Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci);

					if(snapshot.Child("HolyChurch").Child("Flip").GetRawJsonValue() == "1")
							HFlip = true;
					
					Debug.Log(snapshot.Child("HolyChurch").Child("Unlock").GetRawJsonValue());
					if(snapshot.Child("HolyChurch").Child("Unlock").GetRawJsonValue() == "1")
					{
						Debug.Log("Level1");
						HolyChurchToInstantiate = HolyChurchUI.BuildingPrefab;
						Unlock.HolyElement = true;
						HolyChurchLevel1 = true;
					}
					else if(snapshot.Child("HolyChurch").Child("Unlock").GetRawJsonValue() == "2")
					{
						HolyChurchToInstantiate = HolyChurchUI.BuildingUpgradedPrefab;
						Unlock.HolyElement = true;
						Unlock.MoneyElement = true;
						HolyChurchLevel2 = true;
					}
				}
				if((string)snapshot.Child("Cave").Child("Unlock").GetRawJsonValue() != "0")
				{
					CaveUnlock = true;
					if((string)snapshot.Child("Cave").Child("Flip").GetRawJsonValue() == "1")
						CFlip = true;
					Cposition.x = float.Parse(snapshot.Child("Cave").Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci);
					Cposition.y = float.Parse(snapshot.Child("Cave").Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci);
					Cposition.z = float.Parse(snapshot.Child("Cave").Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci);
				}
				if((string)snapshot.Child("WareHouse").Child("Unlock").GetRawJsonValue() != "0")
				{
					WareHouseUnlock = true;
					if((string)snapshot.Child("WareHouse").Child("Flip").GetRawJsonValue() == "1")
						WareHouseFlip = true;
					WareHouseposition.x = float.Parse(snapshot.Child("WareHouse").Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci);
					WareHouseposition.y = float.Parse(snapshot.Child("WareHouse").Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci);
					WareHouseposition.z = float.Parse(snapshot.Child("WareHouse").Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci);
				}
				if((string)snapshot.Child("Canoe").Child("Unlock").GetRawJsonValue() != "0")
				{
					CanoeUnlock = true;
					CanoeFlip = true;
					if(snapshot.Child("Canoe").Child("Flip").GetRawJsonValue() == "0")
					{
						CanoeFlip = false;
					}
					Canoeposition.x = float.Parse(snapshot.Child("Canoe").Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci);
					Canoeposition.y = float.Parse(snapshot.Child("Canoe").Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci);
					Canoeposition.z = float.Parse(snapshot.Child("Canoe").Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci);
				}
				Debug.Log(snapshot.Child("Tree").ChildrenCount);
				if(snapshot.Child("Tree").ChildrenCount != 0)
				{	
					for(int i=0; i<snapshot.Child("Tree").ChildrenCount;i++)
					{
						HasTree = true;
						if(snapshot.Child("Tree").Child("Tree"+i).Child("TreeType").Value != null)
						{
							TreeType.Add(snapshot.Child("Tree").Child("Tree"+i).Child("TreeType").GetRawJsonValue());
							TreeName.Add(snapshot.Child("Tree").Child("Tree"+i).Key);
							TreeFlip.Add(snapshot.Child("Tree").Child("Tree"+i).Child("Flip").GetRawJsonValue());
							TreePosX.Add(float.Parse(snapshot.Child("Tree").Child("Tree"+i).Child("PosX").GetRawJsonValue(),NumberStyles.Any,ci));
							TreePosY.Add(float.Parse(snapshot.Child("Tree").Child("Tree"+i).Child("PosY").GetRawJsonValue(),NumberStyles.Any,ci));
							TreePosZ.Add(float.Parse(snapshot.Child("Tree").Child("Tree"+i).Child("PosZ").GetRawJsonValue(),NumberStyles.Any,ci));
						}
						else
							Debug.Log("----------============");
					}
				}		
				foreach(DataSnapshot StoredBuildingsnapshot in snapshot.Child("StoredBuilding").Children)
				{
					if(int.Parse(StoredBuildingsnapshot.GetRawJsonValue()) != 0)
					{
						CanInsStoredBuildingButton = true;
						StroedBuildingName.Add((string)StoredBuildingsnapshot.Key);
						StoredBuildingNum.Add(int.Parse(StoredBuildingsnapshot.GetRawJsonValue()));
					}
				}
				isFinishTutor = int.Parse((string)snapshot.Child("FinishTutor").GetRawJsonValue());
				isFinishEgg = int.Parse((string)snapshot.Child("FinishEgg").GetRawJsonValue());
			}//Complete

			Debug.Log("End DB BM");
			return 0;
		},TaskScheduler.FromCurrentSynchronizationContext());

		isReadyToUpdate = true;

	}

	public void StartAttatchDB()
	{
		Node = GameObject.Find("BuildingManager").GetComponent<BuildingManager>().GetNodes();
		FB();
	}

	async void Start () {
		Debug.Log(PlayerData.UserId);
//		BuildingManager buildingmanager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
		buildingmanager.SetBM(this);
//		Parent = buildingmanager.StoredList;
//		Node = GameObject.Find("UISpawn").GetComponent<SpawnUI>().BuildNode;

		LastButtonPosition = new Vector3 (-975,88,0);
		InvokeRepeating("BuildMemory",.1f,.1f);
//		PlayerData.UserId = 102;
		
// 		var mainThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
//		, mainThreadScheduler);

		

/* 		Prop.BombNum = PlayerPrefs.GetInt("BombNum",0);
		Prop.CakeNum = PlayerPrefs.GetInt("CakeNum",0);
		
		if(PlayerPrefs.GetInt("FireCamp",0) == 1)
		{
			Debug.Log("enter!!");
			
		}
		if(PlayerPrefs.GetInt("IceHouse",0) == 1)
		{
			GameObject IceHouseToInstantiate = IceHouseUI.BuildingPrefab;

			IceHouseUI.BuildingCanvas.SetActive(false);
			IceHouseUI.BuildingUpgradedCanvas.SetActive(true);
			if(!IceHouseUI.BuildingNotUpgraded)
			{
				Debug.Log("BLock");
				IceHouseToInstantiate = IceHouseUI.BuildingUpgradedPrefab;
				IceHouseUI.BuildingBlock.SetActive(true);
			}

			Unlock.IceElement = true;
			IceIllustrationBlock.SetActive(false);
			float IPosx = PlayerPrefs.GetFloat("IceHousePosX",0);
			float IPosy = PlayerPrefs.GetFloat("IceHousePosY",0);
			float IPosz = PlayerPrefs.GetFloat("IceHousePosZ",0);
			int flipx = PlayerPrefs.GetInt("IceHouseFlip",0);
			Vector3 Iposition;
			Iposition.x = IPosx;
			Iposition.y = IPosy;
			Iposition.z = IPosz;

			GameObject B = Instantiate(IceHouseToInstantiate,Iposition,Quaternion.identity);
			if(flipx == 1)
				B.GetComponent<SpriteRenderer>().flipX = true;
		}
		if(PlayerPrefs.GetInt("HolyChurch",0) == 1)
		{
			GameObject HolyChurchToInstantiate = HolyChurchUI.BuildingPrefab;

			HolyChurchUI.BuildingCanvas.SetActive(false);
			HolyChurchUI.BuildingUpgradedCanvas.SetActive(true);
			if(!HolyChurchUI.BuildingNotUpgraded)
			{
				HolyChurchToInstantiate = HolyChurchUI.BuildingUpgradedPrefab;
				HolyChurchUI.BuildingBlock.SetActive(true);
			}


			Unlock.HolyElement = true;
			HolyIllustrationBlock.SetActive(false);
			float HPosx = PlayerPrefs.GetFloat("HolyChurchPosX",0);
			float HPosy = PlayerPrefs.GetFloat("HolyChurchPosY",0);
			float HPosz = PlayerPrefs.GetFloat("HolyChurchPosZ",0);
			int flipx = PlayerPrefs.GetInt("HolyChurchFlip",0);
			Vector3 Hposition;
			Hposition.x = HPosx;
			Hposition.y = HPosy;
			Hposition.z = HPosz;

			GameObject B = Instantiate(HolyChurchToInstantiate,Hposition,Quaternion.identity);
			if(flipx == 1)
				B.GetComponent<SpriteRenderer>().flipX = true;
		}
/		if(PlayerPrefs.GetInt("Cave",0) == 1)
		{
			CaveBuildingBlockCanvas.SetActive(true);
			float CPosx = PlayerPrefs.GetFloat("CavePosX",0);
			float CPosy = PlayerPrefs.GetFloat("CavePosY",0);
			float CPooz = PlayerPrefs.GetFloat("CavePosZ",0);
			int flipx = PlayerPrefs.GetInt("CaveFlip",0);
			Vector3 Cposition;
			Cposition.x = CPosx;
			Cposition.y = CPosy;
			Cposition.z = CPooz;

			GameObject B = Instantiate(Cave,Cposition,Quaternion.identity);
			if(flipx == 1)
				B.GetComponent<SpriteRenderer>().flipX = true;
		}*/
	}


/* 
	public void BuildTree()
	{
		for(int i=1;i<=DecorateIndex.TreeList.Count;i++)
		{
			Debug.Log("Tree"+i);
			int TreeNum = PlayerPrefs.GetInt("Tree" + i , 0);
			float TPosx = PlayerPrefs.GetFloat("Tree" + i + "X",0);
			float TPosy = PlayerPrefs.GetFloat("Tree" + i + "Y",0);
			float TPooz = PlayerPrefs.GetFloat("Tree" + i + "Z",0);
			int Tflip = PlayerPrefs.GetInt("Tree" + i + "Flip",0);
			Vector3 Tposition;
			Tposition.x = TPosx;
			Tposition.y = TPosy;
			Tposition.z = TPooz;
			
			Debug.Log(TreeNum);
			if(TreeNum == 1)
				Tree = NormalTree;
			else if(TreeNum == 2)
				Tree = MintTree;
			else if(TreeNum == 3)
				Tree = PinkTree;
			else if(TreeNum == 4)
				Tree = PuddingTree;
			else if(TreeNum == 5)
				Tree = GrapeTree;
			GameObject tree = Instantiate(Tree,Tposition,Quaternion.identity);	
			if(Tflip == 1)
				tree.GetComponent<SpriteRenderer>().flipX = true;
			tree.name = "Tree"+i;
		}
	}
	
*/
}