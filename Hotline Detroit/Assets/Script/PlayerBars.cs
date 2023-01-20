using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    public Slider slider;

    public Slider humanitySlider;

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

    public void SetMaxHumanity(int humanity)
    {
        humanitySlider.maxValue = humanity;
        humanitySlider.value = humanity;
    }

    public void SetHumanity(int humanity)
    {
        humanitySlider.value = humanity;
    }

    void Update()
    {
        if(playerScript.maxHumanity == 100)
        {
            m_Image.sprite = face1;
        }
        if(playerScript.maxHumanity == 60)
        {
            m_Image.sprite = face2;
        }
        if(playerScript.maxHumanity <= 0)
        {
            m_Image.sprite = face3;
        }
    }
}
