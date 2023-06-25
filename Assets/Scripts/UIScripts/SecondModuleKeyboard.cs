using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondModuleKeyboard : MonoBehaviour
{
    public GameObject message;
    public GameObject inputField;
    public ChestScript CS;
    void Start()
    {
        CS = GameObject.Find("Chest").GetComponent<ChestScript>();
    }

    // spawn message with the text in the input field
    public void spawnMessage()
    {
        GameObject.Find("StateController").GetComponent<Logger>().logEvent("Chest","user wrote a message"); 
        int firstEmptyIndex = GetFirstEmptyPositionIndex();

        var temp = Instantiate(message, CS.chestPositions[firstEmptyIndex].transform.position, Quaternion.Euler(new Vector3(0, 125.05f, 0)));
        temp.GetComponent<MessageScript>().changeMessageContent(inputField.GetComponent<TMP_InputField>().text,firstEmptyIndex); 
        CS.chestPositionsCurr[firstEmptyIndex]=temp;
        inputField.GetComponent<TMP_InputField>().text = "";
    }

    // determine which next position in the chest is empty
    private int GetFirstEmptyPositionIndex()
    {
        for (int i = 0; i <  CS.chestPositionsCurr.Count; i++)
        {
            if ( CS.chestPositionsCurr[i] == null ||  CS.chestPositionsCurr[i].Equals(null))
            {
                return i;
            }
        }

        return -1; // Return -1 if no empty position is found
    }
    
}
