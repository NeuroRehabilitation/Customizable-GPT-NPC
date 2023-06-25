using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    public float speed = 25.0F;
     public float jumpSpeed = 8.0F; 
     public float gravity = 20.0F;
     private Vector3 moveDirection = Vector3.zero;
     private float turner;
     private float looker;
     public float sensitivity = 5;

     
     
     // Use this for initialization
     void Start () 
     {
         
     }
     
     // Update is called once per frame
    public Rigidbody Rigid;
    public float MouseSensitivity;
    public float MoveSpeed;
    public float JumpForce;
   
    void Update ()
    {
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(Input.GetAxis("Mouse Y") * MouseSensitivity, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));
        Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
        if (Input.GetKeyDown("space"))
            Rigid.AddForce(transform.up * JumpForce);
    }
     
}
