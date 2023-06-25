using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class GalleryStarter : MonoBehaviour
{
    public StateController SC;
    public GameObject[] tables;
    public GameObject msgClip;

    public GameObject msgwTxt;
    public int foundRecordings = 0;
    public int lastI;
    void Start()
    {
        

        // if it's not the default debug username then it means the user has logged in
        if (GameObject.Find("StateController").GetComponent<StateController>().Username != "tests")
        {
            GameObject.Find("StateController").GetComponent<Logger>().logEvent("Gallery","user started gallery");
            JSONUtils.outputJSON( GameObject.Find("StateController").GetComponent<StateController>().Username); // everytime gallery starts, load saved data from users json file
            GameObject.Find("UserMenu").SetActive(false);
        }     
        SC = GameObject.Find("StateController").GetComponent<StateController>();

        // Stinky way of searching for messages recorded previously by the user
        if (SC.Username != "tests")
        {
            for (int i = 0; i < 20; i++)
            {
                StartCoroutine(GetAudioClip(Application.persistentDataPath+"/MyGameSaveFolder/"+GameObject.Find("StateController").GetComponent<StateController>().Username+"/"+(i)+".wav",i));
            }

           
        }
        
    }

    public void ManualStart()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Gallery","user started gallery");

        // if it's not the default debug username then it means the user has logged in
        if (GameObject.Find("StateController").GetComponent<StateController>().Username != "tests")
        {
            JSONUtils.outputJSON( GameObject.Find("StateController").GetComponent<StateController>().Username); // everytime gallery starts, load saved data from users json file
            GameObject.Find("UserMenu").SetActive(false);
        }     
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        print("oi");
        // Stinky way of searching for messages recorded previously by the user

            for (int i = 0; i < 20; i++)
            {
                StartCoroutine(GetAudioClip(Application.persistentDataPath+"/MyGameSaveFolder/"+GameObject.Find("StateController").GetComponent<StateController>().Username+"/"+(i)+".wav",i));
            }

        
    }

    IEnumerator GetAudioClip(string fullPath,int i)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fullPath, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {   
                // if a message recorded by the user with "i" id is found, instantiate cassete into a pre-defined position in the chest
                GameObject currentMsgClip = Instantiate (msgClip, tables[i].transform.position, Quaternion.Euler(new Vector3(0, -145.423f, 0)));
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                currentMsgClip.GetComponent<AudioSource>().clip = myClip;
                currentMsgClip.GetComponent<AudioMessage>().changeID(i);
                foundRecordings++;
                lastI = i;
            }
        }
        // on completing the search for audio recorded messages, get all text recorded messages and instantiate it into the pre-defined positions in the chest
        if (i == 19)
        {
            int localIterator=0;
            foreach (var msgTxt in SC.WritenMessages)
            {
                if(msgTxt != "null")
                {
                    GameObject currentMsgTxt= Instantiate (msgwTxt, tables[localIterator].transform.position, Quaternion.Euler(new Vector3(0, -145.423f, 0)));
                    currentMsgTxt.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SC.WritenMessages[localIterator];
                    currentMsgTxt.GetComponent<MessageScript>().id = localIterator;
                }
                lastI++;
                
                
                localIterator++;
            }
        }
    }
}
