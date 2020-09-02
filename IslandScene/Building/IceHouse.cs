using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class IceHouse : MonoBehaviour {

	void Start () {

		Unlock.IceElement = true;	
		if(this.name == "UpgradeIceHouse(Clone)")
			Unlock.FrozeElement = true;		
//		GameObject IceIllustrationBlock = FindObjectsOfType<BuildingMemory>()[0].IceIllustrationBlock;
	//	IceIllustrationBlock.SetActive(false);
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
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("Unlock").SetValueAsync(1);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("PosX").SetValueAsync(PosX);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("PosY").SetValueAsync(PosY);
				mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("IceHouse").Child("PosZ").SetValueAsync(PosZ);
			}
		});
	//	PlayerPrefs.SetFloat("IceHousePosX",transform.position.x);
	//	PlayerPrefs.SetFloat("IceHousePosY",transform.position.y);
	//	PlayerPrefs.SetFloat("IceHousePosZ",transform.position.z);
	//	PlayerPrefs.SetInt("IceHouse",1);
	}

}
