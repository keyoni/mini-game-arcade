using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed = 10f;
    public float jumpSpeed = 10f;
    public float fallSpeed = 2f;
    public float jumpHeight = 5;
    public float disToGround = 0.5f;
    public float gravityScale = 5;
    public float fallingGravityScale = 40;
    public float speedval = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {

 
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            rb.AddForce(Vector2.up * jumpSpeed, (ForceMode) ForceMode2D.Impulse);
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);
        movement = Vector3.ClampMagnitude(movement, 1);
        transform.Translate(movement * speedval * Time.deltaTime);


    }


    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale-1)*rb.mass);
    }
}
