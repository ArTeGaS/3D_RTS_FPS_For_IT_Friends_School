using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Flash : MonoBehaviour
{
    private void Start()
    {
        float angle = Random.Range(0, 361);
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            angle); ;
        StartCoroutine(EndLight());
    }
    IEnumerator EndLight()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
