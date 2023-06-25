using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasInstantiation : MonoBehaviour
{
    public GameObject Camera;
    public bool MobileVersion;
    public float TreeCameraRange,GrassCameraRange,ObjectsCameraRange;
    // Start is called before the first frame update
    void Start()
    {
        if (MobileVersion == false)
        {
            TreeCameraRange=100f;
            GrassCameraRange=70f;
            ObjectsCameraRange=35f;
        }
        else
        {
            TreeCameraRange=50f;
            GrassCameraRange=40f;
            ObjectsCameraRange=20f;
        }
        for (int i = 0; i<3; i++)
        {
            GameObject test;
            test = Instantiate(Camera);
            test.transform.SetParent(this.transform);
            if (i == 0)
            {
                
                test.GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "Trees");
                test.GetComponent<Camera>().farClipPlane = TreeCameraRange;
                
            }
            if (i == 1)
            {
                test.GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "Trees", "Grass");
                test.GetComponent<Camera>().farClipPlane = GrassCameraRange;
            }
            if (i == 2)
            {
                test.GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "Trees","Grass", "Objects");
                test.GetComponent<Camera>().farClipPlane = ObjectsCameraRange;
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
