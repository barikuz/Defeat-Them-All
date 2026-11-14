using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorAudio : MonoBehaviour
{
    [SerializeField] private AudioClip openClip;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpen()
    {
        if (openClip == null)
        {
            Debug.LogWarning("Door open clip is not assigned.", this);
            return;
        }

        audioSource.PlayOneShot(openClip, volume);
    }
}