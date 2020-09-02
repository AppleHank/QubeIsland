using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buildmanager : MonoBehaviour {

	public static buildmanager instance;
	private GameObject NowPreturrretOBJ;

	public GameObject Grid;
	public GameObject CancelBuild;
	public GameObject TowerUI;
	public GameObject TowerPage;
	public GameObject TowerButtonOpen;
	public GameObject TowerButtonOpend;
	public GameObject MonsterUI;
	public GameObject PropOpen;
	public GameObject PropClose;
	public GameObject ChangeViewUI;
	public GameObject ChangeViewImg;
	public GameObject EnemyViewPosition;
	public GameObject SelfViewPosition;
	public bool clickTurretButton;
	private bool inEnemyView;
    private Vector3 touchPosWorld;
	private GameObject NowTouchNode;
	public bool isOffline;
	public bool isTutor;
	public bool RockMode;
	

	void Awake(){
		if(instance != null){
			Debug.LogError("More than one Buildermanager in scene!");
			return;
		}
		instance = this;
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
		if (Input.touchCount > 0) 
		{	
			if(turretToBuild == null)
				return;
			if(IsPointerOverUIObject())
				return;

			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
			if(hitInformation.collider != null)
			{

				if(hitInformation.collider.tag == "HostNode" | hitInformation.collider.tag == "ClientNode")
				{

					NowTouchNode = 	hitInformation.collider.gameObject;

					ifBuild();
				}
			}
		}
	}

	void ifBuild()
	{
		Touch touch = Input.GetTouch(0);

		if (touch.phase == TouchPhase.Ended)
		{		
			if(isOffline)
			{
				if(NowTouchNode.GetComponent<NodeOff>().turret != null)
					return;
				if(clickTurretButton)
					return;
				Debug.Log(clickTurretButton);
				NowTouchNode.GetComponent<NodeOff>().Build();
			}
			else
			{
				if(NowTouchNode.GetComponent<NODE>().turret != null)
					return;
				if(clickTurretButton)
					return;
				NowTouchNode.GetComponent<NODE>().Build();
			}
		}
	}
	
	public NodeUI nodeui;

	private Blueprint turretToBuild;
	private NODE selectednode;
	private NodeOff selectednodeoff;

	public bool CanBuild { get { return turretToBuild != null; } }

	public void ifJustClickButton()
	{
		clickTurretButton = true;
		StartCoroutine(TurnOff());
	}

	IEnumerator TurnOff()
	{
		yield return new WaitForSeconds(.1f);
		clickTurretButton = false;
	}

	public bool JustClickTurretButton()
	{
		return clickTurretButton;
	}

	public void BuildPreturret (NODE node){
		NowPreturrretOBJ = (GameObject) Instantiate (turretToBuild.Preturret,node.GetBuildPosition(),Quaternion.identity);
		if(NowPreturrretOBJ.tag == "preturret")
		{
			if(NowPreturrretOBJ.name == "Prepath(Clone)")
				return;
			Vector3 temp = NowPreturrretOBJ.transform.position;
			temp.y -= 3.1f;
			NowPreturrretOBJ.transform.position = temp;
		}
	}

	public void BuildPreturretoff (NodeOff node){
		NowPreturrretOBJ = (GameObject) Instantiate (turretToBuild.Preturret,node.GetBuildPosition(),Quaternion.identity);
		if(NowPreturrretOBJ.tag == "preturret")
		{
			if(NowPreturrretOBJ.name == "Prepath(Clone)")
				return;
			Vector3 temp = NowPreturrretOBJ.transform.position;
			temp.y -= 3.1f;
			NowPreturrretOBJ.transform.position = temp;
		}
	}

	public void DestroyPreturret (){
		Destroy(NowPreturrretOBJ);
	}

	public void SelectNode (NODE node) {
		selectednode = node;
		turretToBuild = null;
		nodeui.gameObject.SetActive(true);
		nodeui.SetTarget(node);
	}

	public void SelectNodeOffline (NodeOff node) {
		selectednodeoff = node;
		turretToBuild = null;

		nodeui.SetTargetoff(node);
	}

	public void OffNodeUI()
	{
		nodeui.gameObject.SetActive(false);
	}

	public void NodeUIBlock()
	{
		Debug.Log("ManagerBlock");
		nodeui.SellButtom.GetComponent<Image>().color = new Color32(75,75,75,255);
		nodeui.SellBlock.SetActive(true);
	}

	public void ResetNodeUIColor()
	{
		nodeui.SellButtom.GetComponent<Image>().color = new Color32(255,255,255,255);	
		nodeui.SellBlock.SetActive(false);	
	}

	public void SelectTurretToBuild (Blueprint Turret) {
		turretToBuild = Turret;
		EnterBuildMode();
		DisableCanvas();
	}

	void EnterBuildMode()
	{
		Grid.SetActive(true);
		CancelBuild.SetActive(true);
		TowerUI.SetActive(false);
		MonsterUI.SetActive(false);
		ChangeViewUI.SetActive(false);
		TowerPage.SetActive(false);
	}

	public void DisableCanvas () {
		selectednode = null;
		nodeui.Hide();
	}


	public void DisBuild(){
		turretToBuild = null;
		Destroy (GameObject.FindWithTag("preturret"));
		Grid.SetActive(false);
		CancelBuild.SetActive(false);
		TowerUI.SetActive(true);
//		TowerButtonOpend.SetActive(false);
		TowerButtonOpen.SetActive(true);
		MonsterUI.SetActive(true);
		ChangeViewUI.SetActive(true);
//		PropClose.SetActive(false);
//		PropOpen.SetActive(true);
//		Prop.GetComponent<Animator>().Play("PropClose");
	}




//	void OnMouseDown () {
//		DisableCanvas();
//		turretToBuild = null;
//		Selection.activeGameObject = null;
//	}

	public Blueprint GetTurretToBuild()
	{
		return turretToBuild;
	}

	public void ChangeView()
	{
		inEnemyView = !inEnemyView;
		if(inEnemyView)
		{
			ChangeViewImg.transform.position = EnemyViewPosition.transform.position;
			TowerUI.SetActive(false);
		}
		if(!inEnemyView)
		{
			TowerUI.SetActive(true);
			ChangeViewImg.transform.position = SelfViewPosition.transform.position;
		}
		
	}

}
