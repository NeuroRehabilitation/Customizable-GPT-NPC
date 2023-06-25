using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milestoneTrigger : MonoBehaviour
{
    public GameObject TB;
    public GameObject Collider;
    public GameObject Dependency;
    public bool Activated = false;
    public bool first = false;

	void Start ()
    {
        TB.SetActive(false);
        Collider = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log( Dependency);
        if(other.gameObject.name =="XR Origin"  && (Dependency == null || Dependency.GetComponent<milestoneTrigger>().Activated))
        {
            this.TB.SetActive(true);
            Activated=true;
        }    
    }
}
