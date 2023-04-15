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
    public Shotgun shotgunScript;
    public GameObject shotgunObj;

    public Image _imageRev;
    public Image _imageShot;

    public GameObject revolverUI;
    public GameObject shotgunUI;


    public Sprite s_zero;
    public Sprite s_one;
    public Sprite s_two;

    public GameObject backgoundRev;
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

        shotgunScript = shotgunObj.GetComponent<Shotgun>();

        _imageRev = revolverUI.GetComponent<Image>();
        _imageShot = shotgunUI.GetComponent<Image>();

        animator = revolverUI.GetComponent<Animator>();
    }

    void Update()
    {
        if (playerScript.curWeapon == 1)
        {
            revolverUI.SetActive(true);
            backgoundRev.SetActive(true);
            shotgunUI.SetActive(false);
        }
        if (playerScript.curWeapon == 2)
        {
            revolverUI.SetActive(false);
            backgoundRev.SetActive(false);
            shotgunUI.SetActive(true);
        }

        if (gunScript.magazineCur == 0)
        {
            _imageRev.sprite = a_zero;
        }
        if (gunScript.magazineCur == 1)
        {
            _imageRev.sprite = a_one;
        }
        if (gunScript.magazineCur == 2)
        {
            _imageRev.sprite = a_two;
        }
        if (gunScript.magazineCur == 3)
        {
            _imageRev.sprite = a_three;
        }
        if (gunScript.magazineCur == 4)
        {
            _imageRev.sprite = a_four;

        }
        if (gunScript.magazineCur == 5)
        {
            _imageRev.sprite = a_five;
        }
        if (gunScript.magazineCur == 6)
        {
            _imageRev.sprite = a_six;
        }


        if (shotgunScript.curAmmo == 2)
        {
            _imageShot.sprite = s_two;
        }
        if (shotgunScript.curAmmo == 1)
        {
            _imageShot.sprite = s_one;
        }
        if (shotgunScript.curAmmo == 0)
        {
            _imageShot.sprite = s_zero;
        }

        if (gunScript.isReloading == true)
        {
            animator.SetFloat("Reloading", 1);
            animator.gameObject.GetComponent<Animator>().enabled = true;
        }
        if (gunScript.isReloading == false)
        {
            animator.SetFloat("Reloading", 0);
            animator.gameObject.GetComponent<Animator>().enabled = false;
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
