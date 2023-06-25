using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class JSONUtils : MonoBehaviour
{
    // Player class containing all usefull data saved, will later be loaded into StateController
    [System.Serializable]
    public class Player 
    {
        public List<bool> psycoFilters = new List<bool>();

        // current mapSeed int used for the pseudo random generation
        public int mapSeed = 124;

        // username
        public string name = "tests";

        // numebr of recordings so far
        public int msgClipNumber = 0;

        // last saved position
        public float[][] position = new float[8][] 
        {
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f}
        };

        // all the unchecked milestons of this user
        public List<MilestonesClass> milestonesTopics = new List<MilestonesClass>();

        public List<bool> finishedModules = new List<bool>();


        // all writen messages from this user
        public List<string> WritenMessages = new List<string>();
    }

    public static Player myPlayer = new Player();

    [System.Serializable]
    public class MilestonesClass
    {
        public string prefabName;
        public List<Milestone> milestonesUnchecked = new List<Milestone>();
    }

    [System.Serializable]
    public class Milestone
    {
        public string message;
        public bool placed;
        public bool caught;

        public bool secondary;

        public string clipName;
    }

    // called on login in, or whenever we need to update the data from the json file to the StateController
    public static void outputJSON(string username)
    {
        var SC = GameObject.Find("StateController").GetComponent<StateController>();
        var SCMilestoneCurrentTopic = SC.milestoneCurrentTopic;
        
        // if user exists load myPlayer class with all the contents found in the users saved file
        try 
        {
            string SavedTextsCompleteFilePath="";
            #if !UNITY_EDITOR
                SavedTextsCompleteFilePath = Application.persistentDataPath;
            #elif UNITY_EDITOR
                    SavedTextsCompleteFilePath = "Assets/Resources";
            #endif
            //Debug.LogError("here0 "+SavedTextsCompleteFilePath+"/MyGameSaveFolder/"+username+".txt");
             
            myPlayer =JsonConvert.DeserializeObject<Player>(System.IO.File.ReadAllText(SavedTextsCompleteFilePath+"/MyGameSaveFolder/"+username+".txt"));
            PopulateStateController();
            Debug.LogError("Existing user Saved "+SavedTextsCompleteFilePath);

            SC.mapSeed = myPlayer.mapSeed;
            SC.msgClipNumber = myPlayer.msgClipNumber;
            SC.WritenMessages = myPlayer.WritenMessages;
            SC.psycoFilters = myPlayer.psycoFilters;
            SC.finishedModules = myPlayer.finishedModules;
        }
        // if user doesnt exist, then it's a new account, so we must initiallize all the elements of the Player class and create a file
        catch (Exception e) 
        {
            print(e);
            
            myPlayer.name = username;
            
            string strOutput = JsonConvert.SerializeObject(myPlayer);
            string SavedTextsCompleteFilePath="";
            #if !UNITY_EDITOR
                SavedTextsCompleteFilePath = Application.persistentDataPath;
            #elif UNITY_EDITOR
                    SavedTextsCompleteFilePath = "Assets/Resources";
            #endif
                    // set the base file path, then add the directory if it's not there yet
                    SavedTextsCompleteFilePath = MakeFolder(SavedTextsCompleteFilePath, "/MyGameSaveFolder");
            File.WriteAllText(Path.Combine(SavedTextsCompleteFilePath, username+".txt"), strOutput, System.Text.Encoding.UTF8);
            Debug.LogError("New user Saved "+SavedTextsCompleteFilePath);
        } 
            
    }

    

    // whenever we need to save to the json file the current data from the StateController
    public static void UpdateJson()
    {
        JSONUtils.myPlayer.milestonesTopics = GameObject.Find("StateController").GetComponent<StateController>().milestonesTopics;
        JSONUtils.myPlayer.WritenMessages = GameObject.Find("StateController").GetComponent<StateController>().WritenMessages;
        JSONUtils.myPlayer.finishedModules = GameObject.Find("StateController").GetComponent<StateController>().finishedModules;
        foreach (var topic in JSONUtils.myPlayer.milestonesTopics)
        {
            foreach (var uncheckedMilestone in topic.milestonesUnchecked)
            {
                uncheckedMilestone.placed = false;
            }
        }
        string strOutput = JsonConvert.SerializeObject(myPlayer);
        string SavedTextsCompleteFilePath="";
        #if !UNITY_EDITOR
            SavedTextsCompleteFilePath = Application.persistentDataPath;
        #elif UNITY_EDITOR
                SavedTextsCompleteFilePath = "Assets/Resources";
        #endif
                // set the base file path, then add the directory if it's not there yet
                SavedTextsCompleteFilePath = MakeFolder(SavedTextsCompleteFilePath, "/MyGameSaveFolder");
                MakeFolder(SavedTextsCompleteFilePath, "/MyGameSaveFolder/"+myPlayer.name);
         //Debug.LogError("here31 "+Path.Combine(SavedTextsCompleteFilePath, myPlayer.name+".txt"));           
        File.WriteAllText(Path.Combine(SavedTextsCompleteFilePath, myPlayer.name+".txt"), strOutput, System.Text.Encoding.UTF8);
        Debug.LogError("Updated  "+SavedTextsCompleteFilePath);
        try
        {
            outputJSON(GameObject.Find("StateController").GetComponent<StateController>().Username);
        }
        catch (System.Exception)
        {
            
            print("failed outputJSON, updated tho");
        }
        
    }

    public static void PopulateStateController()
    {

            var SC = GameObject.Find("StateController").GetComponent<StateController>();
            SC.milestonesTopics.Clear();
            SC.lastPosition = myPlayer.position;
            SC.WritenMessages = myPlayer.WritenMessages;
            SC.milestonesTopics = JSONUtils.myPlayer.milestonesTopics;
    }

    private static string MakeFolder(string path, string savedTextsFolder)
    {
        string saveDirectory = path + savedTextsFolder;
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Debug.Log("directory created! at: " + path);
        }
        return saveDirectory;
    }

}
