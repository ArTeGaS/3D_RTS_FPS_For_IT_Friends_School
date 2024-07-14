using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarRotation : MonoBehaviour
{
    private GameObject playerObj;
    private Transform player; // ��������� �� ��'��� ������

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // ���������� ������� �������� �� ������
        Vector3 direction = player.position - transform.position;
        direction.Normalize(); // ����������� ��� ��������� ���������� �������

        // ��������� ������� � �������� �� ������
        Quaternion lookRotation = Quaternion.LookRotation(-direction);
        lookRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        transform.rotation = lookRotation;
    }
}
