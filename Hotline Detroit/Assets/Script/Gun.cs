using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float speed = 20;

    private float shootTimer;

    void Start()
    {
        shootTimer = 0.5f;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (shootTimer < 0)
            {
                Fire();
                shootTimer = 0.4f;
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
