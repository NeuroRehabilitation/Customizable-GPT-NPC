using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemManipulator : MonoBehaviour
{
    public GameObject Player;
    public void IncreaseTreeSize()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Garden"," Increase garden item size"); 
        transform.parent.transform.localScale += new Vector3(transform.parent.transform.localScale.x*.2f,transform.parent.transform.localScale.y*.2f,transform.parent.transform.localScale.z*.2f);
    }

    public void DecreaseTreeSize()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Garden","Decrease garden item size"); 
       transform.parent.transform.localScale -= new Vector3(transform.parent.transform.localScale.x*.2f,transform.parent.transform.localScale.y*.2f,transform.parent.transform.localScale.z*.2f);
    }
    public void DeleteItem()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Garden","Delete garden item");
        Destroy(transform.parent.transform.parent.gameObject);
    }

    public void RotateObject()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Garden","Rotate garden item by 10 degrees"); 
        transform.parent.transform.Rotate(0, 10, 0);
    }


    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);
        //transform.LookAt(Player.transform);
    }
    public void Start() 
    {
        
       Player = GameObject.Find("XR Origin");
    }
}
