using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsycoEducationMessagesViewer : MonoBehaviour
{
    public GameObject[] Messages;
    public GameObject MessageDisplay;
    public bool MessageDisplayBool = false;
    public int currMsg;
    // Start is called before the first frame update
    public void Next()
    {
        foreach (var Msg in Messages)
        {
            Msg.SetActive(false);
        }
        if (currMsg+1 >= 10)
        {
            currMsg = -1;
        }
        currMsg++;
        Messages[currMsg].SetActive(true);
        
    }
    public void Prev()
    {
        foreach (var Msg in Messages)
        {
            Msg.SetActive(false);
        }
        if (currMsg==0)
        {
            currMsg = 5;
        }
        currMsg--;
        Messages[currMsg].SetActive(true);
        
    }
    public void DisplayMessages()
    {
        if (MessageDisplayBool == false)
        {
            MessageDisplayBool = true;
            MessageDisplay.SetActive(true);
        }
        else
        {
            MessageDisplayBool = false;
             MessageDisplay.SetActive(false);
        }
            
        
    }
}
