﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float rangeRadious;

    [SerializeField] private Transform checkRange;
    [SerializeField] private LayerMask checkLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitExplode());
            FindObjectOfType<AudioManager>().Play("Bomb");
        }
    }

    private void CheckRangeExplode()
    {
        if (checkRange == null)
        {
            Debug.LogWarning("checkRange is not assigned.");
            return;
        }

        Collider2D[] objectRange = Physics2D.OverlapCircleAll(checkRange.position, rangeRadious, checkLayer);

        foreach (Collider2D collider in objectRange)
        {
            if (collider != null)
            {
                collider.GetComponent<PlayerHealth>().TakeDamage(15);
            }
        }
    }

    IEnumerator WaitExplode()
    {
        yield return new WaitForSeconds(1f);

        anim.SetTrigger("exploded");
        CheckRangeExplode();

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(checkRange.position, rangeRadious);
    }
}
