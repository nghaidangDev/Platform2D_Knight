using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private PlayerController playerController;

    private Animator anim;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }

        if (anim == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaittingForDoor());
        }
    }

    IEnumerator WaittingForDoor()
    {
        anim.SetTrigger("opendoor");

        playerController.DisableMove();

        yield return new WaitForSeconds(2f);

        TransferScene_Level01();
    }

    private void TransferScene_Level01()
    {
        SceneManager.LoadScene(2);
    }
}
