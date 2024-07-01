using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public string targetName;

    private GameObject target;
    private NavMeshAgent agent; // ����� ��� ���������� NavMeshAgent

    private void Start()
    {
        target = GameObject.Find(targetName);
        agent = GetComponent<NavMeshAgent>(); // ��������� ���������� NavMeshAgent
        if (target != null)
        {
            agent.SetDestination(target.transform.position); // ������������ ������ ����� �������� ��� NavMeshAgent
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            Debug.Log("Core are in danger!");
            StartCoroutine(StopMoving());
        }
    }

    IEnumerator StopMoving()
    {
        agent.isStopped = true; // ������� NavMeshAgent
        yield return new WaitForSeconds(3f);
        agent.isStopped = false; // ³��������� ���� NavMeshAgent
    }
}