using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject BulletKiller;
    public GameObject[] atk1Spawner;
    public GameObject atk2Spawner;    
    public GameObject[] atk3Spawner;
    public GameObject atk4Spawner;
    public GameObject[] atkBullet;
    public GameObject gigaLaser;
    public GameObject chargingLaser;
    public PlayerController player;
    public GameObject explosion;
    public int Health = 200;

    public float probLaser = 0.01f;

    private bool attacking = false;
    private bool laser = false;
    private GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            int x = Random.Range(1, 5);
            switch (x)
            {
                case 1:
                    Ataque1();
                    break;
                case 2:
                    Ataque2();
                    break;
                case 3:
                    Ataque3();
                    break;
                case 4:
                    Ataque4();
                    break;
            }

            attacking = true;
        }

        if (!laser)
        {
            float rand = Random.Range(0f, 100f);

            if (rand < probLaser)
            {
                Ataque5();
                probLaser = 0.01f;
            }
            else
            {
                probLaser += 0.0002f;
            }
        }

        if (Health < 1)
        {
            var inst = Instantiate(explosion);
            inst.transform.position = transform.position;
            inst.transform.localScale = new Vector3(6, 4, 1);
            Destroy(gameObject);

            BulletKiller.SetActive(true);
            GameManager.Game(true);
        }
    }

    private void Ataque1()
    {
        StartCoroutine(Ataque1Coroutine());
    }

    IEnumerator Ataque1Coroutine()
    {
        for(int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                var inst = Instantiate(atkBullet[0]);
                inst.transform.position = atk1Spawner[j].transform.position;
                inst.transform.rotation = atk1Spawner[j].transform.rotation;
            }
            yield return new WaitForSeconds(0.1f);
        }
        attacking = false;
    }

    private void Ataque2()
    {
        StartCoroutine(Ataque2Coroutine());
    }

    IEnumerator Ataque2Coroutine()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j<6; j++)
            {
                var inst = Instantiate(atkBullet[1]);
                inst.transform.position = atk2Spawner.transform.position;

                float rangle = Random.Range(-5f, 10f);
                
                Vector3 targ = player.transform.position;
                targ.z = 0f;

                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                inst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rangle - 90));

                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
        }

        attacking = false;
    }

    private void Ataque3()
    {
        StartCoroutine(Ataque3Coroutine());
    }

    IEnumerator Ataque3Coroutine()
    {
        for(int i = 0; i < 50; i++)
        {
            int rand = Random.Range(0, 37);
            var inst = Instantiate(atkBullet[2]);
            inst.transform.position = atk3Spawner[rand].transform.position;
            inst.transform.rotation = atk3Spawner[rand].transform.rotation;
            yield return new WaitForSeconds(0.05f);
        }
        attacking = false;
    }

    private void Ataque4()
    {
        StartCoroutine(Ataque4Coroutine());
    }

    IEnumerator Ataque4Coroutine()
    {
        float currentAngle = 0.0f;
        for (int i = 0; i < 36; i++)
        {
            var inst = Instantiate(atkBullet[3]);
            inst.transform.position = atk4Spawner.transform.position;
            inst.transform.rotation = atk4Spawner.transform.rotation;
            inst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + currentAngle));
            currentAngle += 10.0f;
        }
        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

    private void Ataque5()
    {
        StartCoroutine(Ataque5Coroutine());
    }

    IEnumerator Ataque5Coroutine()
    {
        laser = true;
        chargingLaser.SetActive(true);
        yield return new WaitForSeconds(1);
        chargingLaser.SetActive(false);
        gigaLaser.SetActive(true);
        yield return new WaitForSeconds(2);
        gigaLaser.SetActive(false);
        laser = false;
    }

    public void Hit()
    {
        Health--;
    }
}
