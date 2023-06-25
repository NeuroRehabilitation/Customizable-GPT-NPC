using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene3 : MonoBehaviour
{
    public GameObject ChooseMenu; 

    void OnCollisionEnter(Collision collision)
    {
        //Door.SetActive(false);
        ChooseMenu.SetActive(true);
    }
}
