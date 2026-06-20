using UnityEngine;
using System;

public class ClueManager : MonoBehaviour
{
    [SerializeField] private bool _mainClueIsNoted = false;
    [SerializeField] private int secondaryClueCounter = 0;
    [SerializeField] private int secondaryClueRequiredAmount;
    [SerializeField] private bool _secondaryClueIsNoted = false;

    public event Action OnMainClueStatusChanged;
    public event Action OnSecondaryClueStatusChanged;

    public bool MainClueNoted
    {
        get => _mainClueIsNoted;
        set
        {
            if (_mainClueIsNoted != value)
            {
                _mainClueIsNoted = value;
                OnMainClueStatusChanged?.Invoke();
            }
        }
    }

    public bool SecondaryClueIsNoted
    {
        get => _secondaryClueIsNoted;
        set
        {
            if (_secondaryClueIsNoted != value)
            {
                _secondaryClueIsNoted = value;
                OnSecondaryClueStatusChanged?.Invoke();
            }
        }
    }

    public void MainClueNote()
    {
        MainClueNoted = true; 
    }

    public void MainClueUnnote()
    {
        MainClueNoted = false;
    }

    public void SecondaryClueUpdate(int points)
    {
        secondaryClueCounter += points;
        if (secondaryClueCounter >= secondaryClueRequiredAmount)
        {
            SecondaryClueIsNoted = true; 
        }
    }
}