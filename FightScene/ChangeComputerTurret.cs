using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeComputerTurret : MonoBehaviour {

	public int FirstChangeTime_Second;
	public int SecondChangeTime_Second;
	public NodeOff InitialBlood1;
	public NodeOff InitialStandard1;
	public NodeOff InitialStandard2;
	public NodeOff FirstChangeBlood1;
	public NodeOff FirstChangeIce1;
	public NodeOff FirstChangeFire1;
	public NodeOff FirstChangeSnow1;
	public NodeOff FirstChangeStandard;
	public NodeOff SecondLava1;
	public NodeOff SecondLava2;

	public NodeOff GroundNode1;
	public NodeOff GroundNode2;
	public NodeOff GroundNode3;
	public NodeOff GroundNode4;
	public NodeOff GroundNode5;
	public NodeOff GroundNode6;
	public NodeOff GroundNode7;
	public NodeOff GroundNode8;
	public NodeOff GroundNode9;
	public NodeOff GroundNode10;
	public NodeOff GroundNode11;
	public NodeOff GroundNode12;
	public NodeOff GroundNode13;
	public NodeOff GroundNode14;
	public NodeOff GroundNode15;
	public NodeOff GroundNode16;
	public NodeOff GroundNode17;
	public NodeOff GroundNodeSell;

	public Blueprint ST;
	public Blueprint BT;
	public Blueprint FT;
	public Blueprint IT;
	public Blueprint SnowT;
	public Blueprint LavaT;
	public Blueprint Ground;

	public void FirstChange()
	{



		InitialBlood1.Upgrade();
		InitialStandard1.Upgrade();
		InitialStandard2.Upgrade();

		BuildComputerTurret(GroundNode1,Ground);
		BuildComputerTurret(GroundNode2,Ground);
		BuildComputerTurret(GroundNode3,Ground);
		BuildComputerTurret(GroundNode4,Ground);
		BuildComputerTurret(GroundNode5,Ground);
		BuildComputerTurret(GroundNode6,Ground);
		BuildComputerTurret(GroundNode7,Ground);
		BuildComputerTurret(GroundNode8,Ground);
		BuildComputerTurret(GroundNode9,Ground);
		BuildComputerTurret(GroundNode10,Ground);
		BuildComputerTurret(GroundNode11,Ground);
		BuildComputerTurret(GroundNode12,Ground);
		BuildComputerTurret(GroundNode13,Ground);
		BuildComputerTurret(GroundNode14,Ground);
		BuildComputerTurret(GroundNode15,Ground);
		BuildComputerTurret(GroundNode16,Ground);
		BuildComputerTurret(GroundNode17,Ground);
		GroundNodeSell.AutoSell();


	//	BuildComputerTurret(FirstChangeBlood1,BT);
		BuildComputerTurret(FirstChangeIce1,IT);
		BuildComputerTurret(FirstChangeFire1,FT);
		BuildComputerTurret(FirstChangeSnow1,SnowT);
	//	BuildComputerTurret(FirstChangeStandard,ST);
	}

	public void SecondChange()
	{
		FirstChangeIce1.Upgrade();
		FirstChangeFire1.Upgrade();
		FirstChangeSnow1.Upgrade();
		BuildComputerTurret(SecondLava1,LavaT);
		BuildComputerTurret(SecondLava2,LavaT);
	}

	void BuildComputerTurret(NodeOff Node,Blueprint turretBlueprint)
	{
		Node.turretBlueprint = turretBlueprint;
		GameObject _turret = (GameObject) Instantiate(Node.turretBlueprint.prefab,Node.GetBuildPosition(),Quaternion.identity);
		if(_turret.tag != "path")
		{
			_turret.GetComponent<Tower>().SetInitialTurret();
			_turret.GetComponent<Tower>().SetOffLine();		
		Vector3 temp = _turret.transform.position;
		temp.y -= 3.1f;
		_turret.transform.position = temp;
		}
		Node.turret = _turret;	
	}
}
