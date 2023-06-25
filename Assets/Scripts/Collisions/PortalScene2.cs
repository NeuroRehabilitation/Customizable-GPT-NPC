using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScene2 : MonoBehaviour
{
    public GameObject ChooseMenu; 

    void OnCollisionEnter(Collision collision)
    {

        //Door.SetActive(false);
        ChooseMenu.SetActive(true);
    }
}
