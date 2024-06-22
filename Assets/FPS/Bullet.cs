using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed;  // Швидкість проєктиля
    public Transform gunTransform; // Позиція зброї

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        gunTransform = GameObject.Find("Supressor").transform;

        if (Weapon.scoped)
        {
            Scoped(rb);
        }
        else
        {
            NoScope(rb);
        }
        StartCoroutine(LifeTime());
    }
    void NoScope(Rigidbody rb)
    {
        // Отримуємо камеру гравця
        Camera playerCamera = Camera.main;

        // Напрямок руху проєктиля відповідає напрямку погляду камери
        Vector3 cameraDirection = playerCamera.transform.forward;

        // Визначаємо напрямок руху проєктиля від позиції зброї
        Vector3 direction = cameraDirection.normalized;

        // Переміщуємо проєктиль в позицію зброї
        transform.position = gunTransform.position;

        // Обертання проєктиля у напрямку руху
        transform.rotation = Quaternion.LookRotation(direction);

        // Встановлюємо швидкість проєктиля
        rb.velocity = direction * projectileSpeed;
    }
    void Scoped(Rigidbody rb)
    {
        // Отримуємо камеру гравця
        Camera playerCamera = Camera.main;

        // Напрямок руху проєктиля відповідає напрямку погляду камери
        Vector3 direction = playerCamera.transform.forward;

        // Обертання проєктиля у напрямку руху
        transform.rotation = Quaternion.LookRotation(direction);

        // Встановлюємо швидкість проєктиля
        rb.velocity = direction * projectileSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Structure"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
