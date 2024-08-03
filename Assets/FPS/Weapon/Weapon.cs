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

    private Camera playerCamera;
    private float normalFOV = 60f;
    private float scopedFOV = 45f;

    private void Start()
    {
        playerCamera = Camera.main;
        weaponTransform = transform;
        xPosBase = transform.localPosition.x;
        yPosBase = transform.localPosition.y;
        zPosBase = transform.localPosition.z;
    }

    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    void HandleAiming()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            scoped = true;
            playerCamera.fieldOfView = scopedFOV;

            crosshair.SetActive(false);
            weaponTransform.localPosition = new Vector3(newXPos, newYPos, newZPos);
        }
        else
        {
            scoped = false;
            playerCamera.fieldOfView = normalFOV;
            crosshair.SetActive(true);
            weaponTransform.localPosition = new Vector3(xPosBase, yPosBase, zPosBase);
        }
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Зміна напрямку для рейкасту з урахуванням прицілу
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20000f))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemyAgent enemy = hit.collider.GetComponent<EnemyAgent>();
                    if (enemy != null && enemy.hitPoints.value > 1)
                    {
                        enemy.hitPoints.value--;
                    }
                    else if (enemy != null && enemy.hitPoints.value <= 1)
                    {
                        Destroy(hit.collider.gameObject);
                        Debug.Log("Hit");
                    }
                }
            }

            // Ефект пострілу
            Instantiate(flash, spawnPoint.position, Quaternion.LookRotation(playerCamera.transform.forward));
        }
    }
}
