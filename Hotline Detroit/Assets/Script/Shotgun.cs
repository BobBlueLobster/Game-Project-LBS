using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int curAmmo = 0;
    private int maxAmmo = 2;
    public int spareAmmo;

    public AudioClip shoot;
    public AudioClip reload;
    public AudioClip noAmmo;
    public AudioSource audioSource;

    public Animator muzFShot;
    public GameObject muzAnim;

    private float shootTimer = 0.75f;

    public bool reloading = false;

    public GameObject bulletPre;
    public Transform bulletSpawn;

    public float fireVelocity = 65;

    public Transform player;

    public float maxSpread;
    public int bulletsShot;

    public static Shotgun instance;

    public bool heardPlayer;
    public Collider2D[] possibleEnemiesWhoHeardMe;
    public GameObject temporaryGunObject;
    public int range = 100;
    private LayerMask enemyLayer;

    private GameObject shotgun;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        muzFShot = muzAnim.GetComponent<Animator>();

        shotgun = GameObject.Find("GunBody");
    }

    void Update()
    {
        possibleEnemiesWhoHeardMe = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        //Debug.Log(possibleEnemiesWhoHeardMe.Length);

        shootTimer -= Time.deltaTime;

        if (shootTimer >= 0f)
            muzFShot.SetBool("Shot", false);

        if (curAmmo > 0 && shootTimer < 0 && reloading == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                muzFShot.SetBool("Shot", true);
                audioSource.PlayOneShot(shoot, 0.5f);
                FireGun();
                shootTimer = 0.2f;
                curAmmo--;

                Destroy(temporaryGunObject);
                foreach (Collider2D Enemy in possibleEnemiesWhoHeardMe)
                {
                    {
                        temporaryGunTransform(shotgun, transform);
                        heardPlayer = true;
                        Debug.Log("shoocks");
                    }
                }
            }
        }
        if (curAmmo == 0 && reloading == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(noAmmo, 0.5f);
                shootTimer = 0.4f;
            }
        }

        if (Input.GetKeyUp(KeyCode.R) && spareAmmo != 0)
        {
            audioSource.PlayOneShot(reload, 0.5f);
            reloading = true;
            Invoke("Reload", 5f);
        }
    }

    public void temporaryGunTransform(GameObject obj, Transform newTransform)
    {
        GameObject tempObj = new GameObject("Temp Shotgun Transform");
        tempObj.transform.position = obj.transform.position;
        tempObj.transform.rotation = obj.transform.rotation;

        temporaryGunObject = tempObj;

        StartCoroutine(DestroyTempTransform(/*obj, */tempObj, 5));
    }

    IEnumerator DestroyTempTransform(/*GameObject obj, */GameObject tempObj, float duration)
    {
        yield return new WaitForSeconds(duration);

        //obj.transform.parent = null;
        //obj.transform.position = tempObj.transform.position;
        //obj.transform.rotation = tempObj.transform.rotation;

        Destroy(tempObj);
    }

    void Reload()
    {
        if(spareAmmo >= 2)
        {
            curAmmo = maxAmmo;
            spareAmmo -= 2;
            reloading = false;
        }
        if(spareAmmo <= 2 && curAmmo == 0)
        {
            curAmmo = spareAmmo;
            spareAmmo = 0;
            reloading = false;
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
