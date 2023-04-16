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

    public FieldOfVision1 fov;
    public AIPath aiPath;

    public AudioClip shooting;
    public AudioSource audioSource;

    void Start()
    {
        shootTimer = 0.5f;
        fov = GetComponent<FieldOfVision1>();
        aiPath = GetComponent<AIPath>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (fov.CanSeePlayer == true && aiPath.enabled)
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

        audioSource.PlayOneShot(shooting, 0.5f);
    }
}
