using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;
    public GameObject bullet;

    private float Timer;
    private GameManager GameManager;

    private void Start()
    {
        Timer = Time.time;
        GameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Time.time - Timer >= 0.25f && GameManager.LevelStarted)
        {
            var bul1 = Instantiate(bullet);
            bul1.transform.position = gun1.transform.position;
            var bul2 = Instantiate(bullet);
            bul2.transform.position = gun2.transform.position;
            Timer = Time.time;
        }
    }
}
