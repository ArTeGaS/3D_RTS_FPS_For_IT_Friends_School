using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationBlock : MonoBehaviour
{
    public bool cubeState = false;
    public bool turretState = false;

    GameObject cube;
    GameObject turret;

    private void Start()
    {
        cube = transform.Find("Cube")?.gameObject;
        turret = transform.Find("Turret")?.gameObject;
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

        if (turretState)
        {
            turret.SetActive(true);
        }
        else if (!turretState)
        {
            turret.SetActive(false);
        }
    }
}
