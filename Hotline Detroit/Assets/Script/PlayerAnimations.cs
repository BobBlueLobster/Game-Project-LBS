using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    float horizontal;
    float vertical;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 || vertical == 0)
        {
            //idle anim
            animator.SetFloat("Speed", 0);
        }
        if (horizontal != 0 || vertical != 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 0.5f);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 1);
        }
    }
}
