using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject ball;
    private Animator animator;
    private Rigidbody2D rb;
    
    void Awake() {
        animator = GetComponent<Animator>();
        rb = ball.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
