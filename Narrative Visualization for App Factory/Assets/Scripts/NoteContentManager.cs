using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NoteContentManager : MonoBehaviour
{
    private bool noteIsFull = false;

    private string pendingNote = "";

    private Action pendingCallback;

    private Slot.SlotStatus pendingClueType;

    [SerializeField] private Button[] eraseButtons = new Button[15];
    [SerializeField] private Button[] replaceButtons = new Button[15];

    [SerializeField] private TMP_Text[] noteSlots = new TMP_Text[15];
    private void Start()
    {
        //eraseButton.SetActive(true);
        //replaceButton.SetActive(false);
    }
    public void takeNotes(string description, Slot.SlotStatus clueType, Action onNoteWritten = null)
    {
        checkIfFull();

        foreach (TMP_Text slot in noteSlots)
        {
            if (slot.text == description)
            {
                Debug.Log("Note already taken, cannot take duplicate notes.");

                //Probably optional?
                onNoteWritten?.Invoke();
                return;
            }
        }

        //Take note of object in first empty slot
        if (!noteIsFull)
        {
            foreach (TMP_Text slot in noteSlots)
            {
                if (slot.text == "")
                {
                    slot.text = description;

                    slot.GetComponent<Slot>().StatusUpdate(clueType);

                    //Run function
                    onNoteWritten?.Invoke();
                    return;
                }
            }
        }

        //Enter replace mode if list is full
        else
        {
            Debug.Log("List is full, entering replace mode. Pending note: " + description);
            EnterReplaceMode(description, clueType, onNoteWritten);
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

    //Replace selected slot via button onclick
    private void EnterReplaceMode(string description, Slot.SlotStatus clueType, Action onNoteWritten)
    {
        pendingNote = description;
        //Same logic as storing the description
        pendingClueType = clueType;
        pendingCallback = onNoteWritten;

        foreach (Button button in eraseButtons)
            button.gameObject.SetActive(false);
        foreach (Button button in replaceButtons)
            button.gameObject.SetActive(true);
    }
    public void eraseLine(int index)
    {
        noteSlots[index].text = "";

        //Sets slot status to empty
        noteSlots[index].GetComponent<Slot>().StatusUpdate(Slot.SlotStatus.Empty);
    }

    //The onclick event triggered by replace buttons (Where the actual replacement happens)
    public void replaceLine(int index)
    {
        Debug.Log("Replacing line " + index + " with note: " + pendingNote);
        noteSlots[index].text = pendingNote;
        noteSlots[index].GetComponent<Slot>().StatusUpdate(pendingClueType);
        pendingNote = "";

        //Exit replace mode after replacing a line
        foreach (Button button in replaceButtons)
            button.gameObject.SetActive(false);
        foreach (Button button in eraseButtons)
            button.gameObject.SetActive(true);

        //Fire callback
        pendingCallback?.Invoke();
        pendingCallback = null;
    }
}
