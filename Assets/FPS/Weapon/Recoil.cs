using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Transform weaponTransform; // Трансформ зброї
    public float recoilDistance = 0.08f; // Відстань віддачі назад
    public float recoilAngle = 5f; // Кут віддачі вниз
    public float recoilSpeed = 0.1f; // Швидкість віддачі
    public float returnSpeed = 0.1f; // Швидкість повернення

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 initialPosScoped;

    void Start()
    {
        initialPosition = weaponTransform.localPosition;
        initialRotation = weaponTransform.localRotation;
        initialPosScoped = new Vector3(0, -0.158f, 0.9f);
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WeaponRecoilScoped());
        }
        else if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WeaponRecoil());
        }
    }

    IEnumerator WeaponRecoil()
    {
        // Віддача
        Vector3 recoilPosition = initialPosition + Vector3.back * recoilDistance;
        Quaternion recoilRotation = initialRotation * Quaternion.Euler(-recoilAngle, 0, 0);

        float elapsedTime = 0f;

        while (elapsedTime < recoilSpeed)
        {
            weaponTransform.localPosition = Vector3.Lerp(initialPosition, recoilPosition, elapsedTime / recoilSpeed);
            weaponTransform.localRotation = Quaternion.Slerp(initialRotation, recoilRotation, elapsedTime / recoilSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Забезпечуємо кінцеве положення віддачі
        weaponTransform.localPosition = recoilPosition;
        weaponTransform.localRotation = recoilRotation;

        // Повернення в початкове положення
        elapsedTime = 0f;

        while (elapsedTime < returnSpeed)
        {
            weaponTransform.localPosition = Vector3.Lerp(recoilPosition, initialPosition, elapsedTime / returnSpeed);
            weaponTransform.localRotation = Quaternion.Slerp(recoilRotation, initialRotation, elapsedTime / returnSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Забезпечуємо кінцеве положення повернення
        weaponTransform.localPosition = initialPosition;
        weaponTransform.localRotation = initialRotation;
    }
    IEnumerator WeaponRecoilScoped()
    {
        // Віддача
        Vector3 recoilPosition = initialPosScoped + Vector3.back * recoilDistance;
        Quaternion recoilRotation = initialRotation * Quaternion.Euler(-recoilAngle, 0, 0);

        float elapsedTime = 0f;

        while (elapsedTime < recoilSpeed)
        {
            weaponTransform.localPosition = Vector3.Lerp(initialPosScoped, recoilPosition, elapsedTime / recoilSpeed);
            weaponTransform.localRotation = Quaternion.Slerp(initialRotation, recoilRotation, elapsedTime / recoilSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Забезпечуємо кінцеве положення віддачі
        weaponTransform.localPosition = recoilPosition;
        weaponTransform.localRotation = recoilRotation;

        // Повернення в початкове положення
        elapsedTime = 0f;

        while (elapsedTime < returnSpeed)
        {
            weaponTransform.localPosition = Vector3.Lerp(recoilPosition, initialPosScoped, elapsedTime / returnSpeed);
            weaponTransform.localRotation = Quaternion.Slerp(recoilRotation, initialRotation, elapsedTime / returnSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Забезпечуємо кінцеве положення повернення
        weaponTransform.localPosition = initialPosScoped;
        weaponTransform.localRotation = initialRotation;
    }
}