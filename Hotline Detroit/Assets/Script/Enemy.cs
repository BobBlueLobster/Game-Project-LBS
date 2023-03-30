using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider2D enemyCollider;
    
    private Animator animator;

    float horizontal;
    float vertical;
     

    public int enemyMaxHP = 5;
    public int enemyCurHP;

    public bool hasEvilGun;

    public Player playerScript;

    void Start()
    {
        enemyCurHP = enemyMaxHP;

        enemyCollider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (enemyCurHP == 0)
        {
            animator.SetBool("Dead", true);
            animator.SetFloat("Speed", 0);

            enemyCollider.enabled = !enemyCollider.enabled;
            enemyCurHP = -1;

            playerScript.maxHumanity -= 10;
            Debug.Log(playerScript.maxHumanity);

            playerScript.killScore++;
        }

        if (horizontal == 0 || vertical == 0)
        {
            //idle anim
            animator.SetFloat("Speed", 0);
        }
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("Speed", 1.5f);
        }
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            enemyCurHP--;
            Destroy(col.gameObject);
        }
    }
}
