using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;

public class Logger : MonoBehaviour
{
    private DateTime appStartTime;
    private int sessionID = 0;
    private bool hasLoggedCurrentSession = false;
    private Dictionary<string, DateTime> moduleStartTimes = new Dictionary<string, DateTime>();

    public class loggableEvent
    {
        public string module;
        public DateTime moduleStartTime;
        public DateTime totalStartTime;
        public float timeElapsedInSeconds;
        public string action;

        public loggableEvent(string module, DateTime moduleStartTime, DateTime totalStartTime, float timeElapsedInSeconds, string action)
        {
            this.module = module;
            this.moduleStartTime = moduleStartTime;
            this.totalStartTime = totalStartTime;
            this.timeElapsedInSeconds = timeElapsedInSeconds;
            this.action = action;
        }
    }

    private void Start()
    {
        appStartTime = DateTime.Now;
    }

    public void logEvent(string module, string action)
{
    try
    {
        float elapsedSeconds = (float)(DateTime.Now - appStartTime).TotalSeconds;
        DateTime moduleStartTime;

        if (!moduleStartTimes.ContainsKey(module))
        {
            moduleStartTime = DateTime.Now;
            moduleStartTimes.Add(module, moduleStartTime);
        }
        else
        {
            moduleStartTime = moduleStartTimes[module];
        }

        loggableEvent newEvent = new loggableEvent(module, moduleStartTime, appStartTime, elapsedSeconds, action);

        string username = transform.GetComponent<StateController>().Username;
        string sessionPath = Path.Combine(Application.persistentDataPath, username, "sessions");

        if (!hasLoggedCurrentSession)
        {
            if (!Directory.Exists(sessionPath))
            {
                Directory.CreateDirectory(sessionPath);
            }
            print(Directory.GetDirectories(sessionPath).Length + " in " + sessionPath);
            sessionID = Directory.GetDirectories(sessionPath).Length + 1;
            hasLoggedCurrentSession = true;
        }

        string path = Path.Combine(sessionPath, sessionID.ToString());
        Directory.CreateDirectory(path); // This ensures that the directory exists
        Debug.Log("Directory path: " + path);

        // The filename now includes the start time of the module, in a year-month-day-hour-minute-second format
        string filename = $"{module}_{moduleStartTime:yyyyMMdd_HHmmss}.json";
        string fullPath = Path.Combine(path, filename);

        List<loggableEvent> events;

        if (File.Exists(fullPath))
        {
            string existingData = File.ReadAllText(fullPath);
            events = JsonConvert.DeserializeObject<List<loggableEvent>>(existingData);
        }
        else
        {
            events = new List<loggableEvent>();
        }

        events.Add(newEvent);

        string json = JsonConvert.SerializeObject(events, Formatting.Indented);
        File.WriteAllText(fullPath, json);

        // If the action is "exiting", remove the module start time from the dictionary
        if (action == "exiting")
        {
            moduleStartTimes.Remove(module);
        }
    }
    catch (System.Exception)
    {
        Debug.LogError("Error writing logs");
    }
}



}
