
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    public Slider slider;

    public Sprite face1;
    public Sprite face2;
    public Sprite face3;

    public Player playerScript;

    public Image m_Image;

    void Start()
    {
        m_Image = GameObject.Find("Player_Face").GetComponent<Image>();

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Update()
    {
        if(playerScript.killScore == 0)
        {
            m_Image.sprite = face1;
        }
        if(playerScript.killScore == 4)
        {
            m_Image.sprite = face2;
        }
        if(playerScript.killScore == 10)
        {
            m_Image.sprite = face3;
        }
    }
}
