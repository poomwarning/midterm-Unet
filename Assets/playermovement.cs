using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playermovement : MonoBehaviour
{
    public Animator police;
    public static bool idle;
    public CharacterController Controller;
    public Rigidbody rigi;
    [SerializeField]  float multipler =1f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 moveDirection;

    private void Start()
    {
        police = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position ,groundDistance,groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0)
        {
            
            police.SetBool("isrunning",true);
            idle = true;
        }
        else
        {
            police.SetBool("isrunning",false);
            idle = false;
        }
        //Vector3 move = transform.right * x + transform.forward * z;
        
      //  Controller.Move((move * speed * Time.deltaTime));
        velocity.y += gravity * Time.deltaTime;
        //Controller.Move(velocity * Time.deltaTime);
       moveDirection = transform.forward * z + transform.right * x;
     
    }

    private void FixedUpdate()
    {
        rigi.AddForce(moveDirection.normalized*speed*multipler,ForceMode.Acceleration);
        rigi.AddForce(velocity,ForceMode.Impulse);
    }
}
