using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int maxHP = 10;
    public int curHP = 4;

    public int maxHumanity = 100;

    public int killScore = 0;

    public int ammoCount;

    public bool hasGun;

    public PlayerBars healthBar;

    public PlayerBars humanityBar;

    void Start()
    {
        ammoCount = 0;

        hasGun = false;

        healthBar = GameObject.Find("HPbar").GetComponent<PlayerBars>();
        humanityBar = GameObject.Find("HPbar").GetComponent<PlayerBars>();

        healthBar.SetHealth(curHP);

        humanityBar.SetHumanity(maxHumanity);
    }

    void Update()
    {
        healthBar.SetHealth(curHP);

        humanityBar.SetHumanity(maxHumanity);
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
        
        if(col.gameObject.tag == "Gun")
        {
            hasGun = true;

            Destroy(col.gameObject);
        }
    }
}
