using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODSkillCaster : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform laucnhPoint;

    public void CastSkill()
    {
        GameObject player = GameObject.Find("Player");
        System.Random rnd = new System.Random();
        double rndX = rnd.NextDouble() * 4;

        laucnhPoint = player.transform;
        laucnhPoint.position.Set(laucnhPoint.position.x + (float)rndX, laucnhPoint.position.y + 1, laucnhPoint.position.z);
        GameObject projectile = Instantiate(projectilePrefab, laucnhPoint.position, projectilePrefab.transform.rotation);
    }
}
