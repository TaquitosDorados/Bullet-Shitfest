using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject clearBullets;
    public GameObject explosion;
    public int Health = 3;
    public GameObject Shine;
    public float shineTimer;
    public float shineRecoil = 5;
    public AudioSource Gritos;
    public AudioClip FoxHit;
    public AudioClip FoxDeD;

    private Vector3 mousePosition;
    private CircleCollider2D hitbox;
    private float Timer;
    private GameManager GameManager;

    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
        Timer = Time.time;
        GameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);

        if (Health < 1)
        {
            Gritos.clip = FoxDeD;
            Gritos.Play();
            GameManager.Game(false);
            var inst = Instantiate(explosion);
            inst.transform.position = transform.position;
            Destroy(gameObject);
        }

        if (shineTimer >= shineRecoil)
        {
            shineTimer = shineRecoil;
        } else
        {
            shineTimer = Time.time - Timer;
        }
    }

    public void Hit()
    {
        Gritos.clip = FoxHit;
        Gritos.Play();
        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        clearBullets.transform.position = new Vector2(0, 0);
        clearBullets.SetActive(true);
        Health--;
        hitbox.enabled = false;
        yield return new WaitForSeconds(0.1f);
        clearBullets.SetActive(false);
        yield return new WaitForSeconds(1);
        hitbox.enabled = true;
    }

    private void OnMouseDown()
    {
        if (shineTimer == shineRecoil)
        {
            StartCoroutine(ShineCoroutine());
            Timer = Time.time;
            shineTimer = 0;
        }
    }

    IEnumerator ShineCoroutine()
    {
        Shine.SetActive(true);
        GetComponent<AudioSource>().Play();
        hitbox.enabled = false;
        yield return new WaitForSeconds(1);
        Shine.SetActive(false);
        hitbox.enabled = true;
    }
}
