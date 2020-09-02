using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

	public Text moneyText;
	public Text AutoAddText;

	void Update ()
	{
		moneyText.text = "$" + Player.Money.ToString();
		AutoAddText.text = "下波增加：" + Player.AddedMoney.ToString();
	}
}
