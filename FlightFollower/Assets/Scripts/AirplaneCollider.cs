using UnityEngine;

public class AirplaneCollider : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private AudioSource audioSource;

    private void ExplosionEffect()
    {
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        // 1. Play the explosion effect
        ExplosionEffect();

        // 2. Play the explosion sound
        audioSource.Play();
    }

}
