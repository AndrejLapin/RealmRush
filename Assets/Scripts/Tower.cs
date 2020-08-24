using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float targetRange = 3f;

    public Waypoint baseWaypoint;

    ParticleSystem.EmissionModule gunEmission;
    Transform targetEnemy;

    // Update is called once per frame

    private void Start()
    {
        gunEmission = GetComponentInChildren<ParticleSystem>().emission;
    }

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            ShootAtEnemy();
        }
        else
        {
            SetGunActive(false);
        }
    }

    private void SetTargetEnemy()
    {
        var SceneEnemies = FindObjectsOfType<EnemyHitHandler>();
        if (SceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemy = SceneEnemies[0].transform;

        foreach (EnemyHitHandler testEnemy in SceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        if (Vector3.Distance(transformA.position, gameObject.transform.position) > Vector3.Distance(transformB.position, gameObject.transform.position))
        {
            return transformB;
        }
        return transformA;
    }

    private void ShootAtEnemy()
    {
        if (DistanceToTarget() > targetRange * 10)
        {
            SetGunActive(false);
        }
        else
        {
            SetGunActive(true);
        }
    }

    private void SetGunActive(bool isActive)
    {
        gunEmission.enabled = isActive;
    }

    private float DistanceToTarget()
    {
        return Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
    }
}
