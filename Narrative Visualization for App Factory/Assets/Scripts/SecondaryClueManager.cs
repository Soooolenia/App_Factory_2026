using Unity.VisualScripting;
using UnityEngine;

public class SecondaryClueManager : MonoBehaviour
{
    private int secondaryClueCount = 0;

    [SerializeField] public bool IsMainClueTaken = false;
    public void AddClueCount()
    {
        secondaryClueCount += 1;
    }
    public void ReduceClueCount()
    {
        secondaryClueCount -= 1;
    }
    public void CheckSecondaryClueCount(int requiredClues)
    {
        if (secondaryClueCount >= requiredClues && IsMainClueTaken)
        {
            Debug.Log("All secondary clues taken, unlocking next chapter!");
            //Unlock next chapter
        }
    }
}
