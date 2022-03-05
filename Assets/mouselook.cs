using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;
    public Rigidbody playerridgi;
    private float YRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
      
      
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
         mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
         xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        YRotation += mouseX * mouseSensitivity/500; 
      
      //  playerBody.Rotate((Vector3.up * mouseX));
      //  playerBody.rotation = Quaternion.Euler(0,YRotation,0);
      transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
      playerridgi.MoveRotation(Quaternion.Euler(0,YRotation,0));
    }

    private void FixedUpdate()
    {
        
      
    }
    
}
