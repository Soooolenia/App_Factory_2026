using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CrossfadeLooper : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioMixerGroup outputMixerGroup;
    [SerializeField] private float crossfadeDuration = 0.5f;
    [SerializeField][Range(0f, 1f)] private float startingVolume = 1f;

    private AudioSource sourceA;
    private AudioSource sourceB;
    private AudioSource activeSource;
    private AudioSource nextSource;

    private float envelopeActive = 1f; // loop crossfade envelope, active source
    private float envelopeNext = 0f;   // loop crossfade envelope, queued source
    private float duckVolume;          // external multiplier set by VolumeControl

    void Awake()
    {
        sourceA = gameObject.AddComponent<AudioSource>();
        sourceB = gameObject.AddComponent<AudioSource>();
        SetupSource(sourceA);
        SetupSource(sourceB);

        activeSource = sourceA;
        nextSource = sourceB;
        duckVolume = startingVolume;
    }

    private void SetupSource(AudioSource src)
    {
        src.clip = musicClip;
        src.loop = false;
        src.playOnAwake = false;
        src.volume = 0f;
        src.outputAudioMixerGroup = outputMixerGroup;
    }

    void Start()
    {
        activeSource.Play();
        StartCoroutine(LoopRoutine());
    }

    void Update()
    {
        activeSource.volume = envelopeActive * duckVolume;
        nextSource.volume = envelopeNext * duckVolume;
    }

    private IEnumerator LoopRoutine()
    {
        while (true)
        {
            float waitTime = musicClip.length - crossfadeDuration;
            yield return new WaitForSeconds(waitTime);

            nextSource.time = 0f;
            nextSource.Play();

            float elapsed = 0f;
            while (elapsed < crossfadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / crossfadeDuration;
                envelopeActive = Mathf.Lerp(1f, 0f, t);
                envelopeNext = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            activeSource.Stop();
            envelopeActive = 1f;
            envelopeNext = 0f;

            (activeSource, nextSource) = (nextSource, activeSource);
        }
    }

    public void SetVolume(float volume) => duckVolume = Mathf.Clamp01(volume);
    public float GetVolume() => duckVolume;
}
