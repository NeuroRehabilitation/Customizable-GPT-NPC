using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RecordVoice : MonoBehaviour
{
    public GameObject message;
    public AudioClip msgVoice;

    
    public StateController SC;
    public ChestScript CS;
    AudioSource audioSource ;

    public GameObject StartRecordButton;
    public GameObject StopRecordButton;
    public GameObject RecordButton;

    public GameObject RedDot;

    

    // the mic you want to use should be set as default on windows
    void Start()
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        CS = GameObject.Find("Chest").GetComponent<ChestScript>();

        print("Microphones: ");
        foreach (var item in Microphone.devices)
        {
            print (item);
        }
        
    }

    // record button clicked start recording for 10m max
    public void Record()
    {
        RedDot.SetActive(true);
         
        msgVoice = Microphone.Start(Microphone.devices[0], false, 600, 44100);
    }

    

    // stop recording and save the audio file
    public void StopRecord()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Chest","user recorded message"); 
        RedDot.SetActive(false);
         
        Microphone.End(Microphone.devices[0]);

        int firstEmptyIndex = GetFirstEmptyPositionIndex();
        var temp = Instantiate(message, CS.chestPositions[firstEmptyIndex].transform.position, Quaternion.Euler(new Vector3(0, 125.05f, 0)));
        audioSource= temp.GetComponent<AudioSource>();
        temp.GetComponent<AudioMessage>().id=firstEmptyIndex;
        CS.chestPositionsCurr[firstEmptyIndex]=temp;
        StartCoroutine(Save(1,firstEmptyIndex));
        
    }
    
     // determine which next position in the chest is empty
    private int GetFirstEmptyPositionIndex()
    {
        for (int i = 0; i <  CS.chestPositionsCurr.Count; i++)
        {
            if ( CS.chestPositionsCurr[i] == null ||  CS.chestPositionsCurr[i].Equals(null))
            {
                return i;
            }
        }

        return -1; // Return -1 if no empty position is found
    }

    // save the audio file to the persistent data path and save the path to the json file
    IEnumerator Save(int secs, int id)
    {
        yield return new WaitForSeconds(secs);
        SavWav.Save("MyGameSaveFolder/"+SC.Username+"/"+id+"", msgVoice);
        StartCoroutine(GetAudioClip2(Application.persistentDataPath+"/MyGameSaveFolder/"+SC.Username+"/"+id+".wav"));

        JSONUtils.myPlayer.msgClipNumber=SC.msgClipNumber;
        JSONUtils.UpdateJson();
    }

    IEnumerator GetAudioClip2(string fullPath)
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
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
            }
        }
    }
}
