using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour {

	public Blueprint standardTurret;
	public Blueprint BloodTurret;
	public Blueprint FireTurret;
	public Blueprint IceTurret;
	public Blueprint HolyTurret;
	public Blueprint FrozeTurret;
	public Blueprint MoneyTurret;
	public Blueprint LavaTurret;
	public Blueprint Ground;
	public NODE node;
	public GameObject Grid;
	public GameObject UI;
	public GameObject UIpage2;
	private Blueprint turrettobuild;
	public GameObject FireTurretBlock;
	public GameObject IceTurretBlock;
	public GameObject HolyTurretBlock;
	public GameObject LavaTurretBlock;
	public GameObject FrozeTurretBlock;
	public GameObject MoneyTurretBlock;
	

	buildmanager buildmanager;

	void Start(){
		buildmanager = buildmanager.instance;

		if(FireTurret.Buttom != false) //Road spawn manager have shop script,but no turret imformation assign
		{
			InvokeRepeating("UpdateButtom",0,0.2f);
			BloodTurret.turretcost.text = '$' + BloodTurret.cost.ToString();
			standardTurret.turretcost.text = '$' + standardTurret.cost.ToString();
			
			if(Unlock.FireElement)
			{
				FireTurretBlock.SetActive(false);
				FireTurret.Buttom.SetActive(true);
				FireTurret.turretcost.text = '$' + FireTurret.cost.ToString();
			}
			if(Unlock.IceElement)
			{
				IceTurret.Buttom.SetActive(true);
				IceTurretBlock.SetActive(false);
				IceTurret.turretcost.text = '$' + IceTurret.cost.ToString();
			}
			if(Unlock.HolyElement)
			{
				HolyTurret.Buttom.SetActive(true);
				HolyTurretBlock.SetActive(false);
				HolyTurret.turretcost.text = '$' + HolyTurret.cost.ToString();
			}
			if(Unlock.LavaElement)
			{
				LavaTurret.Buttom.SetActive(true);
				LavaTurretBlock.SetActive(false);
				LavaTurret.turretcost.text = '$' + LavaTurret.cost.ToString();
			}
			if(Unlock.FrozeElement)
			{
				FrozeTurret.Buttom.SetActive(true);
				FrozeTurretBlock.SetActive(false);
				FrozeTurret.turretcost.text = '$' + FrozeTurret.cost.ToString();
			}
			if(Unlock.MoneyElement)
			{
				MoneyTurret.Buttom.SetActive(true);
				MoneyTurretBlock.SetActive(false);
				MoneyTurret.turretcost.text = '$' + MoneyTurret.cost.ToString();
			}
		}
				
	}

	void UpdateButtom()
	{
		if(BloodTurret.cost>Player.Money)
		{
			HolyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			MoneyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			IceTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FrozeTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			BloodTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FireTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			LavaTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			standardTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
		}		
		else if(standardTurret.cost>Player.Money)
		{
			HolyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			MoneyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			IceTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FrozeTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FireTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			LavaTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			standardTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
		}
		else if(IceTurret.cost>Player.Money)
		{
			HolyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			MoneyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			IceTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FrozeTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FireTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			LavaTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
		}	
		else if(FireTurret.cost>Player.Money)
		{
			HolyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			MoneyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			FireTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			LavaTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
		}
		else if(HolyTurret.cost>Player.Money)
		{
			HolyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
			MoneyTurret.Buttom.GetComponent<Image>().color = new Color32(75,75,75,255);
		}	
		//Have money desision

		if(HolyTurret.cost<=Player.Money)
		{
			FireTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			LavaTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			IceTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			FrozeTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			HolyTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			MoneyTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			BloodTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			standardTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
		else if(FireTurret.cost<=Player.Money)
		{
			FireTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			LavaTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			IceTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			FrozeTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			BloodTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			standardTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
		else if(IceTurret.cost<=Player.Money)
		{
			IceTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			FrozeTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			BloodTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			standardTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
		else if(standardTurret.cost<=Player.Money)
		{
			BloodTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
			standardTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
		else if(BloodTurret.cost<=Player.Money)
		{
			BloodTurret.Buttom.GetComponent<Image>().color = new Color32(255,255,255,255);
		}
	}

	void SelectTurret() //After all check to ensure that can build
	{
		buildmanager.SelectTurretToBuild(turrettobuild);
		UI.SetActive(false);
//		UIpage2.SetActive(false);
	}

	public void assignturret(int number)
	{

		if(number == 1)
			turrettobuild = standardTurret;
		else if(number == 2)
			turrettobuild = BloodTurret;
		else if(number == 3)
			turrettobuild = FireTurret;
		else if(number == 4)
			turrettobuild = IceTurret;
		//number5 is IceGround
		else if(number == 6)
			turrettobuild = HolyTurret;
		else if(number == 7)
			turrettobuild = FrozeTurret;
		else if(number == 8)
			turrettobuild = MoneyTurret;
		else if(number == 9)
			turrettobuild = LavaTurret;
		if(Player.Money < turrettobuild.cost){
			return;
		}

		SelectTurret();
	}


	public void SelectGround(){
		if(Player.Money < Ground.cost){
			Debug.Log("Dont have money");
			return;
		}
		buildmanager.SelectTurretToBuild(Ground);
	}




}
