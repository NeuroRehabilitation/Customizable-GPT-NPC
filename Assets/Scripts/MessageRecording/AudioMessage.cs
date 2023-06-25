using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AudioMessage : MonoBehaviour
{
    public int id;

    public int idTotal;
    public StateController SC;
    public void Start() 
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        /*try
        {
            GameObject.Find("Chest").GetComponent<ChestScript>().chestPositionIterator++;
            GameObject.Find("Chest").GetComponent<ChestScript>().chestPositionIterator--;
            
            id = SC.msgClipNumber;
            idTotal=id;
        }
        catch (System.Exception)
        {
           print("in gallery");
        }*/
        
        //GameObject.Find("RecordMessageMenu").GetComponent<RecordVoice>().CurrentRecord = transform.gameObject;
    }

    public void changeID(int idT)
    {
        id =idT;
    }
    public void Play()
    {
         
        GetComponent<AudioSource>().Play();
    }

    public void DeleteItem()
    {
        
        string filePath = Application.persistentDataPath+"/MyGameSaveFolder/"+SC.Username+"/"+id+".wav";
        File.Delete( filePath );
        try
        {
            GameObject.Find("Chest").GetComponent<ChestScript>().chestPositionsCurr[id]=null;
        }
        catch (System.Exception)
        {
            
            print("gallery");
        }
        
        JSONUtils.UpdateJson();
        Destroy(gameObject);
    }
}
