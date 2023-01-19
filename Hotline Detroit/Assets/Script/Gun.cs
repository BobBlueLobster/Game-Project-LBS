using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float speed = 20;

    private float shootTimer;

    public Player playerScript;

    void Start()
    {
        shootTimer = 0.5f;

        playerScript = GameObject.Find("Sprite").GetComponent<Player>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (playerScript.hasGun)
        {
            if(playerScript.ammoCount > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (shootTimer < 0)
                    {
                        Fire();
                        shootTimer = 0.4f;
                        playerScript.ammoCount--;
                    }
                }
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawn.right * speed, ForceMode2D.Impulse);
    }
}
