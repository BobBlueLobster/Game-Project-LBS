using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI mag;

    public Animator animator;

    public TextMeshProUGUI dialogue1;

    public Player playerScript;

    public Gun gunScript;

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
        gunScript = GameObject.Find("Gun").GetComponent<Gun>();

        animator = GetComponent<Animator>();

        ammo = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();
        mag = GameObject.Find("Mag").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(playerScript.ammoCount < 10)
        {
            ammo.SetText("0"+playerScript.ammoCount.ToString());
        }
        if(playerScript.ammoCount >= 10)
            ammo.SetText(playerScript.ammoCount.ToString());

        mag.SetText(gunScript.magazineCur + "/");

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

        if(playerScript.killScore == 1)
        {
            dialogue1.SetText("[I didn't sign up for all this, I just want to revenge my loved one...]");
            animator.SetBool("Dialogue1", true);
        }
        if(playerScript.killScore == 4)
        {
            dialogue1.SetText("[Shouldn't have gotten in my way, punk...]");
            animator.SetBool("Dialogue2", true);
        }
        if (playerScript.killScore == 10)
        {
            dialogue1.SetText("[I get this ache...and I thought it was for sex, but it's to tear everything to f*kcing pieces...]");
            animator.SetBool("Dialogue3", true);
        }
    }
}
