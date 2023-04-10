using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Animator animator;

    public Player playerScript;

    public Gun gunScript;

    public Image _image;

    public GameObject revolverUI;

    public Sprite a_zero;
    public Sprite a_one;
    public Sprite a_two;
    public Sprite a_three;
    public Sprite a_four;
    public Sprite a_five;
    public Sprite a_six;


    void Start()
    {
        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
        gunScript = GameObject.Find("Gun").GetComponent<Gun>();

        _image = revolverUI.GetComponent<Image>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(gunScript.magazineCur == 0)
        {
            _image.sprite = a_zero;
        }
        if (gunScript.magazineCur == 1)
        {
            _image.sprite = a_one;
        }
        if (gunScript.magazineCur == 2)
        {
            _image.sprite = a_two;
        }
        if (gunScript.magazineCur == 3)
        {
            _image.sprite = a_three;
        }
        if (gunScript.magazineCur == 4)
        {
            _image.sprite = a_four;
        }
        if (gunScript.magazineCur == 5)
        {
            _image.sprite = a_five;
        }
        if (gunScript.magazineCur == 6)
        {
            _image.sprite = a_six;
        }

        /*
        if(playerScript.ammoCount < 10)
        {
            ammo.SetText("0"+playerScript.ammoCount.ToString());
        }
        if(playerScript.ammoCount >= 10)
            ammo.SetText(playerScript.ammoCount.ToString());

        mag.SetText(gunScript.magazineCur + "/");
        */

        /*
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
        */

        /*
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
        */
    }
}
