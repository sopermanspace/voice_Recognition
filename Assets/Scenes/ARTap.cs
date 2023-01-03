using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTap : MonoBehaviour
{
    public GameObject Cursor;
    public GameObject Spawned ;

   [SerializeField] private ARRaycastManager RaycastManager;
   List<ARRaycastHit> Hits = new List<ARRaycastHit>();

 

private void Awake() {
 Cursor.SetActive(true);
}  

    void Update()
    {
          if (Input.touchCount == 0)
        return;

    if (RaycastManager.Raycast(Input.GetTouch(0).position, Hits))
    {
        // Only returns true if there is at least one hit
    }

    }
}
