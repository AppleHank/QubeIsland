using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CaveOnTouch : MonoBehaviour {

	public CaveManager CaveManager;
	public GameObject CaveCanvas;
	public GameObject CaveUI;
	public GameObject FriendUI;
	public BuildingManager buildingmanager;
	public AudioSource ClickAudio;
    private Vector3 touchPosWorld;
	private bool CanOpen;
	void Start()
	{
		buildingmanager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
		
		StartCoroutine(EnableOpen());

		ClickAudio = GameObject.Find("OpenAudiance").GetComponent<AudioSource>();

		GameObject CaveBlock = FindObjectsOfType<BuildingMemory>()[0].CaveBuildingBlockCanvas;
		CaveBlock.SetActive(true);

	}

	IEnumerator EnableOpen()
	{
		yield return new WaitForSeconds(.5f);
		CanOpen = true;
	}

	private bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
	}
	
	void Update()
	{
		if (Input.touchCount > 0) {
			
			if(IsPointerOverUIObject() | buildingmanager.GetClickedButton())
				return;

			if(TouchControl.MovingCamera)
				return;
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
 
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
 
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			if(hitInformation.collider == null)
				return;
			if(hitInformation.collider.name == "Cave(Clone)" | hitInformation.collider.name == "TutorCave(Clone)")
			{
				OpenCave();
			}
			else if(hitInformation.collider.name == "Canoe(Clone)")
				OpenFriend();

		}
	}
	void OpenCave()
	{
		if(!CanOpen)
			return;
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Ended)
		{
			if(BuildingManager.isEditMode)
				return;
			CaveUI.SetActive(true);
			ClickAudio.Play();
		}
	}
	void OpenFriend()
	{
		if(!CanOpen)
			return;
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Ended)
		{
			if(BuildingManager.isEditMode)
				return;
			FriendUI.SetActive(true);
			ClickAudio.Play();
		}
	}
}
