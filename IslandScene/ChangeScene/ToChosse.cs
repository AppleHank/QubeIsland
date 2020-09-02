using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
  using System.Threading.Tasks;
//  using Firebase.Auth;
using Firebase;
using Firebase.Database;

public class ToChosse : MonoBehaviour {


	void Awake()
	{
		Debug.Log("ToChoseAwake");
	}

	async void Start () {

		Debug.Log("1");
//		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		Debug.Log("2");
//		Firebase.Auth.FirebaseUser user = auth.CurrentUser;
		Debug.Log("3");
		SceneManager.LoadScene("ChoseCharacter");
/* 		Task<bool> over = await FirebaseDatabase.DefaultInstance.GetReference("username").GetValueAsync().ContinueWith(async (Firetask) => {
			Debug.Log("4");
			if (Firetask.IsFaulted) {
				Debug.Log("error");
			}
			else if (Firetask.IsCompleted) {
				DataSnapshot snapshot = Firetask.Result;
				foreach (DataSnapshot data in snapshot.Children)
				{
					if(data.Child("Id").Value != null)
					{
						string Uid = data.Child("Id").Value.ToString();
						Debug.Log(Uid);
						if(Uid == user.UserId)
						{
							Debug.Log("uID:"+int.Parse(data.Key.ToString()));
							PlayerData.UserId = int.Parse(data.Key.ToString());
							SignInWithGoogle.hasAccount = true;
							PlayerData.PlayerName = data.Child("Name").Value.ToString();
						}
					}
					else
						Debug.Log("---------------------------------");
				}
			}
			Debug.Log("Test");
			return true;
		});*/
//		StartCoroutine(ChangeScene());
	}
	
	IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds(1.25f);
		Debug.Log("ToCHosse");
	}
}
