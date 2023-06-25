using System.Collections;
using UnityEngine;

public class TreePlanter : MonoBehaviour
{
    public GameObject[] trees;

    // sends data to ControllerReticleHitPosition to spawn the selected object
    public void SpawnTree(int id)
    {
        GameObject.Find("RightHand Controller").GetComponent<ControllerReticleHitPosition>().currentItem = trees[id];
        GameObject.Find("RightHand Controller").GetComponent<ControllerReticleHitPosition>().deploy = true;
        GameObject.Find("RightHand Controller").GetComponent<ControllerReticleHitPosition>().timer = 3.5f;
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Garden","user placed garden item " + id);
    }

    // old functionality to use mouse clicks to drag objects
    public void Update()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);
        /*Reticle.transform.position = Scout.GetComponent<ScoutTerrain>().hitPos;

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        Ray castPoint2 = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit2;

        Ray castPoint3 = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit3;

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                if (Physics.Raycast(castPoint2, out hit2, Mathf.Infinity, ~IgnoreMe))
                {
                    
                    if (hit.transform.tag == "grass")
                    {
                        draggingObject = true;
                        currDraggedObject = hit.transform.gameObject;
                        if (Physics.Raycast(castPoint3, out hit3, Mathf.Infinity, ~IgnoreMe2))
                        {
                            draggingObject = false;
                        }
                    }
                        
                    if (draggingObject == true)
                        currDraggedObject.transform.position = new Vector3 (hit2.point.x,hit2.point.y+5.0f,hit2.point.z);
                    //objectToMove.transform.position = hit.point;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            draggingObject = false;
        }*/
    }

}
