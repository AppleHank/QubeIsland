using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretIllustration : MonoBehaviour {

	public TurretInfoBlueprint StandarTurret;
	public TurretInfoBlueprint BloodTurret;
	public TurretInfoBlueprint FireTurret;
	public TurretInfoBlueprint IceTurret;
	public TurretInfoBlueprint HolyTurret;
	private TurretInfoBlueprint TurretWatchNow;
	public GameObject NextPageButton;
	public GameObject PreviousPageButton;

	public Text TurretName;
	public Image TurretImage;
	public Text TurretInfo;
	public Text TurretPage;

	public void AssignTurretWatchNow()
	{
		if(TurretWatchNow == null )
		{
			Debug.Log("SDASD");
			TurretWatchNow = StandarTurret;
			SetAllInfo(1);
		}
		else
		{
			TurretWatchNow = StandarTurret;
			SetAllInfo(1);
		}
	}

	public void NextPage()
	{
		SetAllInfo(TurretWatchNow.num+1);
	}

	void SetAllInfo(int Nownum)
	{
		if(Nownum == 1)
			TurretWatchNow = StandarTurret;
		else if(Nownum == 2)
			TurretWatchNow = BloodTurret;
		else if(Nownum == 3)
			TurretWatchNow = FireTurret;
		else if(Nownum == 4)
			TurretWatchNow = IceTurret;
		else if(Nownum == 5)
		{
			TurretWatchNow = HolyTurret;
			NextPageButton.SetActive(false);
		}
		
		if(TurretWatchNow.num == 1)
		{
			PreviousPageButton.SetActive(false);
			NextPageButton.SetActive(true);
		}
			

		TurretName.text = TurretWatchNow.Name;
		TurretImage.sprite = TurretWatchNow.Img;
		TurretInfo.text = TurretWatchNow.Info;
		TurretPage.text = Nownum.ToString();

	}

	public void PreviousPage()
	{
		if(TurretWatchNow.num == 5)
			NextPageButton.SetActive(true);
		SetAllInfo(TurretWatchNow.num-1);
	}



}
