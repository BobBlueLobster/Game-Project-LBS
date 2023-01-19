using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI ammo;

    public Player playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Sprite").GetComponent<Player>();

        ammo = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(playerScript.ammoCount < 10)
        {
            ammo.SetText("0"+playerScript.ammoCount.ToString());
        }
        if(playerScript.ammoCount >= 10)
            ammo.SetText(playerScript.ammoCount.ToString());
    }
}
