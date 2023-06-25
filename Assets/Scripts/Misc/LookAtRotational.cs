using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtRotational : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 lookAtRotation = Quaternion.LookRotation(GameObject.Find("Destination").transform.position - transform.position).eulerAngles;
        transform.rotation = Quaternion.Euler(Vector3.Scale(lookAtRotation, new Vector3(0,1,0)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
