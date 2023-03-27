using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private int curAmmo;
    private int maxAmmo = 5;

    public GameObject bulletPre;
    public Transform bulletSpawn;

    public float fireVelocity = 65;

    public float maxSpread;
    public int bulletsShot;

    void Start()
    {
        curAmmo = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireGun();
        }
    }

    void FireGun()
    {
        Vector2 dir = new Vector2(Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread));

        GameObject bullet = Instantiate(bulletPre, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(bulletSpawn.right * fireVelocity, ForceMode2D.Impulse);
    }
}
