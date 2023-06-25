using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelection : MonoBehaviour
{
    public int currAgent=0;
    public GameObject humanAgentC;
    public GameObject humanAgentNC;
    public GameObject narratorAgent;
    public GameObject narrator;
    public GameObject chair;
    public GameObject portal;
    // Start is called before the first frame update
    public void SelectAgent(int agent)
    {
        currAgent=agent;
        if (currAgent == 0)
        {
            humanAgentNC.SetActive(true);
            humanAgentC.SetActive(false);
            narrator.SetActive(true);
            narratorAgent.SetActive(false);
            chair.SetActive(true);
            //portal.SetActive(false);
            

        }
        else if (currAgent == 1)
        {
            humanAgentNC.SetActive(false);
            humanAgentC.SetActive(true);
            narrator.SetActive(false);
            narratorAgent.SetActive(false);
            chair.SetActive(true);
            //portal.SetActive(false);
        }
        else
        {
            humanAgentNC.SetActive(false);
            humanAgentC.SetActive(false);
            narrator.SetActive(false);
            narratorAgent.SetActive(true);
            chair.SetActive(false);
            //portal.SetActive(true);
        }

    }
}
