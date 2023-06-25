using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeboStart : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("StateController").GetComponent<StateController>().finishedModules[0]=true;
        JSONUtils.UpdateJson();
    }
}
