using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [Header("Footstep")]
    [SerializeField] private AudioClip footstepClip;
    [SerializeField, Range(0f, 1f)] private float footstepVolume = 1f;

    [Header("Attack")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField, Range(0f, 1f)] private float attackVolume = 1f;
    
    [Header("Death")]
    [SerializeField] private AudioClip deathClip;
    [SerializeField, Range(0f, 1f)] private float deathVolume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        if (footstepClip == null)
        {
            Debug.LogWarning("Enemy footstep clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(footstepClip, footstepVolume);
    }

    public void PlayAttack()
    {
        if (attackClip == null)
        {
            Debug.LogWarning("Enemy attack clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(attackClip, attackVolume);
    }

    public void PlayDeath()
    {
        if (deathClip == null)
        {
            Debug.LogWarning("Enemy death clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(deathClip, deathVolume);
    }
}