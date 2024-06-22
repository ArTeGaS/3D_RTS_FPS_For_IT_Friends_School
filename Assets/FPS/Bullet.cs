using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed;  // �������� ��������
    public Transform gunTransform; // ������� ����

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
        // �������� ������ ������
        Camera playerCamera = Camera.main;

        // �������� ���� �������� ������� �������� ������� ������
        Vector3 cameraDirection = playerCamera.transform.forward;

        // ��������� �������� ���� �������� �� ������� ����
        Vector3 direction = cameraDirection.normalized;

        // ��������� �������� � ������� ����
        transform.position = gunTransform.position;

        // ��������� �������� � �������� ����
        transform.rotation = Quaternion.LookRotation(direction);

        // ������������ �������� ��������
        rb.velocity = direction * projectileSpeed;
    }
    void Scoped(Rigidbody rb)
    {
        // �������� ������ ������
        Camera playerCamera = Camera.main;

        // �������� ���� �������� ������� �������� ������� ������
        Vector3 direction = playerCamera.transform.forward;

        // ��������� �������� � �������� ����
        transform.rotation = Quaternion.LookRotation(direction);

        // ������������ �������� ��������
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
