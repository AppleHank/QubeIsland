using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GoodsInfo
{
	public Text DiamandText;
	public Text BackText;
	public int Diamand;
	public int Back;
}

public class Shoppingmall : MonoBehaviour {

	public PetEgg petegg;
	public GoodsManager goodsmanager;
	public GameObject ErrorPanel;
	public GameObject BuyPanel;

	public GoodsInfo PetEgg;
	public GoodsInfo Cake;
	public GoodsInfo Food;
	public GoodsInfo UpFood;
	public GoodsInfo RecoverPotion;
	public GoodsInfo HealthPotion;
	public GoodsInfo NormalTree;
	public GoodsInfo GreenTree;
	public GoodsInfo MintTree;
	public GoodsInfo PinkTree;
	public GoodsInfo PuddingTree;
	public GoodsInfo GrapeTree;
	public GoodsInfo TrebleTree;
	public GoodsInfo RustTree;
	public GoodsInfo RustForest;
	public GoodsInfo Fir;
	public GoodsInfo LittleFirFroest;
	public GoodsInfo FirForest;
	public GoodsInfo DreamForest;
	public GoodsInfo LittleForest;
	public GoodsInfo ChristmasTree;
	public GoodsInfo ChangeDoll;
	public int TreeBack;
	public int TreeDiamand;
	public int ForestBack;
	public int ForestDiamand;
	public AudioSource BuyAudio;
	public PetEgg TutroEgg;
	public GameObject BuyChangeDollPanel;

	

	void Start()
	{
		PetEgg.DiamandText.text = PetEgg.Diamand.ToString();
		PetEgg.BackText.text = PetEgg.Back.ToString();
		Cake.DiamandText.text = Cake.Diamand.ToString();
		Cake.BackText.text = Cake.Back.ToString();
		Food.DiamandText.text = Food.Diamand.ToString();
		Food.BackText.text = Food.Back.ToString();
		UpFood.DiamandText.text = UpFood.Diamand.ToString();
		UpFood.BackText.text = UpFood.Back.ToString();
		RecoverPotion.DiamandText.text = RecoverPotion.Diamand.ToString();
		RecoverPotion.BackText.text = RecoverPotion.Back.ToString();
		HealthPotion.DiamandText.text = HealthPotion.Diamand.ToString();
		HealthPotion.BackText.text = HealthPotion.Back.ToString();
		MintTree.BackText.text = TreeBack.ToString();
		MintTree.DiamandText.text = TreeDiamand.ToString();
		PinkTree.BackText.text = TreeBack.ToString();
		PinkTree.DiamandText.text = TreeDiamand.ToString();
	//	PuddingTree.BackText.text = TreeBack.ToString();
	//	PuddingTree.DiamandText.text = TreeDiamand.ToString();
	//	GrapeTree.BackText.text = TreeBack.ToString();
	//	GrapeTree.DiamandText.text = TreeDiamand.ToString();
		TrebleTree.BackText.text = TreeBack.ToString();
		TrebleTree.DiamandText.text = TreeDiamand.ToString();
		GreenTree.BackText.text = TreeBack.ToString();
		GreenTree.DiamandText.text = TreeDiamand.ToString();
		ChangeDoll.BackText.text = ChangeDoll.Back.ToString();
		ChangeDoll.DiamandText.text = ChangeDoll.Diamand.ToString();
		RustTree.BackText.text = TreeBack.ToString();
		RustTree.DiamandText.text = TreeDiamand.ToString();
		Fir.BackText.text = TreeBack.ToString();
		Fir.DiamandText.text = TreeDiamand.ToString();
		ChristmasTree.BackText.text = TreeBack.ToString();
		ChristmasTree.DiamandText.text = TreeDiamand.ToString();
		PuddingTree.BackText.text = TreeBack.ToString();
		PuddingTree.DiamandText.text = TreeDiamand.ToString();
		GrapeTree.BackText.text = TreeBack.ToString();
		GrapeTree.DiamandText.text = TreeDiamand.ToString();
//		LittleFirFroest.BackText.text = ForestBack.ToString();
//		LittleFirFroest.DiamandText.text = ForestDiamand.ToString();
		FirForest.BackText.text = ForestBack.ToString();
		FirForest.DiamandText.text = ForestDiamand.ToString();
		RustForest.BackText.text = ForestBack.ToString();
		RustForest.DiamandText.text = ForestDiamand.ToString();
		LittleForest.BackText.text = ForestBack.ToString();
		LittleForest.DiamandText.text = ForestDiamand.ToString();
		DreamForest.BackText.text = ForestBack.ToString();
		DreamForest.DiamandText.text = ForestDiamand.ToString();
	}



