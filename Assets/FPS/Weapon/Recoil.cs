using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Transform weaponTransform; // ��������� ����
    public float recoilDistance = 0.08f; // ³������ ������ �����
    public float recoilAngle = 5f; // ��� ������ ����
    public float recoilSpeed = 0.1f; // �������� ������
    public float returnSpeed = 0.1f; // �������� ����������

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
        // ³�����
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

        // ����������� ������ ��������� ������
        weaponTransform.localPosition = recoilPosition;
        weaponTransform.localRotation = recoilRotation;

        // ���������� � ��������� ���������
        elapsedTime = 0f;

        while (elapsedTime < returnSpeed)
        {
            weaponTransform.localPosition = Vector3.Lerp(recoilPosition, initialPosition, elapsedTime / returnSpeed);
            weaponTransform.localRotation = Quaternion.Slerp(recoilRotation, initialRotation, elapsedTime / returnSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����������� ������ ��������� ����������
        weaponTransform.localPosition = initialPosition;
        weaponTransform.localRotation = initialRotation;
    }
    IEnumerator WeaponRecoilScoped()
    {
        // ³�����
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

        // ����������� ������ ��������� ������
        weaponTransform.localPosition = recoilPosition;
        weaponTransform.localRotation = recoilRotation;

        // ���������� � ��������� ���������
        elapsedTime = 0f;

        while (elapsedTime < returnSpeed)
        {
            weaponTransform.localPosition = Vector3.Lerp(recoilPosition, initialPosScoped, elapsedTime / returnSpeed);
            weaponTransform.localRotation = Quaternion.Slerp(recoilRotation, initialRotation, elapsedTime / returnSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����������� ������ ��������� ����������
        weaponTransform.localPosition = initialPosScoped;
        weaponTransform.localRotation = initialRotation;
    }
}