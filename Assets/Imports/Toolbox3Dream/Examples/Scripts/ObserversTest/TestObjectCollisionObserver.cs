using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox3Dream;
using System.IO;
using System;
using TMPro;
/// <summary>
/// Test script to show how a sphere reacts when the trigger collision called "InsideBox".
/// gameObject changes its scale beween 1 and 2 according the received IntensityLevel of the emotion.
/// </summary>
public class TestObjectCollisionObserver : MonoBehaviour {
    public GameObject Dependency;

    public StateController SC;
    public int milestoneID;

    public GameObject Dependender;

    void Start ()
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
    }

    // if player collides with this object, and it is a valid one (has no dependency or dependency is already caught), then show the message and some other stuff
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="XR Origin" && ( milestoneID == 0 || Dependency== null || SC.milestonesTopics[SC.milestoneCurrentTopic].milestonesUnchecked[Dependency.GetComponent<TestObjectCollisionObserver>().milestoneID].caught))
        {
            // make user unable to move
            try
            {
                GameObject.Find("LeftHand Controller").SetActive(false);
                GameObject.Find("ReticleTeleport(Clone)").SetActive(false);
            }
            catch (System.Exception)
            {
                
            }
            
            SC.milestonesTopics[SC.milestoneCurrentTopic].milestonesUnchecked[milestoneID].caught=true;

            // show message card and play audio
            GameObject.Find("CanvasPlayer").transform.Find("MilestoneScroll1").gameObject.transform.Find("Text (TMP)").gameObject.GetComponent<GetPsycoeducationMessage>().id = milestoneID;
            GameObject.Find("CanvasPlayer").transform.Find("MilestoneScroll1").gameObject.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>().text= SC.milestonesTopics[SC.milestoneCurrentTopic].milestonesUnchecked[milestoneID].message;
            GameObject.Find("CanvasPlayer").transform.Find("MilestoneScroll1").gameObject.GetComponent<AudioSource>().clip= Resources.Load<AudioClip>(SC.milestonesTopics[SC.milestoneCurrentTopic].milestonesUnchecked[milestoneID].clipName);
            GameObject.Find("CanvasPlayer").transform.Find("MilestoneScroll1").gameObject.SetActive(true);
            GameObject.Find("CanvasPlayer").transform.Find("Continue Button").gameObject.SetActive(true);     
            GameObject.Find("ObjectCaughtAnimated "+(SC.milestoneCurrentTopic+1)).SetActive(true);

            // Because this one was caught, we can now activate the next milestone, unless it is the last one or is a secondary milestone (that dont have dependers)
            try
            {
                Dependender.transform.GetChild(0).gameObject.SetActive(true);
            }
            catch (System.Exception)
            {
                print("secondary");
            }
            
            // update json data
            JSONUtils.myPlayer.milestonesTopics = SC.milestonesTopics;
            StateController stateController = GameObject.Find("StateController").GetComponent<StateController>();
            Vector3 xrOriginPosition = GameObject.Find("XR Origin").transform.position;
            stateController.lastPosition[stateController.milestoneCurrentTopic][0] = xrOriginPosition.x;
            stateController.lastPosition[stateController.milestoneCurrentTopic][1] = xrOriginPosition.y;
            stateController.lastPosition[stateController.milestoneCurrentTopic][2] = xrOriginPosition.z;
            JSONUtils.myPlayer.position = stateController.lastPosition;
            JSONUtils.UpdateJson();

            GameObject.Find("StateController").GetComponent<Logger>().logEvent("Exploration","Caught Object "+SC.milestoneCurrentTopic + " message id "+milestoneID);
            gameObject.SetActive(false);
        }
            
    }
}
