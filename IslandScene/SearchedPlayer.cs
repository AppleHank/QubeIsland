using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchedPlayer : MonoBehaviour {

	public Text PlayerName;
	public int PlayerId;
	private FriendManager FM;
	public GameObject InviteButton;
	public GameObject InviteButtonBlock;
	public Image HeadImg;
	public Image EyesImg;
	public Image MouseImg;
	public Image ClothImg;
	public Image UpperArmImg;

	public void SetFM(FriendManager FM)
	{
		this.FM = FM;
	}

	public void AddFriend()
	{
		FM.SendInvitedFirend(this);
	}

}
