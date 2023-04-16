using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private bool collided = false;
    public bool firstDoor;

    public bool openOut = false;
    public bool openIn = false;
    private bool interacted = false;

    public AudioSource audioSource;
    public AudioClip open;

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            collided = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        collided = false;
    }

    void Update()
    {
        if (collided == true)
        {
            if (Input.GetKeyDown("e"))
            {
                if(interacted == false)
                {
                    audioSource.PlayOneShot(open, 0.5f);
                    interacted = true;
                }

                if (openOut == true)
                {
                    animator.SetBool("Interacted", true);
                }
                else if(openIn == true)
                {
                    animator.SetBool("Interacted2", true);
                }
            }
        }
        if(firstDoor == true)
        {
            FirstDoorInteraction();
        }
    }

    private void FirstDoorInteraction()
    {

    }
}
