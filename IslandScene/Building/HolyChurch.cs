using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class HolyChurch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Unlock.HolyElement = true;
		if(this.name == "UpgradeHolyChurch(Clone)")
			Unlock.MoneyElement = true;
//		GameObject HolyIllustrationBlock = FindObjectsOfType<BuildingMemory>()[0].HolyIllustrationBlock;
//		HolyIllustrationBlock.SetActive(false);

		if(PlayerData.UserId == null)
		{
			Debug.Log("PlayerIdnull");
			return;
		}

		float PosX = transform.position.x;
		float PosY = transform.position.y;
		float PosZ = transform.position.z;

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://my-rock-project.firebaseio.com/");
		FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("users");
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("Unlock").SetValueAsync(1);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("PosX").SetValueAsync(PosX);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("PosY").SetValueAsync(PosY);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("HolyChurch").Child("PosZ").SetValueAsync(PosZ);
			}
		});

	//	PlayerPrefs.SetFloat("HolyChurchPosX",transform.position.x);
	//	PlayerPrefs.SetFloat("HolyChurchPosY",transform.position.y);
	//	PlayerPrefs.SetFloat("HolyChurchPosZ",transform.position.z);
	//	PlayerPrefs.SetInt("HolyChurch",1);
	}
}
