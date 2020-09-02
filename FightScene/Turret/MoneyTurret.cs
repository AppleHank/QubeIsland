using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MoneyTurret : NetworkBehaviour {

	public int AddMoney;
	public int DecreseLife;
	private bool assigndone;
	private Player player;
	public GameObject ChangeEffect;

	void Start()
	{
		StartCoroutine(WaitForAssignAuthority());
	}

	IEnumerator WaitForAssignAuthority()
	{
		yield return new WaitForSeconds(.8f);
		InvokeRepeating("waitAuthority",0,0.5f);
	}



	public void MoneyOff()
	{
		Debug.Log("offLine");
		PlayerOff playeroff = GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<PlayerOff>();
		if(playeroff.Lives <= 2)
			return;
		PlayerOff.Money += AddMoney;
		playeroff.Lives -= DecreseLife;
		Debug.Log(PlayerOff.Money);
		Debug.Log(playeroff.Lives);
		playeroff.UpdateLive();
	}

	public void Money()
	{
		if(!assigndone)
				return;
			CancelInvoke("waitAuthority");
		if(player == null)
		{
			Debug.Log("player null");
			return;
		}
		if(player.Lives <=2)
			return;
		CmdUpdateLive(player.GetComponent<NetworkIdentity>().netId);
		Player.Money += AddMoney;
		ChangeEffect.SetActive(true);
		StartCoroutine(CloseEffect());
	}

	IEnumerator CloseEffect()
	{
		yield return new WaitForSeconds(2f);
		ChangeEffect.SetActive(false);
	}

	void waitAuthority()
	{
		if(isServer & hasAuthority)
		{
			player = GameObject.FindGameObjectsWithTag("HostHealth")[0].GetComponent<Player>();
			assigndone = true;
		}
		if(!isServer & hasAuthority)
		{
			player = GameObject.FindGameObjectsWithTag("ClientHealth")[0].GetComponent<Player>();
			assigndone = true;
		}
	}	
	
	[Command]
	public void CmdUpdateLive(NetworkInstanceId healthid)
	{
		RpcUpdateLive(healthid);
	}

	[ClientRpc]
	public void RpcUpdateLive(NetworkInstanceId healthid)
	{
		Player HealthControl = ClientScene.FindLocalObject(healthid).GetComponent<Player>();
		HealthControl.Lives -=2;
		HealthControl.liveText.text = HealthControl.Lives.ToString() + "/" + HealthControl.startLives.ToString();
		HealthControl.LivesBar.fillAmount = HealthControl.Lives / HealthControl.startLives;
		HealthControl.Clone.UpdateLive(HealthControl.Lives,HealthControl.startLives);
	}
}
