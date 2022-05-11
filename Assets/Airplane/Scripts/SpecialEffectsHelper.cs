using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper Instance;

    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private ParticleSystem _fireEffect;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Несколько экземпляров SpecialEffectsHelper!");
            Destroy(this);
        }

        Instance = this;
    }

    public void Explosion(Vector3 position)
    {
        instantiate(_smokeEffect, position);
        instantiate(_fireEffect, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity);
        Destroy(newParticleSystem.gameObject, newParticleSystem.main.startLifetimeMultiplier);
        return newParticleSystem;
    }
}
