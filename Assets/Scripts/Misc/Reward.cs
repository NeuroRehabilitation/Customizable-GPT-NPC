using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public StateController SC;
    public int milestoneID;

    void Start()
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        StartCoroutine(UpdateEverySecond());

    }

    // every second checks if all the mandatory milestones of that topic have been caught if so, set the gameobject active
    IEnumerator UpdateEverySecond()
    {
        while (true)
        {
            try
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(AllPrimaryMilestonesCaught());
                }
            }
            catch (System.Exception)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
    }


    public bool AllPrimaryMilestonesCaught()
    {
        foreach (JSONUtils.Milestone milestone in SC.milestonesTopics[milestoneID].milestonesUnchecked)
        {
            if (!milestone.secondary && !milestone.caught)
            {
                return false;
            }
        }
        return true;
    }

}
