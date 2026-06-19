using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NoteContentManager : MonoBehaviour
{
    private bool noteIsFull = false;

    private string pendingNote = "";

    private Action pendingCallback;

    //[SerializeField] private GameObject eraseButton;
    //[SerializeField] private GameObject replaceButton;

    [SerializeField] private Button[] eraseButtons = new Button[15];
    [SerializeField] private Button[] replaceButtons = new Button[15];

    [SerializeField] private TMP_Text[] noteSlots = new TMP_Text[15];
    private void Start()
    {
        //eraseButton.SetActive(true);
        //replaceButton.SetActive(false);
    }
    public void takeNotes(string description, Action onNoteWritten = null)
    {
        checkIfFull();

        foreach (TMP_Text slot in noteSlots)
        {
            if (slot.text == description)
            {
                Debug.Log("Note already taken, cannot take duplicate notes.");

                onNoteWritten?.Invoke();
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

                    //Run function
                    onNoteWritten?.Invoke();
                    return;
                }
            }
        }

        else
        {
            Debug.Log("List is full, entering replace mode. Pending note: " + description);
            EnterReplaceMode(description, onNoteWritten);
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
    private void EnterReplaceMode(string description, Action onNoteWritten)
    {
        pendingNote = description;
        pendingCallback = onNoteWritten;

        foreach (Button button in eraseButtons)
            button.gameObject.SetActive(false);
        foreach (Button button in replaceButtons)
            button.gameObject.SetActive(true);
    }
    public void eraseLine(int index)
    {
        noteSlots[index].text = "";
    }
    public void replaceLine(int index)
    {
        Debug.Log("Replacing line " + index + " with note: " + pendingNote);
        noteSlots[index].text = pendingNote;
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
