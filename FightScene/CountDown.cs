using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CountDown : NetworkBehaviour {

	public Text CountDownText;
	private int NowLeftTotalSecond;
	public GameManageNewOff GameManageroff;
	public GameManagerNew GameManager;
	public GameObject CountDownCanvas;
	private NetPlayer netplayer;
	public bool isOffLine;
	private int NowLeftMinute;
	private int NowLeftSecond;
	public ChangeComputerTurret CCT;
	public ModeManager MM;
	public bool isTutor;
	public bool isMode;


	// Use this for initialization
	void Start () {
		NowLeftTotalSecond = 300;
		if(isTutor)
			return;
		if(!isOffLine)
		{
			if(!isServer)
				return;
		}
		else	
			InvokeRepeating("CountDownTime",1f,1f);
//		Invoke("OpenCanvas",240f);
	}

	public void StartCount()
	{
		
		InvokeRepeating("CountDownTime",1f,1f);
	}	
	public void StopCount()
	{
		
		CancelInvoke("CountDownTime");
	}

/* 	void OpenCanvas()
	{
		Debug.Log(Time.time);
		CountDownCanvas.SetActive(true);
	}*/

	public void SetNetPlayer(NetPlayer NP)
	{
		netplayer = NP;
		InvokeRepeating("CountDownTime",1f,1f);
	}
	
	void CountDownTime()
	{
		if(NowLeftTotalSecond == 0)
		{
			CancelInvoke("CountDownTime");
			if(isOffLine)
				GameManageroff.DefineTimeWinLose();
			else
				GameManager.DefineTimeWinLose();

			this.GetComponent<AudioSource>().Play();
		}

		NowLeftTotalSecond -= 1;
		if(isOffLine)
		{
			if(isMode)
			{
				if(MM.FirstChangeTime_Second == NowLeftTotalSecond)
					MM.FirstChange();
				else if(MM.SecondChangeTime_Second == NowLeftTotalSecond)
					MM.SecondChange();
			}
			else
			{
				if(CCT.FirstChangeTime_Second == NowLeftTotalSecond)
					CCT.FirstChange();
				else if(CCT.SecondChangeTime_Second == NowLeftTotalSecond)
					CCT.SecondChange();
			}
		}
		if(NowLeftTotalSecond <= 0)
		{
			NowLeftMinute = 0;
			NowLeftSecond = 0;
		}
		else
		{
			NowLeftMinute = NowLeftTotalSecond/60;
			NowLeftSecond = NowLeftTotalSecond%60;
		}
		
		CallNetPlayer();
	}

	void CallNetPlayer()
	{
		if(isOffLine)
		{
			if(NowLeftSecond < 10)
				CountDownText.text = NowLeftMinute.ToString() + ":0" + NowLeftSecond.ToString();
			else
				CountDownText.text = NowLeftMinute.ToString() + ":" + NowLeftSecond.ToString();
		}
		else
		{
			netplayer.CmdCountTime(NowLeftMinute,NowLeftSecond);
		}
	}

}
