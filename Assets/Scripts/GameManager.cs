using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyHealthBar;
    public GameObject FoxHealthBar;
    public GameObject ShineBar;
    public GameObject Level1;
    public GameObject Level2;
    public AudioClip Galaga;
    public AudioClip Spider;
    public AudioClip Notisistema;
    public AudioClip YaNo;
    public AudioClip Wuf;
    public GameObject Invader;
    public bool LevelStarted = false;
    public bool BossStart = false;
    public GameObject GAME;
    public GameObject Win;
    public GameObject LOSE;
    public GameObject MenuPausa;

    private PlayerController player;
    private EnemyBehaviour enemy;
    private AudioSource AudioSource;
    private bool Pausa=false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = FindObjectOfType<EnemyBehaviour>();
        AudioSource = GetComponent<AudioSource>();

        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        float enemyValue = enemy.Health / 300f;
        float playerValue = player.Health / 5f;
        float shineValue = player.shineTimer / player.shineRecoil;

        EnemyHealthBar.GetComponent<Slider>().value = enemyValue;
        FoxHealthBar.GetComponent<Slider>().value = playerValue;
        ShineBar.GetComponent<Slider>().value = shineValue;

        if (Input.GetKeyDown(KeyCode.Escape)){
            Pausa = !Pausa;
            Pausar(Pausa);
        }
    }

    IEnumerator startGame()
    {
        AudioSource.clip = Galaga;
        AudioSource.Play();
        yield return new WaitForSeconds(1);
        Level1.SetActive(true);
        yield return new WaitForSeconds(8);
        AudioSource.clip = Spider;
        AudioSource.Play();
        LevelStarted = true;
        Level1.SetActive(false);
        var inst = Instantiate(Invader);
        inst.transform.position = new Vector2(-8, 3);
    }

    public void startLevel2()
    {
        AudioSource.Stop();
        LevelStarted = false;
        StartCoroutine(Level2Coroutine());
    }

    IEnumerator Level2Coroutine()
    {
        yield return new WaitForSeconds(2);
        AudioSource.clip = Galaga;
        AudioSource.Play();
        yield return new WaitForSeconds(1);
        Level2.SetActive(true);
        yield return new WaitForSeconds(8);
        Level2.SetActive(false);
        BossStart = true;
        AudioSource.clip = Wuf;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void Game(bool ganaste)
    {
        AudioSource.Stop();
        AudioSource.loop = false;
        StartCoroutine(GameCoroutine(ganaste));
    }

    IEnumerator GameCoroutine(bool ganaste)
    {
        LevelStarted = false;
        yield return new WaitForSeconds(1.5f);
        GAME.SetActive(true);
        yield return new WaitForSeconds(4);
        if (ganaste)
        {
            AudioSource.clip = Notisistema;
            AudioSource.Play();
            Win.SetActive(true);
        }
        else
        {
            AudioSource.clip = YaNo;
            AudioSource.Play();
            LOSE.SetActive(true);
        }
        GAME.SetActive(false);
    }

    private void Pausar(bool p)
    {
        if (p)
        {
            MenuPausa.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            MenuPausa.SetActive(false);
        }
    }
}
