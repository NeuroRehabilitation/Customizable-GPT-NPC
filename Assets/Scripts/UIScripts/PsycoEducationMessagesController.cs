using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsycoEducationMessagesController : MonoBehaviour
{
    public GameObject[] msgs;
    public GameObject[] tokensGrabbable;

    void OnEnable()
    {
        tokensGrabbable[GameObject.Find("StateController").GetComponent<StateController>().milestoneCurrentTopic].SetActive(true);
    }

    public void closeMsgs()
    {
        GameObject parentObj = GameObject.Find("Camera Offset");
        if (parentObj != null) 
        {
            GameObject leftHandController = parentObj.transform.Find("LeftHand Controller").gameObject;
            if (leftHandController != null) {
                leftHandController.SetActive(true);
            }
        }

         foreach (GameObject msg in msgs)
         {
            try
            {
            msg.SetActive(false);
                
            }
            catch (System.Exception)
            {
            }
         }
         tokensGrabbable[GameObject.Find("StateController").GetComponent<StateController>().milestoneCurrentTopic].SetActive(false);
    }

    public void repeatMsg()
    {
        for (int i = 0; i <8; i++)
        {
            try
            {
                GameObject.Find("MilestoneScroll"+(i+1)).GetComponent<AudioSource>().enabled = false;
                GameObject.Find("MilestoneScroll"+(i+1)).GetComponent<AudioSource>().enabled = true;
            }
            catch (System.Exception e)
            {
                 
            }
        }
        
    }
}
