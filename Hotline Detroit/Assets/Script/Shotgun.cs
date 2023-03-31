using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int curAmmo;
    private int maxAmmo = 2;
    public int spareAmmo;

    private float shootTimer;

    public GameObject bulletPre;
    public Transform bulletSpawn;

    public float fireVelocity = 65;

    public Transform player;

    public float maxSpread;
    public int bulletsShot;

    void Start()
    {
        curAmmo = 0;

        shootTimer = 0.75f;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (curAmmo > 0 && shootTimer < 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireGun();
                shootTimer = 0.75f;
                curAmmo--;
            }
        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            Invoke("Reload", 3f);
        }
    }

    void Reload()
    {
        if(spareAmmo >= 2)
        {
            curAmmo = maxAmmo;
            spareAmmo -= 2;
        }
        if(spareAmmo <= 2 && curAmmo == 0)
        {
            curAmmo = spareAmmo;
            spareAmmo = 0;
        }
    }

    void FireGun()
    {
        for(int i = 0; i<bulletsShot; i++)
        {
            Quaternion newRot = bulletSpawn.rotation;

            float addedoffset = Random.Range(-maxSpread, maxSpread);

            newRot = Quaternion.Euler(bulletSpawn.eulerAngles.x, bulletSpawn.eulerAngles.y, bulletSpawn.eulerAngles.z + addedoffset);

            bulletSpawn.rotation = newRot;

            GameObject bullet = Instantiate(bulletPre, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(bulletSpawn.right * fireVelocity, ForceMode2D.Impulse);


            bulletSpawn.rotation = player.transform.rotation;
        }
    }
}
