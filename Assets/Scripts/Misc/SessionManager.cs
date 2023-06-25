using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System;
using TMPro;

public class SessionManager : MonoBehaviour
{
    public Button startButton;
    public Button endButton;
    public TMP_InputField inputField;
    public GameObject ray;
    string inputString = "s";

    public class KeyPressEvent
    {
        public string key;
        public string time;

        public float lookPercent;

        public KeyPressEvent(string key, string time, float lookPercent)
        {
            this.key = key;
            this.time = time;
            this.lookPercent = lookPercent;
        }
    }
    public List<KeyPressEvent> keyPressEvents = new List<KeyPressEvent>();
    private void Update()
    {
        // Update the keyPressEvents array
        if (Input.anyKeyDown)
        {
            ray.GetComponent<raycastEyes>().timeHittingAgent=0.01f;
            ray.GetComponent<raycastEyes>().timeHittingOther=0.01f;
            string input = Input.inputString;
            keyPressEvents.Add(new KeyPressEvent(input, DateTime.Now+"", ray.GetComponent<raycastEyes>().lookingAtAgentPercentage));
        }
    }

    private void Start()
    {
        
        endButton.gameObject.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        
        inputString = inputField.text;
        startButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(true);
        
    }

    public void OnEndButtonClicked()
    {
        endButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        SaveData();
    }

    public void SaveData()
    {
        // Create a new JSON object
        var data = new {
            timeLookAgent = ray.GetComponent<raycastEyes>().timeHittingAgent,
            timeLookAway = ray.GetComponent<raycastEyes>().timeHittingOther,
            percentageLookAgent =ray.GetComponent<raycastEyes>().lookingAtAgentPercentage,
            keyPressEvents = keyPressEvents
        };

        // Convert the object to a JSON string
        string json = JsonConvert.SerializeObject(data);

        // Write the JSON string to a file
        File.WriteAllText(Application.dataPath + "/"+inputString+"_"+DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".json", json);
    
    }
}
