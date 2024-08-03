using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    // Посилання на три RawImage об'єкти, які представляють рамки піктограм
    public RawImage frame1;
    public RawImage frame2;
    public RawImage frame3;

    public GameObject buildText;
    public GameObject destructText;
    public GameObject turretPanel;
    public GameObject upgradeMenu;

    private int currentFrameUI = 1;

    private Camera playerCamera;

    // Колір для виділення та колір за замовчуванням
    private Color highlightColor = Color.yellow;
    private Color defaultColor = Color.white;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Перевірка натискання клавіш
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectFrame(1);
            currentFrameUI = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectFrame(2);
            currentFrameUI = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectFrame(3);
            currentFrameUI = 3;
        }

        // Перевірка наведення на турель

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20000f))
        {
            if (hit.collider.CompareTag("Frame"))
            {
                buildText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActivateChildObjects(hit.collider.gameObject, "Foundation");
                }
            }
            else
            {
                buildText.SetActive(false);
            }

            if (hit.collider.CompareTag("Foundation"))
            {
                turretPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && currentFrameUI == 1)
                {
                    Debug.Log("Турель повинна спавнитись");
                    ActivateChildObjectsFromAnother(hit.collider.gameObject, "Turret_1");
                }
                else if (Input.GetKeyDown(KeyCode.E) && currentFrameUI == 2)
                {
                    ActivateChildObjectsFromAnother(hit.collider.gameObject, "Turret_2");
                }
                else if (Input.GetKeyDown(KeyCode.E) && currentFrameUI == 3)
                {
                    ActivateChildObjectsFromAnother(hit.collider.gameObject, "Turret_3");
                }
            }
            else
            {
                turretPanel.SetActive(false);
            }

            if (hit.collider.CompareTag("Turret_1") ||
                hit.collider.CompareTag("Turret_2") ||
                hit.collider.CompareTag("Turret_3"))
            {
                upgradeMenu.SetActive(true);
            }
            else
            {
                upgradeMenu.SetActive(false);
            }
        }
    }

    void SelectFrame(int frameNumber)
    {
        // Скидання кольорів всіх рамок до білого
        frame1.color = defaultColor;
        frame2.color = defaultColor;
        frame3.color = defaultColor;

        // Встановлення кольору виділення для обраної рамки
        switch (frameNumber)
        {
            case 1:
                frame1.color = highlightColor;
                break;
            case 2:
                frame2.color = highlightColor;
                break;
            case 3:
                frame3.color = highlightColor;
                break;
        }
    }
    void ActivateChildObjects(GameObject parent, string targetTag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            // Перевіряємо тег або шар
            if (child.CompareTag(targetTag))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    void ActivateChildObjectsFromAnother(GameObject currentChild, string targetTag)
    {
        Transform[] children = currentChild.transform.parent.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            // Перевіряємо тег або шар
            if (child.CompareTag(targetTag))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
