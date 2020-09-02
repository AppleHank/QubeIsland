using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illustrations : MonoBehaviour {

	public Sprite Img;
	public string name;
	public Sprite Icon1;
	public Sprite Icon2;
	public int Health;
	public string Speed;
	[TextArea]
	public string Info;
	public IllustrationManager IM;

	public void SeeMonsterIllustation()
	{
		IM.SeeMonsterIllustation(this,Img,name,Icon1,Icon2,Health,Speed,Info);
	}
}
