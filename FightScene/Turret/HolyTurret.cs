using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HolyTurret : NetworkBehaviour {

	private Player player;
	private PlayerOff playeroff;
	public float HealTime;
	public float ProgressTime;
	public Image ProgressBar;
	public Color FillColor;
	private float LastHealTime;
	private Color InitialColor;
	private bool assigndone;
	private bool isOffline;
	public GameObject Effect;
	// Use this for initialization
	void Start () {
		LastHealTime = Time.time;
		if(this.GetComponent<Tower>().GetOffline())
			isOffline = true;
		InitialColor = this.GetComponent<HolyTurret>().ProgressBar.GetComponent<Image>().color;

		if(!isOffline)
			InvokeRepeating("waitAuthority",0,0.5f);
		else
			playeroff = GameObject.FindGameObjectWithTag("HostHealth").GetComponent<PlayerOff>();
			
	}

	void Update()
	{
		ProgressTime = Time.time;
		if(isOffline)
		{	
			ProgressBar.fillAmount = (ProgressTime - LastHealTime) / (HealTime);
		}
		else
			CmdUpdateProgress(this.GetComponent<NetworkIdentity>().netId);
		if(ProgressBar.fillAmount >= 1)
		{
			ProgressBar.GetComponent<Image>().color = FillColor;
			Effect.SetActive(true);
		}
		else
		{
			ProgressBar.GetComponent<Image>().color = InitialColor;
			Effect.SetActive(false);
		}
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

	public void HealOff()
	{
		if(ProgressBar.fillAmount <1)
			return;
		playeroff.Lives += 2;
		LastHealTime = Time.time;
		playeroff.UpdateLive();
	}

	public void Heal()
	{
		if(assigndone)
			CancelInvoke("waitAuthority");
		if(ProgressBar.fillAmount <1)
			return;
		if(player == null)
		{
			Debug.Log("player null");
			return;
		}
		if(player.Lives == player.startLives)
			return;
		LastHealTime = Time.time;
		CmdUpdateLive(player.GetComponent<NetworkIdentity>().netId);
	}

	[Command]
	void CmdUpdateProgress(NetworkInstanceId HolyTurretID)
	{
		RpcUpdateProgress(HolyTurretID);
	}

	[ClientRpc]
	void RpcUpdateProgress(NetworkInstanceId HolyTurretID)
	{
		HolyTurret HTurret = ClientScene.FindLocalObject(HolyTurretID).GetComponent<HolyTurret>();
		HTurret.ProgressBar.fillAmount = (HTurret.ProgressTime - HTurret.LastHealTime) / (HTurret.HealTime);
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
		HealthControl.Lives +=1;
		HealthControl.liveText.text = HealthControl.Lives.ToString() + "/" + HealthControl.startLives.ToString();
		HealthControl.LivesBar.fillAmount = HealthControl.Lives / HealthControl.startLives;
		HealthControl.Clone.UpdateLive(HealthControl.Lives,HealthControl.startLives);
	}
}
