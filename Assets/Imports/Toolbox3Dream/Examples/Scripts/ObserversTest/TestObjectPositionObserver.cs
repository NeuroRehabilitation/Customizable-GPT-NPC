using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox3Dream;

/// <summary>
/// Test script to show how a sphere reacts when the trigger collision called "InsideBox".
/// gameObject changes its scale beween 1 and 2 according the received IntensityLevel of the emotion.
/// </summary>
public class TestObjectPositionObserver : MonoBehaviour, ITriggered3Dream {

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cubo triggered.......");
    }

    public void OnTrigger3Dream(DataTrigger data)
    {
        if(data.TriggerId == "Passou")
        {
            Debug.Log( gameObject.name + " notified of Trigger name: " + data.TriggerId + "\nValue: " + data.InterpolationValue);
        }
    }
    
}
