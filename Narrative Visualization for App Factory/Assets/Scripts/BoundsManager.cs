using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    [SerializeField] private ClueManager clueManager;
    [SerializeField] private GameObject nextMapBound;

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

        if (clueManager != null && clueManager.MainClueNoted && clueManager.SecondaryClueIsNoted)
        {
            UnlockNextBound();
        }
    }

    private void UnlockNextBound()
    {
        isUnlocked = true;

        if (nextMapBound != null)
        {
            nextMapBound.SetActive(true);
        }
    }
}