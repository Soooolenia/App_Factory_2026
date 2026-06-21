using UnityEngine;

public class Slot : MonoBehaviour
{
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
        currentStatus = newstatus;
        Debug.Log($"Current slot status is noted with {currentStatus}");
    }
}
