using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationBlock : MonoBehaviour
{
    public bool cubeState = false;

    GameObject cube;

    private void Start()
    {
        cube = transform.Find("Cube")?.gameObject;
    }
    private void Update()
    {
        if (cubeState)
        {
            cube.SetActive(true);
        }
        else if (!cubeState)
        {
            cube.SetActive(false);
        }
    }
}
