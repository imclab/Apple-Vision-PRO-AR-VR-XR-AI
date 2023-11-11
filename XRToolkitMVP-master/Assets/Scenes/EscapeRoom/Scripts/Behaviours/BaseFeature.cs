using UnityEngine;

public class BaseFeature : MonoBehaviour
{
    public bool SFXAudioSourceCreated { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipForOnStarted { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipForOnEnded { get; set; }

    private AudioSource audioSource;

    [SerializeField]
    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        CreateSFXAudioSource();
    }

    public void CreateSFXAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        SFXAudioSourceCreated = audioSource != null;
    }

    public void PlayOnStarted()
    {
        if (SFXAudioSourceCreated)
        {
            audioSource.clip = AudioClipForOnStarted;
            audioSource.Play();
        }
    }

    public void PlayOnEnded()
    {
        if (SFXAudioSourceCreated)
        {
            audioSource.clip = AudioClipForOnEnded;
            audioSource.Play();
        }
    }
}
