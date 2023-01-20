using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour
{
    public Rigidbody2D body;

    float horizontal;
    float vertical;

    public float sneakSpeed = 4.0f;

    public float walkSpeed = 7.0f;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Screen.SetResolution(1920, 1080, false);

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = sneakSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
            walkSpeed = 7;

        
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * walkSpeed, vertical * walkSpeed);
    }
}