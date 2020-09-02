using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditOrder : MonoBehaviour {


	public GameObject NowChangeBuilding;

	public void AddOrderNum()
	{
		NowChangeBuilding.GetComponent<SpriteRenderer>().sortingOrder += 1;
	}

	public void DecresOrderNum()
	{
		NowChangeBuilding.GetComponent<SpriteRenderer>().sortingOrder -= 1;
	}

	public void ResetORder()
	{
		this.GetComponent<Canvas>().sortingLayerName = "Default";
	}
}
