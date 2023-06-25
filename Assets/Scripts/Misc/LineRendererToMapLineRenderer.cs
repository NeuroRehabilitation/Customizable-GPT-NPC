using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererToMapLineRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer parentLR;
    LineRenderer LR;
    void Start()
    {
        parentLR = transform.parent.GetComponent<LineRenderer>();
        LR = GetComponent<LineRenderer>();
        StartCoroutine(Wait(30));
    }
     IEnumerator Wait(int secs)
    {
        yield return new WaitForSeconds(secs);
        
        for (int i=0; i<parentLR.positionCount; i++)
        {   
            LR.positionCount++;
             
            LR.SetPosition(i,new Vector3(parentLR.GetPosition(i)[0],parentLR.GetPosition(i)[1]+50.0f,parentLR.GetPosition(i)[2]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
