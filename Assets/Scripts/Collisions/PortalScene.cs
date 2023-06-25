using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScene : MonoBehaviour
{
    public GameObject ChooseMenu;

    public GameObject Door;

    void OnCollisionEnter(Collision collision)
    {
        //GameObject.Find("StateController").GetComponent<StateController>().unableToMove=true;
        ChooseMenu.SetActive(true);
        Collider ownCollider = GetComponent<Collider>();
    ownCollider.enabled = false;
    }
}
