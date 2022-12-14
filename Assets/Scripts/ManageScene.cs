using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public void ChangeScene(string escena)
    {
        StartCoroutine(ChangeSceneCoroutine(escena));
    }

    IEnumerator ChangeSceneCoroutine(string escena)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(escena);
    }
}
