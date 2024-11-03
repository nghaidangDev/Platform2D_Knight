using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject cvWin;

    public void NextMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleScene0());
        }
    }

    IEnumerator TeleScene0()
    {
        cvWin.SetActive(true);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }
}
