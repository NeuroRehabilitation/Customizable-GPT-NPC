using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    // ghetto singleton for some reason 
    void Awake()
    {
        int numStateControllers = FindObjectsOfType<StateController>().Length;
        if (numStateControllers != 1)
        {
            Destroy(this.gameObject);
        }
        // if more then one State Controller is in the scene
        //destroy ourselves
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }


    public string currScene="Gallery";
    public List<bool> psycoFilters = new List<bool>();

    [System.Serializable]
    public class mod4MessageClass
    {
        public AudioClip speech;
        public string message;
    }

    [System.Serializable]
    public class mod4MessagesClass
    {
        public List<mod4MessageClass> mod4messages = new List<mod4MessageClass>();
    }

    [Header("Last Position in Exploration Scenes")]
    public float[][] lastPosition = new float[8][] 
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

    [Header("Player's username")]
    public string Username="test";

    [Header("Player's Milestones per topic")]
    public List<JSONUtils.MilestonesClass> milestonesTopics = new List<JSONUtils.MilestonesClass>();

    [Header("Extra/Easy Modules finished")]
    public List<bool> finishedModules = new List<bool>();

    [Header("Player's last chosen milestone ID")]
    public int milestoneCurrentTopic= 0;

    [Header("Map seed to generate")]
    public int mapSeed = 5;


    [Header("Color Pallette for each component type of all canvas'")]
    public string TitleHexValue;
    public string TextHexValue;
    public string ImageHexValue;
    public string ButtonImageHexValue;

    public AudioClip buttonSound;

    [Header("Number of Message Clips")]
    public int msgClipNumber = 0;

    [Header("Written messages on the chest")]
    public List<string> WritenMessages = new List<string>();

    [Header("Where to spawn player in this scene")]
    public Vector3 SpawnPos;

    [Header("Current Stage 0-4")]
    public int currStage=0;

    public List<mod4MessagesClass> mod4Topics = new List<mod4MessagesClass>();

    public void playButtonSound()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }
    
    //public List<MilestonesClass> milestonesTopics = new List<MilestonesClass>();

    
}
