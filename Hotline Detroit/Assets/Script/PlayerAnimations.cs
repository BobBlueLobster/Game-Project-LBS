using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimations : MonoBehaviour
{
    public Animator animator;

    public float horizontal;
    public float vertical;

    public Player playerScript;
    public GameObject thePlayer;
    public Gun gunScript;

    private float shootTimer = 0.1f;

    void Start()
    {
        gunScript = GameObject.Find("Gun").GetComponent<Gun>();

        animator = GetComponent<Animator>();

        playerScript = thePlayer.GetComponent<Player>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (playerScript.hasGun == false)
        {
            if(horizontal == 0 || vertical == 0)
            {
                //idle unarmed
                animator.SetFloat("Speed", 0f);
            }
            if(horizontal != 0 || vertical != 0)
            {
                animator.SetFloat("Speed", 0.5f);
            }
        }

        /*
        if(playerScript.curHP <= 0)
        {
            animator.SetBool("Dead", true);
            animator.SetFloat("Speed", 0);
        }
        */
        /*
        if (!playerScript.hasGun)
        {   //Unarmed
            if (horizontal == 0 || vertical == 0)
            {
                //idle anim
                animator.SetFloat("Speed", 0);
            }
            if (horizontal != 0 || vertical != 0)
            {
                animator.SetFloat("Speed", 0.5f);
                Debug.Log("kokoko");
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
    */
    }
    
    void ShootAnim()
    {
        animator.SetBool("Firing", false);
        //animator.SetFloat("Speed", 0.1f);
    }
}
