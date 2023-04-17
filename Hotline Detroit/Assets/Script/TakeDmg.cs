using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeDmg : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {
        if(playerScript.curHP <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "EnemyBullet")
        {
            playerScript.curHP -= 3;
        }
    }
}
