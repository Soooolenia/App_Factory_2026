using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    [SerializeField] private ClueManager clueManager;
    [SerializeField] private GameObject nextMapBound;
    [SerializeField] private GameObject nextBoundManager;
    [SerializeField] private GameObject indicationSystem;
    [SerializeField] private NagivationVisualManager lastIndicationSystem;

    [SerializeField] private int nextSecondaryClueAmount;

    [SerializeField] private AudioSource boundUnlockSound;

    private bool isUnlocked = false;

    private void OnEnable()
    {
        if (clueManager != null)
        {
            clueManager.OnMainClueStatusChanged += CheckConditions;
            clueManager.OnSecondaryClueStatusChanged += CheckConditions;
        }
    }

    private void OnDisable()
    {
        if (clueManager != null)
        {
            clueManager.OnMainClueStatusChanged -= CheckConditions;
            clueManager.OnSecondaryClueStatusChanged -= CheckConditions;
        }
    }

    private void CheckConditions()
    {
        if (isUnlocked) return;

        if (clueManager != null && clueManager.MainClueNoted && clueManager.secondaryClueIsNoted)
        {
            UnlockNextBound();
        }
    }

    private void UnlockNextBound()
    {
        isUnlocked = true;

        boundUnlockSound.Play();

        if (nextMapBound != null)
        {
            nextMapBound.SetActive(true);
            nextBoundManager.SetActive(true);
            clueManager.ResetClue(nextSecondaryClueAmount);

            if (indicationSystem != null)
            {
                indicationSystem.SetActive(true);
                lastIndicationSystem.StopUIAnimation();
            }
        }
    }
}