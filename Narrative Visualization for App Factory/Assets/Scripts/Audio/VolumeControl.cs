using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioSource mainTrack;

    private float mainTrackVolume;

    public void FadeToVolume(float targetVolume, float duration)
    {
        StartCoroutine(FadeVolume(targetVolume, duration));
    }
    private IEnumerator FadeVolume(float targetVolume, float duration)
    {
        float startVolume = mainTrack.volume;   
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mainTrack.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        mainTrack.volume = targetVolume;
    }
    public void VolumeUp()
    {
        FadeToVolume(1f, 1f);
    }
    public void VolumeDown()
    {
        FadeToVolume(0f, 1f);
    }
}