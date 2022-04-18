using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	float speed;
	Rigidbody2D rb;
    Vector2 moveVelocity;

    void Start()
    {
    	speed = GetComponent<Player>().speed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    	moveVelocity = moveInput.normalized * speed;
    }

    void FixedUpdate() 
    {
    	rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);	
    }	
}
