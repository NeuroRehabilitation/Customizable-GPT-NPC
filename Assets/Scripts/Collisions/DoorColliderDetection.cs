using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorColliderDetection : MonoBehaviour
{
    public Vector3 SpawnPos;
    public void Start() 
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);
        
        Vector3 lookAtRotation = Quaternion.LookRotation(GameObject.Find("Rocks").transform.GetChild(GameObject.Find("Rocks").transform.childCount - 1).position - transform.position).eulerAngles;
        transform.rotation = Quaternion.Euler(Vector3.Scale(lookAtRotation, new Vector3(0,1,0)));
    }
    
    void OnCollisionEnter(Collision collision)
    {
        GameObject.Find("StateController").GetComponent<StateController>().SpawnPos=SpawnPos;
        if ( GameObject.Find("StateController").GetComponent<StateController>().milestoneCurrentTopic == 7 && GameObject.Find("StateController").GetComponent<StateController>().currStage == 4)
                GameObject.Find("StateController").GetComponent<StateController>().SpawnPos = new Vector3(-50f,12f,38f );
        if (collision.gameObject.tag == "Player")
        {
            JSONUtils.myPlayer.position[ GameObject.Find("StateController").GetComponent<StateController>().milestoneCurrentTopic][0] = 0.0f;
            JSONUtils.myPlayer.position[ GameObject.Find("StateController").GetComponent<StateController>().milestoneCurrentTopic][1] = 0.0f;
            JSONUtils.myPlayer.position[ GameObject.Find("StateController").GetComponent<StateController>().milestoneCurrentTopic][2] = 0.0f;

            /*JSONUtils.myPlayer.position[0]=0.0f;
            JSONUtils.myPlayer.position[1]=0.0f;
            JSONUtils.myPlayer.position[2]=0.0f;*/
            JSONUtils.UpdateJson();
            GameObject.Find("StateController").GetComponent<StateController>().lastPosition = JSONUtils.myPlayer.position;
            GameObject.Find("StateController").GetComponent<Logger>().logEvent( GameObject.Find("StateController").GetComponent<StateController>().currScene,"exiting");
            GameObject.Find("StateController").GetComponent<StateController>().currScene="Gallery";
            
            SceneManager.LoadScene("Gallery");
        }
            
    }
}
