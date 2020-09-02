using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePVE : MonoBehaviour {

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void ContinueGame()
	{
		Time.timeScale = 1;
	}

}
