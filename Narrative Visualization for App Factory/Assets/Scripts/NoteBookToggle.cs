using System;
using UnityEngine;

public class NoteBookToggle : MonoBehaviour
{
    private Animator animator;

    private bool initialized = false;

    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;

    [SerializeField] private GameObject NotebookUI;
    private void Start()
    {
        animator = GetComponent<Animator>();
        downButton.SetActive(false);
        upButton.SetActive(true);
    }
    public void NoteBookUp()
    {
        if (!initialized)
        {
            initialized = true;
            Initialize();
        }

        animator.SetTrigger("Up");
        downButton.SetActive(true);
        upButton.SetActive(false);
    }

    private void Initialize()
    {
        NotebookUI.SetActive(false);
    }

    public void NoteBookDown()
    {
        if (!enabled)
        {
            return;
        }

        animator = GetComponent<Animator>();
        animator.SetTrigger("Down");

        downButton.SetActive(false);
        upButton.SetActive(true);
    }
    public void AddNotes()
    {
        Debug.Log("Add Notes");
    }
}
