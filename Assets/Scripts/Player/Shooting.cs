using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ObjectPooling pooling;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float cooldownTime = 0.5f;

    private float lastShotTime;
    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        firePoint.right = direction;

        if (Input.GetMouseButtonDown(1) && Time.time >= lastShotTime + cooldownTime)
        {
            lastShotTime = Time.time;
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
