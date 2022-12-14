using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public GameObject bullet;

    private float timer;
    private GameManager GameManager;
    private void Start()
    {
        timer = Time.time;
        GameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime);

        if (Time.time - timer >= 3)
        {
            var inst = Instantiate(bullet);
            inst.transform.position = transform.position;
            inst.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            timer = Time.time;
        }
    }

    public void Hit()
    {
        GameManager.startLevel2();
        Destroy(gameObject);
    }
}
