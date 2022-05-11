using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
    public static SoundEffectsHelper Instance;

    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _playerShotSound;
    [SerializeField] private AudioClip _enemyShotSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Несколько экземпляров SoundEffectsHelper!");
            Destroy(this);
        }
        Instance = this;
    }

    public void MakeExplosionSound()
    {
        MakeSound(_explosionSound);
    }

    public void MakePlayerShotSound()
    {
        MakeSound(_playerShotSound);
    }

    public void MakeEnemyShotSound()
    {
        MakeSound(_enemyShotSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}