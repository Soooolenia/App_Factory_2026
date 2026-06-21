using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private ClueManager clueManager;
    public enum SlotStatus
    {
        Main,
        Secondary,
        None,
        Empty
    }

    public SlotStatus currentStatus = SlotStatus.Empty;
    public void StatusUpdate(SlotStatus newstatus)
    {
        SlotStatus oldStatus = currentStatus;
        currentStatus = newstatus;
        Debug.Log($"Current slot status is noted with {currentStatus}");

        if (oldStatus == SlotStatus.Main && newstatus != SlotStatus.Main)
        {
            clueManager.MainClueUnnote();
        }

        if (newstatus == SlotStatus.Main && oldStatus != SlotStatus.Main)
        {
            clueManager.MainClueNote();
        }

        if (newstatus == SlotStatus.Secondary && oldStatus != SlotStatus.Secondary)
        {
            clueManager.SecondaryClueUpdate(1);
        }

        if (oldStatus == SlotStatus.Secondary && newstatus != SlotStatus.Secondary)
        {
            clueManager.SecondaryClueUpdate(-1);
        }
    }
}
