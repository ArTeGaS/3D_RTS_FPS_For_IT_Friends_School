using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarRotation : MonoBehaviour
{
    private GameObject playerObj;
    private Transform player; // посилання на об'єкт гравця

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // Визначення вектора напрямку до гравця
        Vector3 direction = player.position - transform.position;
        direction.Normalize(); // Нормалізація для отримання одиничного вектора

        // Створюємо поворот в напрямку до гравця
        Quaternion lookRotation = Quaternion.LookRotation(-direction);
        lookRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        transform.rotation = lookRotation;
    }
}
