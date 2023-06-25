using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PsycoMessage : MonoBehaviour
{
    public int topic = 0;// your topic value;
    public int id = 0 ;// your id value;
    public StateController SC;
     public void Start() 
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        textMeshProUI.text = SC.milestonesTopics[topic].milestonesUnchecked[id].message;
        
    }
    public void next() 
    {
        id++;
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        try
        {
            textMeshProUI.text = SC.milestonesTopics[topic].milestonesUnchecked[id].message;
        }
        catch (System.Exception)
        {
            id=-1;
            next() ;
        }
        
    }

    public void prev() 
    {
        id--;
        TMP_Text textMeshProUI = GetComponent<TMP_Text>();
        try
        {
            textMeshProUI.text = SC.milestonesTopics[topic].milestonesUnchecked[id].message;
        }
        catch (System.Exception)
        {
            id=SC.milestonesTopics[topic].milestonesUnchecked.Count;
            prev() ;
        }
        
    }
}
