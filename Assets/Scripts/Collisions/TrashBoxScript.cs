using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBoxScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AudioMessage")
        {
            Destroy(collision.gameObject);

            GameObject.Find("StateController").GetComponent<StateController>().msgClipNumber--;
            JSONUtils.myPlayer.msgClipNumber=GameObject.Find("StateController").GetComponent<StateController>().msgClipNumber;
            JSONUtils.UpdateJson();
        }
            
    }  
}
