using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower;

    public Transform target;

    public GameObject thePlayer;
    private Player playerScript;

    private float shootTimer;

    void Start()
    {
        shootTimer = 0.4f;
        playerScript = thePlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (playerScript.hasGun == true)
                {
                    StartShake(.1f, 0.2f);
                }
                shootTimer = .4f;
            }
        }
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

    }

    private void LateUpdate()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
        }
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;
    }
}
