using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FireCamp : MonoBehaviour {

	private bool FireBaseComplete;

	void Start () {
		Unlock.FireElement = true;
		if(this.name == "UpgradeFireCamp(Clone)")
			Unlock.LavaElement = true;
//		GameObject FireIllustrationBlock = FindObjectsOfType<BuildingMemory>()[0].FireIllustrationBlock;
//		FireIllustrationBlock.SetActive(false);
		
		if(PlayerData.UserId == null)
		{
			Debug.Log("PlayerIdnull");
			return;
		}

		float PosX = transform.position.x;
		float PosY = transform.position.y;
		float PosZ = transform.position.z;

/* 		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://my-rock-project.firebaseio.com/");
		FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("users");
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("PosX").SetValueAsync(PosX);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("PosY").SetValueAsync(PosY);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("FireCamp").Child("PosZ").SetValueAsync(PosZ);
				Debug.Log(snapshot.Child("0/username").Value);
			}
		});*/

//		PlayerPrefs.SetFloat("FireCampPosX",transform.position.x);
//		PlayerPrefs.SetFloat("FireCampPosY",transform.position.y);
//		PlayerPrefs.SetFloat("FireCampPosZ",transform.position.z);
//		PlayerPrefs.SetInt("FireCamp",1);
	}



}
