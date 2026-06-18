using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class NoteContentManager : MonoBehaviour
{
    private bool noteIsFull = false;

    [SerializeField] private TMP_Text[] noteSlots = new TMP_Text[15];
    public void takeNotes(string description)
    {
        checkIfFull();

        foreach (TMP_Text slot in noteSlots)
        {
            if (slot.text == description)
            {
                Debug.Log("Note already taken, cannot take duplicate notes.");
                return;
            }
        }

        if (!noteIsFull)
        {
            foreach (TMP_Text slot in noteSlots)
            {
                if (slot.text == "")
                {
                    slot.text = description;
                    return;
                }
            }
        }

        else
        {
            Debug.Log("Note is full, cannot take more notes.");
        }
    }
    private void checkIfFull()
    {
        //Assume it is full
        noteIsFull = true;

        //Check if full, set to false if not
        foreach (TMP_Text slot in noteSlots)
        {
            if (slot.text == "")
            {
                noteIsFull = false;
                return;
            }
        }
    }
}
