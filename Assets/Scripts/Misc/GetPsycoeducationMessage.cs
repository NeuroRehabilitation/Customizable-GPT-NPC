using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GetPsycoeducationMessage : MonoBehaviour
{
    public int id=0;
    public StateController SC;
     public void Start() 
    {
        /*print("open");
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        GetComponent<TMP_Text>().text=SC.psycoEducationMessages[SC.milestoneCurrentTopic].psycoEducationTopic[id];
        GameObject.Find("CanvasPlayer").transform.Find("MilestoneScroll1").GetComponent<AudioSource>().clip=SC.psycoEducationAudios[SC.milestoneCurrentTopic].psycoEducationTopic[id];
        GameObject.Find("CanvasPlayer").transform.Find("MilestoneScroll1").GetComponent<AudioSource>().Play(0);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
