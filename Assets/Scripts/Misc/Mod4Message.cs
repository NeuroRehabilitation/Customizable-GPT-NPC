using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Mod4Message : MonoBehaviour
{
    public int topic = 0;// your topic value;
    public int id = 0 ;// your id value;
    public StateController SC;
    public AudioSource AS;
     public void Start() 
    {
        AS =GetComponent<AudioSource>();
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        textMeshProUI.text = SC.mod4Topics[topic].mod4messages[id].message;
        
        AS.PlayOneShot(SC.mod4Topics[topic].mod4messages[id].speech);
        
    }
    public void next() 
    {
         AS.Stop();
        id++;
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        try
        {
            textMeshProUI.text = SC.mod4Topics[topic].mod4messages[id].message;
        AS.PlayOneShot(SC.mod4Topics[topic].mod4messages[id].speech);
        }
        catch (System.Exception)
        {
            id=-1;
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
        AS.PlayOneShot(SC.mod4Topics[topic].mod4messages[id].speech);
        }
        catch (System.Exception)
        {
            id=SC.mod4Topics[topic].mod4messages.Count;
            prev() ;
        }
        
    }
}
