using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantheonStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            GameObject.Find("StateController").GetComponent<StateController>().finishedModules[2]=true;
            JSONUtils.UpdateJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
