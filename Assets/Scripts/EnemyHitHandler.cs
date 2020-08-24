using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour
{
    [SerializeField] int hits = 10;
    [SerializeField] ParticleSystem onHitParticlesPrefab;
    [SerializeField] ParticleSystem deathParticlesPrefab;

    void OnParticleCollision(GameObject other)
    {
        hits--;
        onHitParticlesPrefab.Play();
        if(hits <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        OnDeathParticles();
        Destroy(gameObject);
    }

    void OnDeathParticles()
    {
        var particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(particles.gameObject, particles.main.duration);
    }
}
