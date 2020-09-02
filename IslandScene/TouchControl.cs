using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour {
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

	private Camera camera;

    private bool wasZoomingLastFrame; // Touch mode only
	
    private Vector2[] lastZoomPositions; // Touch mode only
    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only
    private static readonly float PanSpeed = 60f;
    private static readonly float[] BoundsX = new float[]{-32.7f, 40f};
    private static readonly float[] BoundsY = new float[]{-5.8f, -5.8f};
    private static readonly float[] BoundsZ = new float[]{-18f, -4f};

    public GameObject Top;
    public GameObject Bottom;
    public static bool MovingCamera;
    public BuildingManager buildingmanager;

    private Vector3 touchPosWorld;

	void Start()
	{
		camera = GetComponent<Camera>();
	}

    void Update()
    {
	//	if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
        if(Input.touchCount == 1)
        {
            HandleTouch();
        }
        // If there are two touches on the device...
        else if (Input.touchCount == 2)
        {
            MovingCamera = true;
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {  
                Debug.Log(Top.transform.position.y);
                Debug.Log(Bottom.transform.position.y);

           //     Debug.Log(camera.transform.InverseTransformPoint(Top.transform.position).y);
                if((Top.transform.position).y > 50.5)
                    if(deltaMagnitudeDiff > 0)
                    {
                        Vector3 temp3 = transform.position;
                        temp3.y -= 2;
                        transform.position = temp3;
                    }
                if((Bottom.transform.position).y < -86)
                    if(deltaMagnitudeDiff > 0)
                    {
                        Vector3 temp3 = transform.position;
                        temp3.y += 2;
                        transform.position = temp3;
                    }
           //          return;
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;


                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
                


                if(camera.orthographicSize > 69f)
                    camera.orthographicSize = 69f;
                if(camera.orthographicSize <10f)
                    camera.orthographicSize = 10f;      
        //        Debug.Log(camera.orthographicSize);         
                
                Vector3 temp = Bottom.transform.position;
                Debug.Log(temp.y);
                Vector3 tempc = new Vector3 (0,camera.orthographicSize,0);
                temp.y = -camera.transform.InverseTransformPoint(tempc).y;
                Debug.Log(temp.y);
                Bottom.transform.position = (temp);

                Vector3 temp2 =(Top.transform.position);
                Debug.Log(camera.transform.InverseTransformPoint(temp2).y);
           //     Vector3 tempc2 = new Vector3 (0,camera.orthographicSize,0);
          //      temp2.y = camera.transform.InverseTransformPoint(tempc2).y ;
                temp2.y = camera.orthographicSize;
                Debug.Log(camera.transform.InverseTransformPoint(temp2).y);
                Top.transform.position = camera.transform.TransformPoint(temp2);
      //          Debug.Log(camera.transform.InverseTransformPoint(Top.transform.position).y);
      //          Debug.Log(Bottom.transform.position.y);
     //           Debug.Log(temp.y);
     //           Debug.Log(temp2.y);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
        else
        {
            MovingCamera = false;
        }
    }

	private bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
	}
	
    void HandleTouch() {
                switch(Input.touchCount) {
    
        case 1: // Panning
            wasZoomingLastFrame = false;
            // If the touch began, capture its position and its finger ID.
            // Otherwise, if the finger ID of the touch doesn't match, skip it.
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                lastPanPosition = touch.position;
                panFingerId = touch.fingerId;
            } else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) {
                if(IsPointerOverUIObject())
                    return;
//                Debug.Log(BuildingManager.isEditMode);
                if(BuildingManager.isBuildingMode | BuildingManager.isEditMode)
                {
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
//                    Debug.Log(hitInformation.collider.name);
                    if(hitInformation.collider == null)
                        return;
                    if(buildingmanager.GetPreBuilding() != null)
                    {
                        if(GameObject.Find("MovingManager").GetComponent<MovingBuilding>().isMoving)
                            return;
                    }
                    if(hitInformation.collider.tag == "BuildNode" & !BuildingManager.isBuildingMode)
                       PanCamera(touch.position);
                    if(hitInformation.collider.name == "Obstacle")
                        PanCamera(touch.position);
                    return;
                }

                PanCamera(touch.position);
            }
            break;
            
        default: 
            wasZoomingLastFrame = false;
            break;
        }
	}
	void PanCamera(Vector3 newPanPosition) {
        
                MovingCamera = true;
        // Determine how much to move the camera
        Vector3 offset = camera.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed, 0);
        // Perform the movement
        transform.Translate(move, Space.World);  
        
 //       Debug.Log(camera.orthographicSize);
        // Ensure the camera remains within bounds.
        BoundsY[0] = -(69 - camera.orthographicSize) - 16.9f;
        BoundsY[1] = (69 - camera.orthographicSize) - 16.9f;

    //    Debug.Log("BountsY[0]" + BoundsY[0]);
   //     Debug.Log("BountsY[1]" + BoundsY[1]);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.y = Mathf.Clamp(transform.position.y, BoundsY[0], BoundsY[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;
    
        // Cache the position
        lastPanPosition = newPanPosition;
    }
}
