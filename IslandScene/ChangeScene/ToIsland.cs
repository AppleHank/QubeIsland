using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToIsland : MonoBehaviour {

	public ProgressControl PC;

	// Use this for initialization
	void Start () {
		Debug.Log("ToIsland");
		StartCoroutine(ChangeScene());
	}
	
	IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds(2f);
		PC.LoadLevel("Island");
	}
}
