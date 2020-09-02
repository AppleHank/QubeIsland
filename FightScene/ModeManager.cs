using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mode
{
	public NodeOff InitialBlood1;
	public NodeOff InitialStandard1;
	public NodeOff InitialStandard2;
	public NodeOff InitialFire;
	public NodeOff FirstChangeIce1;
	public NodeOff FirstChangeLava1;
	public NodeOff FirstChangeSnow1;
	public NodeOff SecondFire;
	public NodeOff SecondLava1;

	public List<NodeOff> PlayerGroundNode = new List<NodeOff>();

	public List<NodeOff> GroundNode = new List<NodeOff>();

	public List<NodeOff> NodeToDel = new List<NodeOff>();

}

public class ModeManager : MonoBehaviour {

	public int FirstChangeTime_Second;
	public int SecondChangeTime_Second;
	[Header("Modes")]
	public Mode Mode1;
	public Mode Mode2;
	public Mode Mode3;
	public Mode Mode4;
	public Mode Mode5;
	public Mode Mode6;

	private Mode ModeSelected;

	[Header("TurretBlueprint")]
	public Blueprint ST;
	public Blueprint BT;
	public Blueprint FT;
	public Blueprint IT;
	public Blueprint SnowT;
	public Blueprint LavaT;
	public Blueprint Ground;

	Mode DecideMode(int mode)
	{
		if(mode == 1)
			return Mode1;
		else if(mode == 2)
			return Mode2;
		else if(mode == 3)
			return Mode3;
		else if(mode == 4)
			return Mode4;
		else if(mode == 5)
			return Mode5;
		else if(mode == 6)
			return Mode6;
		else 
			return new Mode();
	}

	void Start()
	{
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(.1f);
		int mode = Random.Range(1,6);
		ModeSelected = DecideMode(mode);

		int mode2 = Random.Range(1,6);
		Mode PlayerMode = DecideMode(mode2);

		Debug.Log("mode1:"+mode);
		Debug.Log("mode2:"+mode2);

		foreach(NodeOff PGNode in PlayerMode.PlayerGroundNode)
		{
			BuildComputerTurret(PGNode,Ground);
		}

		foreach(NodeOff SNode in ModeSelected.NodeToDel)
		{
			if(SNode.turret == null)
				StartCoroutine(WaitToDel(SNode));
			else
				SNode.AutoSell();
		}

		foreach(NodeOff GNode in ModeSelected.GroundNode)
		{
			BuildComputerTurret(GNode,Ground);
		}

		BuildComputerTurret(ModeSelected.InitialBlood1,BT);
		BuildComputerTurret(ModeSelected.InitialStandard1,ST);
		BuildComputerTurret(ModeSelected.InitialStandard2,ST);
		BuildComputerTurret(ModeSelected.InitialFire,FT);
		
	}

	IEnumerator WaitToDel(NodeOff SNode)
	{
		yield return new WaitForSeconds(.1f);
		if(SNode != null)
			SNode.AutoSell();
		else
			StartCoroutine(WaitToDel(SNode));
	}

	public void FirstChange()
	{



		ModeSelected.InitialBlood1.Upgrade();
		ModeSelected.InitialStandard1.Upgrade();
		ModeSelected.InitialStandard2.Upgrade();
		ModeSelected.InitialFire.Upgrade();

		BuildComputerTurret(ModeSelected.FirstChangeIce1,IT);
		BuildComputerTurret(ModeSelected.FirstChangeLava1,LavaT);
		BuildComputerTurret(ModeSelected.FirstChangeSnow1,SnowT);
	}

	public void SecondChange()
	{
		ModeSelected.FirstChangeIce1.Upgrade();
		ModeSelected.FirstChangeLava1.Upgrade();
		ModeSelected.FirstChangeSnow1.Upgrade();
		BuildComputerTurret(ModeSelected.SecondLava1,LavaT);
		BuildComputerTurret(ModeSelected.SecondFire,FT);
		ModeSelected.SecondLava1.Upgrade();
		ModeSelected.SecondFire.Upgrade();
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
