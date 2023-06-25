using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageScript : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public int id=0;
    public int idTotal=0;
    public Animator animator;
    public void deleteMessage()
    {
        GameObject.Find("StateController").GetComponent<StateController>().WritenMessages[id]="null";

        /*try
        {
            GameObject.Find("Chest").GetComponent<ChestScript>().updatePositions();
            
            GameObject.Find("Chest").GetComponent<ChestScript>().chestPositionIterator--;
            GameObject.Find("Chest").GetComponent<ChestScript>().updatePositions();
        }
        catch (System.Exception)
        {
            
            print("no Chest Script");
        }*/
        try
        {
            GameObject.Find("Chest").GetComponent<ChestScript>().chestPositionsCurr[id]=null;
        }
        catch (System.Exception)
        {
            
            print("gallery");
        }
        
        JSONUtils.myPlayer.WritenMessages=GameObject.Find("StateController").GetComponent<StateController>().WritenMessages;
        JSONUtils.UpdateJson();
        Destroy(gameObject);
    }

    public void changeMessageContent(string messageContent, int id)
    {
        this.id = id;
        //idTotal=id;
        textField.text = messageContent;
        GameObject.Find("StateController").GetComponent<StateController>().WritenMessages[id]=messageContent;
        JSONUtils.myPlayer.WritenMessages=GameObject.Find("StateController").GetComponent<StateController>().WritenMessages;
        JSONUtils.UpdateJson();
    }

    public void Grabbed()
    {
         
        animator.SetBool("Open",true);
        
    }

    public void Released()
    { 
        animator.SetBool("Open",false);
    }
}
