using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider2D collider;
    
    private Animator animator;

    public int enemyMaxHP = 5;
    public int enemyCurHP;

    void Start()
    {
        enemyCurHP = enemyMaxHP;

        collider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if(enemyCurHP == 0)
        {
            animator.SetBool("Dead", true);
            animator.SetFloat("Speed", 0);

            collider.enabled = !collider.enabled;
            enemyCurHP = -1;
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
