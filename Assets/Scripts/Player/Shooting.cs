using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ObjectPooling pooling;
    public Transform firePoint;
    public float bulletSpeed = 5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shotting();
        }
    }

    private void Shotting()
    {
        GameObject bullet = pooling.GetGameObject();
        bullet.transform.position = firePoint.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }
}
