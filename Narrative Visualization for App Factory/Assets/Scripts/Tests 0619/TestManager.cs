using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TestA testA;
    [SerializeField] private TestB testB;

    private void OnEnable()
    {
        //Subscribe to both events
        if (testA != null) testA.OnKeyStatusChanged += CheckBothConditions;
        if (testB != null) testB.OnPowerStatusChanged += CheckBothConditions;
    }

    private void OnDisable()
    {
        //Unsubscribe
        if (testA != null) testA.OnKeyStatusChanged -= CheckBothConditions;
        if (testB != null) testB.OnPowerStatusChanged -= CheckBothConditions;
    }

    private void CheckBothConditions()
    {
        //Null check, then check if both are true
        if (testA != null && testB != null)
        {
            if (testA.HasKey && testB.IsPowerOn)
            {
                UnlockLevel();
            }
        }
    }

    private void UnlockLevel()
    {
        Debug.Log("Conditions met!");
    }
}
