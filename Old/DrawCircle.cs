using UnityEditor;
 using UnityEngine; 
 
 public class DrawCircle : MonoBehaviour
 {
    public void OnMouseDown ()
    {
        OnDrawGizmosSelected();
    }


    public void OnDrawGizmosSelected()
     {
         #if UNITY_EDITOR
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position , transform.forward, 2f);
        #endif
     }
 }
