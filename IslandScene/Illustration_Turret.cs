using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illustration_Turret : MonoBehaviour {

	public string Name;
	public int DamageFullStarNum;
	public bool DamagehalfStar;
	public int RangeFullStarNum;
	public bool RangehalfStar;
	[TextArea]
	public string Info;
	public Sprite TurretSprite;
	public IllustrationManager IM;

	[Header("Upgrade")]
	public int UpgradeDamageFullStarNum;
	public bool UpgradeDamagehalfStar;
	public int UpgradeRangeFullStarNum;
	public bool UpgradeRangehalfStar;
	public Sprite UpgradeTurretSprite;
	

	public void WhenClick()
	{
		IM.SeeTurretInfo(this,TurretSprite,DamageFullStarNum,DamagehalfStar,Info,Name,RangeFullStarNum,RangehalfStar);
		IM.SetUpgradeTurretInfo(UpgradeDamageFullStarNum,UpgradeDamagehalfStar,UpgradeRangeFullStarNum,UpgradeRangehalfStar,UpgradeTurretSprite);
	}

}
