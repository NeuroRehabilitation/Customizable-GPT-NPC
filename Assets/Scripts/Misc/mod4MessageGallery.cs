using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class mod4MessageGallery : MonoBehaviour
{
    public int topic = 0;// your topic value;
    public int id = 0 ;// your id value;
    public StateController SC;
    public AudioSource AS;
     public void Start() 
    {
        AS = GetComponent<AudioSource>();
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        textMeshProUI.text = SC.mod4Topics[topic].mod4messages[id].message;
        
        //AS.PlayOneShot(SC.mod4Topics[topic].mod4messages[id].speech);
        
    }
    public void next() 
    {
        AS.Stop();
        id++;
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        try
        {   
            textMeshProUI.text = SC.mod4Topics[topic].mod4messages[id].message;
            //AS.PlayOneShot(SC.mod4Topics[topic].mod4messages[id].speech);
        }
        catch (System.Exception)
        {
            topic++;
            id=-1;
            if(topic>=SC.mod4Topics.Count)
            {
                topic=0;
            }
            next() ;
        }
        
    }

    public void prev() 
    {
         AS.Stop();
        id--;
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        try
        {
            textMeshProUI.text = SC.mod4Topics[topic].mod4messages[id].message;
            //AS.PlayOneShot(SC.mod4Topics[topic].mod4messages[id].speech);
        }
        catch (System.Exception)
        {
            
            topic--;
            if(topic==-1)
            {
                topic=SC.mod4Topics.Count-1;
            }
            id=SC.mod4Topics[topic].mod4messages.Count-1;
            prev() ;
        }
        
    }
}
