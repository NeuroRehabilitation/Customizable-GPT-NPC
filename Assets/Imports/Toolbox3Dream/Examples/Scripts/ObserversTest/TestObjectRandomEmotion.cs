using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox3Dream;

/// <summary>
/// Test script to show how a sphere reacts to Trigger3Dream called "RandomEmotion"
/// Change the color according the EmotionType received and change scale between 1 and 5 depending on its IntensityLevel.
/// </summary>
public class TestObjectRandomEmotion : MonoBehaviour, ITriggered3Dream {

    private Vector3 newScale;
    private Color color = Color.white;
    private Material objMaterial;

	// Use this for initialization
	void Start () {
        newScale = Vector3.one;
        objMaterial = gameObject.GetComponent<Renderer>().material;

        UpdateObjectProperties();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateObjectProperties()
    {
        gameObject.transform.localScale = newScale;
        objMaterial.color = color;
    }

    public void OnTrigger3Dream(DataTrigger data)
    {
        if(data.TriggerId == "RandomEmotion")
        {
            Debug.Log(gameObject.name + " notified of Trigger -> " + data.ToString());

            switch (data.EmotionType)
            {
                case EmotionalType.Anger:
                    color = Color.red;
                    break;
                case EmotionalType.Happiness:
                    color = Color.green;
                    break;
                case EmotionalType.Sadness:
                    color = Color.gray;
                    break;
                case EmotionalType.Fear:
                    color = Color.black;
                    break;
            }

            newScale = Vector3.Lerp(Vector3.one, Vector3.one * 5.0f, data.InterpolationValue);
            UpdateObjectProperties();
        }
    }
    
}
