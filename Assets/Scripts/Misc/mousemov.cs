using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousemov : MonoBehaviour
{
    public float speed = 25.0F;
     public float jumpSpeed = 8.0F; 
     public float gravity = 20.0F;
     private Vector3 moveDirection = Vector3.zero;
     private float turner;
     private float looker;
     public float sensitivity = 5;
     
     
     // Use this for initialization
     void Start () {
         
     }
     
     // Update is called once per frame
        public Rigidbody Rigid;
    public float MouseSensitivity;
    public float MoveSpeed;
    public float JumpForce;
   
    void Update ()
    {
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(Input.GetAxis("Mouse Y") * MouseSensitivity, 0, 0)));
    }
}
