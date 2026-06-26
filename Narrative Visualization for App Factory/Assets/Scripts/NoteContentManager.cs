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

    private NoteTakeToggle pendingOwner;

    //private bool firstTimeReplaceNotes = true;
    //private bool firstTimeReplaceNotesUI = true;

    [SerializeField] private Button[] eraseButtons = new Button[15];
    [SerializeField] private Button[] replaceButtons = new Button[15];

    [SerializeField] private TMP_Text[] noteSlots = new TMP_Text[15];
    private void Start()
    {
        //eraseButton.SetActive(true);
        //replaceButton.SetActive(false);
    }

    //Stores all information about interactable, description, clue type, and the object itself
    public void takeNotes(string description, Slot.SlotStatus clueType, NoteTakeToggle owner, Action onNoteWritten = null)
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

                    Slot slotComponent = slot.GetComponent<Slot>();
                    slotComponent.StatusUpdate(clueType);
                    slotComponent.ownerToggle = owner;

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
            EnterReplaceMode(description, clueType, owner, onNoteWritten);
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
    private void EnterReplaceMode(string description, Slot.SlotStatus clueType, NoteTakeToggle owner, Action onNoteWritten)
    {
        //if (firstTimeReplaceNotes)
        //{
        //    Debug.Log("First time replacing, show UI");
        //    firstTimeReplaceNotes = false;
        //}

        pendingNote = description;
        //Same logic as storing the description
        pendingClueType = clueType;
        pendingOwner = owner;
        pendingCallback = onNoteWritten;

        foreach (Button button in eraseButtons)
            button.gameObject.SetActive(false);
        foreach (Button button in replaceButtons)
            button.gameObject.SetActive(true);
    }
    public void eraseLine(int index)
    {
        Slot slotComponent = noteSlots[index].GetComponent<Slot>();
        if (slotComponent.ownerToggle != null)
        {
            //Unlock interactable and set to null 
            slotComponent.ownerToggle.NoteUnTaken(); 
            slotComponent.ownerToggle = null;
        }

        noteSlots[index].text = "";

        //Sets slot status to empty
        slotComponent.StatusUpdate(Slot.SlotStatus.Empty);
    }

    //The onclick event triggered by replace buttons (Where the actual replacement happens)
    public void replaceLine(int index)
    {
        //if (firstTimeReplaceNotesUI)
        //{
        //    Debug.Log("Replace Notes Initial UI Gone!");
        //    firstTimeReplaceNotesUI = false;
        //}

        Debug.Log("Replacing line " + index + " with note: " + pendingNote);

        Slot slotComponent = noteSlots[index].GetComponent<Slot>();

        if (slotComponent.ownerToggle != null)
        {
            //Unlock interactable
            slotComponent.ownerToggle.NoteUnTaken(); 
        }

        noteSlots[index].text = pendingNote;
        slotComponent.StatusUpdate(pendingClueType);
        slotComponent.ownerToggle = pendingOwner;
        pendingNote = "";
        pendingOwner = null;

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
