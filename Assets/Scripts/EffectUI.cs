using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject effectStart;

    private void Start()
    {
        EffectText();
    }

    private void EffectText()
    {
        StartCoroutine(WaitingSmooth());
    }

    IEnumerator WaitingSmooth()
    {
        anim.SetTrigger("smooth");

        yield return new WaitForSeconds(2f);

        effectStart.SetActive(false);
    }
}
