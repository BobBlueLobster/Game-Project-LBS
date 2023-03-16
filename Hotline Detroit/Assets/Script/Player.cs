using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int maxHP = 10;
    public int curHP = 4;

    public int maxHumanity = 100;

    public Collider2D collider1;

    public int killScore = 0;

    public int ammoCount;

    public bool hasGun;
    public bool hasShotgun;

    public PlayerBars healthBar;

    public Gun gunScript;
    public Move moveScript;
    public PlayerRotate rotateScript;

    public PlayerBars humanityBar;

    public AudioClip dying;
    public AudioSource audioSource;

    void Start()
    {
        ammoCount = 0;

        hasGun = false;

        audioSource = GetComponent<AudioSource>();

        collider1 = GetComponent<Collider2D>();

        gunScript = GameObject.Find("Gun").GetComponent<Gun>();
        moveScript = GameObject.Find("TestPlayer2").GetComponent<Move>();
        rotateScript = GameObject.Find("PlayerTransform").GetComponent<PlayerRotate>();


        healthBar = GameObject.Find("HPbar").GetComponent<PlayerBars>();
        humanityBar = GameObject.Find("HPbar").GetComponent<PlayerBars>();

        healthBar.SetHealth(curHP);

        humanityBar.SetHumanity(maxHumanity);
    }

    void Update()
    {
        /*
        healthBar.SetHealth(curHP);

        humanityBar.SetHumanity(maxHumanity);

        if(curHP == 0)
        {
        }
        */
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            curHP--;
            Destroy(col.gameObject);

            if (curHP == 0)
            {
                audioSource.PlayOneShot(dying, 0.7f);

                gunScript.enabled = false;
                collider1.enabled = !collider1.enabled;

                moveScript.body.velocity = new Vector2(0, 0);
                moveScript.enabled = false;

                rotateScript.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Bandage")
        {
            if (curHP < 10)
            {
                curHP += 3;

                if (curHP > 10)
                    curHP = 10;

                healthBar.SetHealth(curHP);

                Destroy(col.gameObject);
                Debug.Log(curHP); 
            }
        }
        
        if(col.gameObject.tag == "Ammo")
        {
            ammoCount += 3;

            Destroy(col.gameObject);
            Debug.Log(ammoCount);
        }
        
        if(col.gameObject.tag == "Pistol")
        {
            hasGun = true;

            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Shotgun")
        {
            hasShotgun = true;

            Destroy(col.gameObject);
        }
    }
}
