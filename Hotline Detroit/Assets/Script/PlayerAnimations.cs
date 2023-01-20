using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    float horizontal;
    float vertical;

    public Player playerScript;
    public Gun gunScript;

    private float shootTimer = 0.1f;

    void Start()
    {
        gunScript = GameObject.Find("Gun").GetComponent<Gun>();

        animator = GetComponent<Animator>();

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(playerScript.curHP <= 0)
        {
            animator.SetBool("Dead", true);
            animator.SetFloat("Speed", 0);
        }

        if (!playerScript.hasGun)
        {   //Unarmed
            if (horizontal == 0 || vertical == 0)
            {
                //idle anim
                animator.SetFloat("Speed", 0);
            }
            if (horizontal != 0 || vertical != 0 && !Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed", 0.5f);
            }
            if (Input.GetKey(KeyCode.LeftShift) && horizontal != 0 || vertical != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed", 0.9f);
            }
        }
        if (playerScript.hasGun)
        {   //Has Gun
            if (horizontal == 0 || vertical == 0)
            {
                //idle anim
                animator.SetFloat("Speed", 1);
            }
            if (horizontal != 0 || vertical != 0 && !Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed", 1.5f);
            }
            if (Input.GetKey(KeyCode.LeftShift) && horizontal != 0 || Input.GetKey(KeyCode.LeftShift) && vertical != 0)
            {
                animator.SetFloat("Speed", 2);
            }
        }
        if(gunScript.magazineCur > 0)
        {
            if (shootTimer < 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("Firing", true);
                    Invoke("ShootAnim", 0.1f);

                    shootTimer = 0.4f;
                }
            }
        }
    }

    void ShootAnim()
    {
        animator.SetBool("Firing", false);
        //animator.SetFloat("Speed", 0.1f);
    }
}
