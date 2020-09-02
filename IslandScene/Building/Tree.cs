using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {


	void Start()
	{
		if(this.name == "樹(Clone)")
			PlayerPrefs.SetInt("Tree" + DecorateIndex.GetIndex(), 1);
		if(this.name == "薄荷樹(Clone)")
			PlayerPrefs.SetInt("Tree" + DecorateIndex.GetIndex(), 2);
		if(this.name == "粉粉樹(Clone)")
			PlayerPrefs.SetInt("Tree" + DecorateIndex.GetIndex(), 3);
		if(this.name == "焦糖布丁樹(Clone)")
			PlayerPrefs.SetInt("Tree" + DecorateIndex.GetIndex(), 4);
		if(this.name == "香冰葡萄樹(Clone)")
			PlayerPrefs.SetInt("Tree" + DecorateIndex.GetIndex(), 5);

		PlayerPrefs.SetFloat("Tree" + DecorateIndex.GetIndex() + "X",transform.position.x);
		PlayerPrefs.SetFloat("Tree" + DecorateIndex.GetIndex() + "Y",transform.position.y);
		PlayerPrefs.SetFloat("Tree" + DecorateIndex.GetIndex() + "Z",transform.position.z);
		PlayerPrefs.SetInt("TreeList" + DecorateIndex.GetIndex(),1);

		DecorateIndex.AddListNum();

		Debug.Log("Tree"+DecorateIndex.GetIndex());
	}

}
