using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float xPosBase;
    float yPosBase;
    float zPosBase;
    Transform weaponTransform;

    public float newXPos;
    public float newYPos;
    public float newZPos;

    public GameObject bulletPrefab;
    public Transform spawnPoint;

    public GameObject crosshair;
    public GameObject flash;

    public static bool scoped = false;
    private void Start()
    {
        weaponTransform = transform;
        xPosBase = transform.localPosition.x;
        yPosBase = transform.localPosition.y;
        zPosBase = transform.localPosition.z;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            scoped = true;
            crosshair.SetActive(false);
            weaponTransform.localPosition = new Vector3(newXPos, newYPos, newZPos);
        }
        else
        {
            scoped = false;
            crosshair.SetActive(true);
            weaponTransform.localPosition = new Vector3(xPosBase, yPosBase, zPosBase);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Отримуємо камеру гравця
            Camera playerCamera = Camera.main;

            // Напрямок руху проєктиля відповідає напрямку погляду камери
            Vector3 cameraDirection = playerCamera.transform.forward;

            // Визначаємо напрямок руху проєктиля від позиції зброї
            Vector3 direction = cameraDirection.normalized;

            RaycastHit hit;
            if (Physics.Raycast(spawnPoint.position, direction, out hit, 20000f))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            Instantiate(flash, spawnPoint.position, Quaternion.LookRotation(direction));
        }
    }
}
