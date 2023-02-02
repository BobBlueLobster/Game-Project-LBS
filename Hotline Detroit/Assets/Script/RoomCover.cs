using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCover : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            animator.SetBool("Colided", true);
        }
    }
}
