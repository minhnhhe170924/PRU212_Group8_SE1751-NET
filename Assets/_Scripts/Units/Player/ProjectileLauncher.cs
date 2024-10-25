using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform laucnhPoint;

    public void FireProjectile()
    {
        Instantiate(projectilePrefab, laucnhPoint.position, projectilePrefab.transform.rotation);
    }
}
