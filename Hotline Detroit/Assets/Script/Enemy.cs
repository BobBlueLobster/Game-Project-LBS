using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private AIPath aiPath;
    private Collider2D enemyCollider;
    
    private Animator animator;

    float horizontal;
    float vertical;
     

    public float enemyMaxHP = 9;
    public float enemyCurHP;

    public bool hasEvilGun;

    public FieldOfVision1 fov;
    public Player playerScript;
    public GameObject temporaryEnemyTransform;
    public GameObject enemysprite;

    void Start()
    {
        enemyCurHP = enemyMaxHP;

        fov = GetComponentInParent<FieldOfVision1>();

        aiPath = GetComponentInParent<AIPath>();

        enemyCollider = GetComponent<Collider2D>();

        animator = GetComponent<Animator>();

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();

        temporaryGunTransform(enemysprite, transform);
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (enemyCurHP <= 0f)
        {
            animator.SetBool("Dead", true);
            animator.SetFloat("Speed", 0);

            aiPath.enabled = false;
            enemyCollider.enabled = false;
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

    public void temporaryGunTransform(GameObject obj, Transform newTransform)
    {
        GameObject tempObj = new GameObject("Temp Enemy Transform");
        tempObj.transform.position = obj.transform.position;
        tempObj.transform.rotation = obj.transform.rotation;

        temporaryEnemyTransform = tempObj;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            enemyCurHP = enemyCurHP - 3f;
            Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "Pellet")
        {
            enemyCurHP =- 1.5f;
            Destroy(col.gameObject);
        }
    }
}
