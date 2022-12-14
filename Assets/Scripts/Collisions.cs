using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public bool isMine = false;
    public bool isLaser = false;

    private PlayerController player;
    private EnemyBehaviour enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = FindObjectOfType<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMine && collision.tag == "Enemy")
        {
            enemy.Hit();
            Destroy(gameObject);
        }
        else if (!isMine && collision.tag == "Player")
        {
            player.Hit();
        }
        else if (collision.tag == "Killer" && !isLaser)
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Invader" && isMine)
        {
            collision.GetComponent<Invader>().Hit();
        }
    }
}
