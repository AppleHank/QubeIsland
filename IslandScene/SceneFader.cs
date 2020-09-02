using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public PetMoveManager PMM;
	public FriendManager FM;
	public GameObject LoadPanel;
	public Slider ProgressBar;
	public AdTest AdManager;
	
	public void LoadLevel(string SceneName)
	{
		
        FM = GameObject.FindObjectOfType<FriendManager>();
        PMM = GameObject.FindObjectOfType<PetMoveManager>();
        AdManager = GameObject.FindObjectOfType<AdTest>();
		if(PMM != null)
			PMM.UnSubScribePet();
		if(FM != null)
		{
			FM.UnSubScribeFriend();
			FM.StopTellFriendOnline();
		}
		if(AdManager != null)
            AdManager.Unsubscribe();
		StartCoroutine(LoadAsynchronously(SceneName));
	}

	IEnumerator LoadAsynchronously(string SceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

		while(!operation.isDone)
		{
			LoadPanel.SetActive(true);
			float Progress = Mathf.Clamp01(operation.progress / 0.9f);
			ProgressBar.value = Progress;

			yield return null;
		}
	}

}
