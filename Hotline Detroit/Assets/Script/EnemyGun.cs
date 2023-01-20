using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGun : VersionedMonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float speed = 20;

    private float shootTimer;

    public Enemy enemyScript;
    public FieldOfVision1 fov;

    void Start()
    {
        shootTimer = 0.5f;
        fov = GetComponent<FieldOfVision1>();
        //enemyScript = GameObject.Find("Sprite").GetComponent<Player>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (fov.CanSeePlayer == true)
        {
            if(shootTimer < 0)
            {
                Fire();
                shootTimer = 0.5f;
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawn.right * speed, ForceMode2D.Impulse);
        Debug.Log("Fire");
    }
}
