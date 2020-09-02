using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class CapturableEnemy : NetworkBehaviour {

	public int PetId;
	public GameObject Sound;
	private bool OverCapture;
	private GameObject CaptrueEffect;
	public GameObject CaptrueEffectPrefab;
	private buildmanager BM;

	void Start()
	{
		BM = GameObject.FindObjectOfType<buildmanager>();
 		if(BM.isTutor)
		{
			if(TutorFightV2.OverCapture)
				return;
				
			GameObject.FindObjectOfType<TutorFightV2>().CaptureTutor();
		//	TutorFightV2.OverCapture = true;
			if(GameObject.FindObjectOfType<CameraControlOff>().NowWatchEnemyArea)
			{
				GameObject.FindObjectOfType<CameraControlOff>().ChangeView();
				BM.ChangeView();
			}
		}
		
		CaptrueEffect = Instantiate(CaptrueEffectPrefab,transform.position,Quaternion.identity);
		CaptrueEffect.transform.parent = gameObject.transform;
		CaptrueEffect.transform.position += new Vector3 (0,4,0);
		CaptrueEffect.transform.localScale = new Vector3(8,8,8);
	}
	
	public GameObject GetEffect()
	{
		return CaptrueEffect;
	}

	void OnMouseDown()
	{
		if(BM.isTutor)
		{
			if(!TutorFightV2.OverCapture)
			{
				GameObject.FindObjectOfType<TutorFightV2>().CaptureTutor();
				TutorFightV2.OverCapture = true;
			}	
		}
		GameObject SoundOBJ = Instantiate(Sound,transform.position,Quaternion.identity);
		Destroy(CaptrueEffect);
//		PetManager.Capture(gameObject);
		Debug.Log("Capture!!!");
		FirebaseDatabase.DefaultInstance.GetReference("users").Child(PlayerData.UserId.ToString()).Child("Pet").GetValueAsync().ContinueWith(task => {
		Debug.Log("Capture In DB!!!");
			if (task.IsFaulted) {
				Debug.Log("error");
			}
			else if (task.IsCompleted) {
		Debug.Log("Capture!!!111");
				DataSnapshot snapshot = task.Result;
		Debug.Log("Capture!!!2222");
				DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("users");
		Debug.Log("Capture33333!!!");

			float PetNumNow = snapshot.ChildrenCount;
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("PetId").SetValueAsync(PetId);
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("PetLife").SetValueAsync(5);
		Debug.Log("Capture55555!!!");
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("PetLevel").SetValueAsync(1);
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("PetGrade").SetValueAsync(1);
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("IsWarrior").SetValueAsync(0);
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("PetExp").SetValueAsync(0);
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("IsOnIsland").SetValueAsync(0);
			mDatabaseRef.Child(PlayerData.UserId.ToString()).Child("Pet").Child("Pet"+PetNumNow.ToString()).Child("Burial").SetValueAsync(0);
		Debug.Log("Capture44444!!!");
			}
		Debug.Log("Capture!!!66666");
		});
		gameObject.SetActive(false);
		
		Destroy(SoundOBJ,2f);
	}



}
