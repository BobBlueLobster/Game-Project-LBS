using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int pHP = 10;

    public bool hasGun;

    void Start()
    {
        hasGun = true;
    }

    void Update()
    {
        if(pHP == 0)
        {
            //gameover
        }
    }
}
