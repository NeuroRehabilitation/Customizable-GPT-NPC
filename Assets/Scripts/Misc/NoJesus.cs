using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJesus : MonoBehaviour
{
private Vector3 previousPosition;
private PlayerController playerController;

private void Start() {
    playerController = GameObject.Find("VRObject").GetComponent<PlayerController>();
}

private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "water")
    {
         
        GameObject VRObject = GameObject.Find("VRObject");
        VRObject.transform.position = previousPosition;
        playerController.speed = 0;
        Invoke("ResetSpeed", 1);
    }
    
}

private void ResetSpeed() {
    playerController.speed = 10;// your desired speed value;
}

private void Update() {
    //previousPosition = GameObject.Find("VRObject").transform.position;
}


}
