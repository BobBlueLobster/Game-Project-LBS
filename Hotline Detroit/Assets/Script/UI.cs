using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI ammo;

    public Player playerScript;

    public Image crack;

    public Sprite crack1;
    public Sprite crack2;
    public Sprite crack3;
    public Sprite crack4;
    public Sprite crack5;

    void Start()
    {
        crack = GameObject.Find("Crack").GetComponent<Image>();

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


        if(playerScript.curHP < 10 && playerScript.curHP >= 8)
        {
            crack.gameObject.SetActive(true);
            crack.sprite = crack1;
        }
        else if (playerScript.curHP < 8 && playerScript.curHP >= 6)
        {
            crack.gameObject.SetActive(true);
            crack.sprite = crack2;
        }
        else if (playerScript.curHP < 6 && playerScript.curHP >= 4)
        {
            crack.gameObject.SetActive(true);
            crack.sprite = crack3;
        }
        else if (playerScript.curHP < 4 && playerScript.curHP >= 2)
        {
            crack.gameObject.SetActive(true);
            crack.sprite = crack4;
        }
        else if (playerScript.curHP < 2 && playerScript.curHP >= 0)
        {
            crack.gameObject.SetActive(true);
            crack.sprite = crack5;
        }
        else
        {
            crack.gameObject.SetActive(false);

        }
    }
}
