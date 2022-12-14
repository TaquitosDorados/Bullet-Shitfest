using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public GameObject WolfDialog;
    public GameObject FoxDialog;
    public GameObject FoxBar;
    public GameObject WolfBar;
    public GameObject Wolf;
    public GameObject Fox;

    private GameManager GameManager;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.BossStart)
        {
            GameManager.BossStart = false;
            StartCoroutine(startBoss());
        }

        if (!GameManager.LevelStarted)
        {
            GetComponent<EnemyBehaviour>().enabled = false;
        }
    }

    IEnumerator startBoss()
    {
        animator.Play("EnemyEnter");
        yield return new WaitForSeconds(8);
        WolfDialog.SetActive(true);
        yield return new WaitForSeconds(3);
        WolfDialog.SetActive(false);
        FoxDialog.SetActive(true);
        yield return new WaitForSeconds(3);
        FoxDialog.SetActive(false);
        GetComponent<EnemyBehaviour>().enabled = true;
        GameManager.LevelStarted = true;
        FoxBar.SetActive(true);
        WolfBar.SetActive(true);
        Fox.SetActive(true);
        Wolf.SetActive(true);
    }
}