	public void CheckDiamand(int ProductNum)
	{
		if(ProductNum == 1)
		{
			if(Meterial.diamandbee<PetEgg.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				Debug.Log(TutorIsland.isTutor);
				if(TutorIsland.isTutor)
				{
					TutroEgg.Lotter();
					Debug.Log("!!!!!!!!!!!!!!!!111111111111111111111111111111111111111111111111!");
				}
				else
				{
					petegg.Lotter();
					Meterial.Reduce("Diamand",PetEgg.Diamand);
				}
				BuyAudio.Play();
			}
		}			
		if(ProductNum == 2 )
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockGreenTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}	
		if(ProductNum == 3 )
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockMintTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 4 )
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockPinkTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 5 )
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockPuddingTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 6 )
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockGrapeTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 7 )
		{
			if(Meterial.diamandbee< Food.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("Food",1);
				Meterial.Reduce("Diamand",Food.Diamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 8 )
		{
			if(Meterial.diamandbee<Cake.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("Cake",1);
				Meterial.Reduce("Diamand",Cake.Diamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 9)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockTrebleTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 10)
		{
			if(Meterial.diamandbee < UpFood.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("UpFood",1);
				Meterial.Reduce("Diamand",UpFood.Diamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 11)
		{
			if(Meterial.diamandbee < RecoverPotion.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("RecoverPotion",1);
				Meterial.Reduce("Diamand",RecoverPotion.Diamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 12)
		{
			if(Meterial.diamandbee < HealthPotion.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("HealthPotion",1);
				Meterial.Reduce("Diamand",HealthPotion.Diamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 13)
		{
			if(Meterial.diamandbee < ChangeDoll.Diamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyChangeDollPanel.SetActive(true);
				Meterial.Reduce("Diamand",ChangeDoll.Diamand);
				BuyAudio.Play();
				SceneManager.LoadScene("ChoseCharacter");
			}
		}
		if(ProductNum == 14)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockRustTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 15)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockRustForest();
				Meterial.Reduce("Diamand",ForestDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 16)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockFir();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 17)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockFirForest();
				Meterial.Reduce("Diamand",ForestDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 18)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockDreamForest();
				Meterial.Reduce("Diamand",ForestDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 19)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockLittleForest();
				Meterial.Reduce("Diamand",ForestDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 20)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockChristmasTree();
				Meterial.Reduce("Diamand",TreeDiamand);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 21)
		{
			if(Meterial.diamandbee<TreeDiamand)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockLittleFirForest();
				Meterial.Reduce("Diamand",ForestDiamand);
				BuyAudio.Play();
			}
		}
	}

	public void CheckBack(int ProductNum)
	{
		if(ProductNum == 1)
		{
			if(Meterial.backbee<PetEgg.Back)
				ErrorPanel.SetActive(true);
			else
			{
				if(TutorIsland.isTutor)
				{
					TutroEgg.Lotter();
				}
				else
				{
					petegg.Lotter();
					Meterial.Reduce("Back",PetEgg.Back);
				}
				BuyAudio.Play();
			}
		}
		if(ProductNum == 2 )
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockGreenTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}			
		if(ProductNum == 3 )
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockMintTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 4 )
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockPinkTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 5 )
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockPuddingTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 6 )
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockGrapeTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 7 )
		{
			if(Meterial.backbee < Food.Back)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("Food",1);
				Meterial.Reduce("Back",Food.Back);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 8 )
		{
			if(Meterial.backbee<Cake.Back)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("Cake",1);
				Meterial.Reduce("Back",Cake.Back);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 9)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockTrebleTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 10)
		{
			if(Meterial.backbee < UpFood.Back)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("UpFood",1);
				Meterial.Reduce("Back",UpFood.Back);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 11)
		{
			if(Meterial.backbee < RecoverPotion.Back)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("RecoverPotion",1);
				Meterial.Reduce("Back",RecoverPotion.Back);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 12)
		{
			if(Meterial.backbee < HealthPotion.Back)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				Meterial.AddMaterial("HealthPotion",1);
				Meterial.Reduce("Back",HealthPotion.Back);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 13)
		{
			if(Meterial.backbee < ChangeDoll.Back)
				ErrorPanel.SetActive(true);
			else
			{
				BuyChangeDollPanel.SetActive(true);
				Meterial.Reduce("Back",ChangeDoll.Back);
				BuyAudio.Play();
				SceneManager.LoadScene("ChoseCharacter");
			}
		}
		if(ProductNum == 14)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockRustTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 15)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockRustForest();
				Meterial.Reduce("Back",ForestBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 16)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockFir();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 17)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockFirForest();
				Meterial.Reduce("Back",ForestBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 18)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockDreamForest();
				Meterial.Reduce("Back",ForestBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 19)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockLittleForest();
				Meterial.Reduce("Back",ForestBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 20)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockChristmasTree();
				Meterial.Reduce("Back",TreeBack);
				BuyAudio.Play();
			}
		}
		if(ProductNum == 21)
		{
			if(Meterial.backbee<TreeBack)
				ErrorPanel.SetActive(true);
			else
			{
				BuyPanel.SetActive(true);
				goodsmanager.UnlockLittleFirForest();
				Meterial.Reduce("Back",ForestBack);
				BuyAudio.Play();
			}
		}
		
	}


}
