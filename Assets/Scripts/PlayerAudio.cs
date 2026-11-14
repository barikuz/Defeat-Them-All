using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [Header("Footstep")]
    [SerializeField] private AudioClip footstepClip;
    [SerializeField, Range(0f, 1f)] private float footstepVolume = 1f;

    [Header("Jump")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField, Range(0f, 1f)] private float jumpVolume = 1f;

    [Header("Attack")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField, Range(0f, 1f)] private float attackVolume = 1f;

    [Header("Hit")]
    [SerializeField] private AudioClip hitClip;
    [SerializeField, Range(0f, 1f)] private float hitVolume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        if (footstepClip == null)
        {
            Debug.LogWarning("Footstep clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(footstepClip, footstepVolume);
    }
    public void PlayJump()
    {
        if (jumpClip == null)
        {
            Debug.LogWarning("Jump clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(jumpClip, jumpVolume);
    }

    public void PlayAttack()
    {
        if (attackClip == null)
        {
            Debug.LogWarning("Attack clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(attackClip, attackVolume);
    }

    public void PlayHit()
    {
        if (hitClip == null)
        {
            Debug.LogWarning("Hit clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(hitClip, hitVolume);
    }
}