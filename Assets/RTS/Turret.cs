using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject flash;

    public Transform turretBase; // ������ ���������
    public Transform turretBarrel; // ����� �����
    
    public float rotationSpeed = 5f; // �������� ���������

    private Transform enemy; // ֳ�� ������
    private List<GameObject> enemiesInRange;

    private void Start()
    {
        enemiesInRange = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enemiesInRange.Remove(other.gameObject);
    }
    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            enemy = enemiesInRange[0].transform;
        }
        if (enemy != null)
        {
            // ��������� ��������� �� ����������
            Vector3 directionToEnemy = enemy.position - turretBase.position;
            directionToEnemy.y = 0; // �������� ����������� ����������
            Quaternion targetRotationBase = Quaternion.LookRotation(directionToEnemy);
            turretBase.rotation = Quaternion.Slerp(turretBase.rotation, targetRotationBase, rotationSpeed * Time.deltaTime);

            // ��������� ������ �� ��������
            Vector3 directionToEnemyBarrel = enemy.position - turretBarrel.position;
            Quaternion targetRotationBarrel = Quaternion.LookRotation(directionToEnemyBarrel);
            Vector3 barrelEulerAngles = targetRotationBarrel.eulerAngles;
            barrelEulerAngles.y = turretBarrel.localEulerAngles.y; // �������� ������� ������������� ��������
            turretBarrel.localEulerAngles = Vector3.Slerp(turretBarrel.localEulerAngles, barrelEulerAngles, rotationSpeed * Time.deltaTime);
        }
    }
}
