using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level2Starter : MonoBehaviour
{
    public GameObject msgClip;
    public StateController SC;
    // Start is called before the first frame update
    void Start()
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        /*for (int i = 0; i < SC.msgClipNumber; i++)
        {
            StartCoroutine(GetAudioClip2(Application.persistentDataPath+"/MyGameSaveFolder/"+GameObject.Find("StateController").GetComponent<StateController>().Username+"/"+i+".wav"));
        }*/
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
                GameObject currentMsgClip = Instantiate (msgClip, new Vector3(10, 10, 85), Quaternion.identity);
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                currentMsgClip.GetComponent<AudioSource>().clip = myClip;
                //currentMsgClip.GetComponent<AudioSource>().Play();
            }
        }
    }
}
