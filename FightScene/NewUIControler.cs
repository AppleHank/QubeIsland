using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUIControler : MonoBehaviour {

	public Animator Ani;
	public Animator Ice;
	public Animator Fire;
	public Animator Holy;

	public void SetAnimationBool(string parameter)
	{
		Ani.SetBool(parameter,!Ani.GetBool(parameter));
	}

	public void SetIceBool(string parameter)
	{
		Ice.SetBool(parameter,!Ice.GetBool(parameter));
	}

	public void SetFireBool(string parameter)
	{
		Fire.SetBool(parameter,!Fire.GetBool(parameter));
	}

	public void SetHolyBool(string parameter)
	{
		Holy.SetBool(parameter,!Holy.GetBool(parameter));
	}

}
